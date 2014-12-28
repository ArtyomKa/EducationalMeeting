using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HelloAkka
{
    class MessagePrinter : ReceiveActor
    {
        
        public MessagePrinter()
        {
            Receive<Message>(message => Console.WriteLine("{0} Received {1}", Thread.CurrentThread.ManagedThreadId, message.Text));
        }

    }
}
