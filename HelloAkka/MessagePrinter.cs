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
            Receive<Message>(message => Console.WriteLine("Received {0} in thread {1} ", message.Text, Thread.CurrentThread.ManagedThreadId));
        }

    }
}
