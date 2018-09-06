using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Topshelf;

namespace Notification.Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            Host host = HostFactory.New(c =>
            {
                c.Service<NotificationServer>(s =>
                {
                    s.ConstructUsing(builder =>
                    {
                        var server = new NotificationServer();
                        server.Initialize();
                        return server;
                    });

                    s.WhenStarted(server => server.Start());
                    s.WhenPaused(server => server.Pause());
                    s.WhenContinued(server => server.Resume());
                    s.WhenStopped(server => server.Stop());
                });

                c.RunAsLocalSystem();
                c.SetDescription("Log Processor");
                c.SetDisplayName("Log Processor");
                c.SetServiceName("NotificationProcessor");
            });

            host.Run();
        }
    }
}
