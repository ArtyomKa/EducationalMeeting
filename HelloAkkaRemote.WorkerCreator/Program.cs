using System.IO;
using Akka.Actor;
using Akka.Configuration;

namespace HelloAkkaRemote.WorkerCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Config config;
            using (StreamReader reader = new StreamReader(@"akka.conf"))
            {
                string configString = reader.ReadToEnd();
                config = ConfigurationFactory.ParseString(configString);
            }
            ActorSystem actorSystem = ActorSystem.Create("MyActorSystem", config);
            actorSystem.ActorOf(Props.Create<Player>(), "Player");
        }

        public class Player : ReceiveActor
        {
            public Player()
            {
                Receive<string>(s => Sender.Tell("Hello"));
            }
        }
    }
}
