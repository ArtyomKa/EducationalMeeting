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

namespace Akka.Crawlers.Workers
{
    class Downloader : ReceiveActor
    {

        public Downloader()
        {
            int numberOfParsers;
            if (!int.TryParse(ConfigurationManager.AppSettings["numberOfParsers"], out numberOfParsers)) numberOfParsers = 2;
            ActorRef parsers = Context.ActorOf(new RoundRobinPool(numberOfParsers).Props(Props.Create<Parser>()),"parsers");
            Receive<DownloadPage>(message =>
            {
                string page = DownloadAsStr(message.Url);
                if (page != null)
                {
                    parsers.Tell(new ParseHtml(message.Depth, page, message.Url ,Sender));
                }
                
            });
        }

        private string DownloadAsStr(string strURL)
        {
            Console.WriteLine("Downloading " + strURL);
            WebRequest objRequest = WebRequest.Create(strURL);
            try
            {
                WebResponse objResponse = objRequest.GetResponse();
                string strResult;
                using (var sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
                Console.WriteLine("Finished Downloading " + strURL);
                return strResult;
            }
            catch(WebException e)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Failed Downloading {0}  - status {1}", strURL, e.Status);
                Console.ResetColor();
                return null;
            }
            
        }
        
    }
}
