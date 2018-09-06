using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Transactions;

namespace Notification.Processor
{
    class NotificationServer: INotificationServer
    {
        private bool _shouldWork;
        private int _clock=1;
        public void Initialize()
        {
            _shouldWork = true;
        }
        public void Start()
        {
            var workingThread = new Thread(new ThreadStart(SendNotification));
            workingThread.Start();
        }
        void SendNotification()
        {
            Console.Write("Start Sending Notification \n");

            do
            {
                try
                {
                    // declare the transaction options
                    var transactionOptions = new TransactionOptions
                    {
                        IsolationLevel = IsolationLevel.ReadUncommitted
                    };
                   
                    using (var transScope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                    {

                        Console.Write("Testing " + _clock.ToString() + "\n");






                        transScope.Complete();
                        _clock++;

                        //if (_clock == 10)
                        //{
                        //    _shouldWork = false;
                        //}
                    }
                }
                catch (Exception x)
                {
                    Console.Write("transScope.Complete exception, sleeping 10 sec", x);
                    Thread.Sleep(10000);

                }

                Thread.Sleep(Settings.Default.FetchingIntervalSeconds);

            } while (_shouldWork);
        }
        public void Stop()
        {
            _shouldWork = false;
        }
        public void Pause()
        {
        }
        public void Resume()
        {
        }

    }
}
