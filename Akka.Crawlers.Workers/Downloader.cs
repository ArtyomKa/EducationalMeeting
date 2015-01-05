using Akka.Actor;
using Akka.Crawlers.Workers.Messages;
using Akka.Routing;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using log4net;
namespace Akka.Crawlers.Workers
{
    class Downloader : ReceiveActor
    {
        readonly ILog m_Logger = LogManager.GetLogger("downloader");
        ActorRef m_Parsers;
        public Downloader()
        {

            Receive<DownloadPage>(message => DownloadAsStr(message.Url,message.Depth,Sender));            
        }

        protected override void PreStart()
        {
            base.PreStart();
            m_Logger.Debug("Created Debugger " + GetHashCode());
            int numberOfParsers;
            if (!int.TryParse(ConfigurationManager.AppSettings["numberOfParsers"], out numberOfParsers)) numberOfParsers = 2;
            m_Parsers = Context.ActorOf(new RoundRobinPool(numberOfParsers).Props(Props.Create<Parser>()), "parsers");
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
                    m_Parsers.Tell(new ParseHtml(depth, strResult, strURL), sender);
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
            m_Logger.Debug("Restarting Debugger " + GetHashCode());
        }
        protected override void PostRestart(Exception reason)
        {
            base.PostRestart(reason);
            m_Logger.Debug("Debugger " + GetHashCode() + " Restarted");
        }
        
        
    }
}
