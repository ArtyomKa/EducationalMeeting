using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Akka.Crawlers.Workers
{
    public class Persist
    {
        private readonly List<string> m_ImageUrls = new List<string>();
        public string PageUrl { get; private set; }

        public ReadOnlyCollection<string> ImgLinks { get { return m_ImageUrls.AsReadOnly(); } }
        public Persist(string pageUrl,List<string> imgLinks)
        {
            PageUrl = pageUrl;
            
            m_ImageUrls.AddRange(imgLinks);
        }

    }
}
