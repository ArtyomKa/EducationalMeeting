using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloAkka.Common
{
    public class Message
    {
        
        public string Text { get; private set; }

        public Message(string text)
        {
            Text = text;        
        }

    }
}
