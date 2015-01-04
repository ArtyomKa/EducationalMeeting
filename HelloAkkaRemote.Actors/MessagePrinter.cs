using Akka.Actor;
using System;
using HelloAkka.Common;

namespace HelloAkkaRemote.Actors
{
    public class MessagePrinter : ReceiveActor
    {
        public MessagePrinter()
        {
            Receive<Message>(s => Console.WriteLine(s.Text));
        }
    }
}
