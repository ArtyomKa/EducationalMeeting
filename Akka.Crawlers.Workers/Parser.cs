using Akka.Actor;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Akka.Crawlers.Workers
{
    class Parser : ReceiveActor
    {
        public Parser()
        {
            Receive<ParseHtml>(message => message.Controller.Tell(new DiscoveredLinks(message.Depth,message.Url, ExtractLinks(message.Depth,message.Url,message.Html))));
        }

        private List<string> ExtractLinks(int depth,string url,string htmlString)
        {
            Console.WriteLine("Parsing {0} - {1}", depth,url);
            
            Uri pageUri = new Uri(url);
            
            Uri hostUri = new Uri((pageUri.Scheme+"://"+pageUri.Authority+"/"));
            List<string> result = new List<string>();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlString);
            foreach(HtmlNode linkElement in document.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute attribute = linkElement.Attributes["href"];
                string urlStr = attribute.Value;
                Uri uri;
                
                if (Uri.TryCreate(hostUri, urlStr, out uri))
                {
                    if(uri.Host.Equals(hostUri.Host))
                    {
                        result.Add(uri.AbsoluteUri);
                    }
                }
                else
                {
                    uri = new Uri(hostUri, uri);
                    result.Add(uri.ToString());
                }
             
            }
            Console.WriteLine("Finished Parsing ", url);
            return result;
        }
    }
}
