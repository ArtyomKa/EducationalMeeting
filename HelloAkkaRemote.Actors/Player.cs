using Akka.Actor;

namespace HelloAkkaRemote.Actors
{
    public class Player : ReceiveActor
    {
        public Player()
        {
            Receive<string>(s => s == "ping", _ => Sender.Tell("pong"));
        }
    }
}
