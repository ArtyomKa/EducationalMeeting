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
        private List<string> m_Links = new List<string>();
        private List<string> m_Images = new List<string>();

        public string Url { get { return m_Url; } }
        public ReadOnlyCollection<string> Links { get { return m_Links.AsReadOnly(); } }
        public ReadOnlyCollection<string> Images { get { return m_Images.AsReadOnly(); } }

        public DiscoveredLinks(int depth,string url, List<string> links, List<string> images)
        {    
            this.m_Url = url;
            this.m_Links.AddRange(links);
            this.m_Images.AddRange(images);
            Depth = depth;
        }

        public int Depth { get; private set; }
    }
}
