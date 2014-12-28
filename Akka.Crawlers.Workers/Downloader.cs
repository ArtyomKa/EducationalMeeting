using Akka.Actor;
using Akka.Crawlers.Workers.Messages;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using log4net;
namespace Akka.Crawlers.Workers
{
    class Downloader : ReceiveActor
    {

        ILog m_logger = LogManager.GetLogger("downloader");
        ActorRef m_parsers;
        public Downloader()
        {
            m_logger.Debug("Created Debugger " + GetHashCode());
            int numberOfParsers;
            if (!int.TryParse(ConfigurationManager.AppSettings["numberOfParsers"], out numberOfParsers)) numberOfParsers = 2;
            m_parsers = Context.ActorOf(new RoundRobinPool(numberOfParsers).Props(Props.Create<Parser>()),"parsers");
            Random random = new Random();
            Receive<DownloadPage>(message =>
            {
                DownloadAsStr(message.Url,message.Depth,Sender);
                
            });
            
        }

        private async void DownloadAsStr(string strURL, int depth, ActorRef sender)
        {
            Console.WriteLine("Downloading " + strURL);
            WebRequest objRequest = WebRequest.Create(strURL);
            try
            {
                WebResponse objResponse = await objRequest.GetResponseAsync();
                string strResult;
                using (var sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = await sr.ReadToEndAsync();
                    sr.Close();
                }
                Console.WriteLine("Finished Downloading " + strURL);
                if (strResult != null)
                {
                    m_parsers.Tell(new ParseHtml(depth, strResult, strURL), sender);
                }
                
            }
            catch(WebException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Failed Downloading {0}  - status {1}", strURL, e.Status);
                Console.ResetColor();
                
            }
            
        }
        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
            m_logger.Debug("Restarting Debugger " + GetHashCode());
        }
        protected override void PostRestart(Exception reason)
        {
            base.PostRestart(reason);
            m_logger.Debug("Debugger " + GetHashCode() + " Restarted");
        }
        
        
    }
}
