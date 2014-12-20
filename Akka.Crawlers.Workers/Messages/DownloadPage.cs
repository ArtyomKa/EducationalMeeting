using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Crawlers.Workers.Messages
{
    class DownloadPage
    {
        private readonly string m_url;
        public string Url { get { return m_url; } }

        public DownloadPage(int depth,string url)
        {
            m_url = url;
            Depth = depth;

        }

        public int Depth { get; private set; }
    }
}
