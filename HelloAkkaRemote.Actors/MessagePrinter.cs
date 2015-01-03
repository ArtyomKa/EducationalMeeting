using Akka.Actor;
using System;

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
