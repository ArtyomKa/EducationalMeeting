using Akka.Actor;
using System;

namespace HelloAkkaRemote.Actors
{
    public class Player : ReceiveActor
    {
        public Player()
        {
            Receive<string>(s => s == "ping", _ => 
                {
                    Console.WriteLine("Got ping. sending pong");
                    Sender.Tell("pong");
                });
        }
    }
}
