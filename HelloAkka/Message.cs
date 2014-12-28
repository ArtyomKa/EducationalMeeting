using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloAkka
{
    class Message
    {
        private readonly string m_Message;
        
        public string Text { get; private set; }

        public Message(string text)
        {
            Text = text;        
        }

    }
}
