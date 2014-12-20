using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Akka.Crawlers.Workers
{
    class ParseHtml
    {
        private string m_Url;
        private string m_Html;
        private ActorRef m_Controller;
        private int m_Depth;
        public ParseHtml(int depth, string page, string url, ActorRef controller)
        {
            m_Depth = depth;
            m_Html = page;
            m_Controller = controller;
            m_Url = url;
        }

        public string Url { get { return m_Url; } }
        public string Html { get { return m_Html; } }
        public ActorRef Controller { get { return m_Controller; } }
        public int Depth { get { return m_Depth; } }
    }
}
