using System.IO;
using Akka.Actor;
using Akka.Configuration;
using System;
using HelloAkkaRemote.Actors;

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
            Console.WriteLine("Started system {0}",actorSystem.Name);
            //actorSystem.ActorOf(Props.Create<Player>(), "Player");

            Console.ReadLine();
        }

       
    }
}
