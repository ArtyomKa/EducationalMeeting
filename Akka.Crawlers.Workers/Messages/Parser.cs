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
            log4net.LogManager.GetLogger("parser").Debug("Created Parser " + GetHashCode());
            
            Receive<ParseHtml>(message => 
                {
                    Tuple<List<string>,List<string>> linksAndImages = ExtractLinks(message.Depth, message.Url, message.Html);
                    Sender.Tell(new DiscoveredLinks(message.Depth, message.Url, linksAndImages.Item1, linksAndImages.Item2));
                });
        }

        private Tuple<List<string>,List<string>> ExtractLinks(int depth, string url, string htmlString)
        {
            Console.WriteLine("Parsing {0} - {1}", depth, url);

            Uri pageUri = new Uri(url);

            Uri hostUri = new Uri((pageUri.Scheme + "://" + pageUri.Authority + "/"));
            List<string> links = new List<string>();
            List<string> images = new List<string>();
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(htmlString);
            HtmlNodeCollection refs = document.DocumentNode.SelectNodes("//a[@href]");
            if (refs != null)
            {

                foreach (HtmlNode linkElement in refs)
                {
                    HtmlAttribute attribute = linkElement.Attributes["href"];
                    string urlStr = attribute.Value;
                    Uri uri;

                    if (Uri.TryCreate(hostUri, urlStr, out uri))
                    {
                        if (uri.Host.Equals(hostUri.Host))
                        {
                            links.Add(uri.AbsoluteUri);
                        }
                    }


                }
            }
            HtmlNodeCollection imageElements = document.DocumentNode.SelectNodes("//img[@src]");
            if (imageElements != null)
            {
                foreach (HtmlNode imageElement in imageElements)
                {
                    images.Add(imageElement.Attributes["src"].Value);
                }
            }
            Console.WriteLine("Finished Parsing ", url);
            return new Tuple<List<string>,List<string>>(links,images);
        }
    }
}
