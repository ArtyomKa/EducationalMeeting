using Akka.Actor;
using Akka.Crawlers.Workers.Messages;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Crawlers.Workers
{
    public class Controller : ReceiveActor
    {
       
        private readonly ActorRef m_Downloader;
        private readonly HashSet<string> m_VisitedUrls = new HashSet<string>();
        static private int MaxDepth = 3;
        public Controller()
        {
            log4net.LogManager.GetLogger("controller").Debug("Created Controller " + GetHashCode());
            int numberOfDownloaders;
            if (!int.TryParse(ConfigurationManager.AppSettings["numberOfDownloaders"], out numberOfDownloaders)) numberOfDownloaders = 2;
            m_Downloader = Context.ActorOf(new RoundRobinPool(numberOfDownloaders).Props(Props.Create<Downloader>()),"downloaders");
            Receive<string>(s => 
                {
                    log4net.LogManager.GetLogger("controller").Debug("Received a new link " + s);
                    m_Downloader.Tell(new DownloadPage(0, s));
                });
            Receive<DiscoveredLinks>(message => 
            {
                foreach (string link in message.Links)
                {
                    if (!m_VisitedUrls.Contains(link) && message.Depth +1 < MaxDepth)
                    {
                        m_VisitedUrls.Add(link);
                        m_Downloader.Tell(new DownloadPage(message.Depth + 1,link));
                    }
                }
            });
        }

    }
}
