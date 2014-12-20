using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Akka.Crawlers.Workers
{
    class DiscoveredLinks
    {
        private string m_Url;
        private List<string> m_Links;

        public string Url { get { return m_Url; } }
        public ReadOnlyCollection<string> Links { get { return m_Links.AsReadOnly(); } }

        public DiscoveredLinks(int depth,string url, List<string> links)
        {    
            this.m_Url = url;
            this.m_Links = links;
            Depth = depth;
        }

        public int Depth { get; private set; }
    }
}
