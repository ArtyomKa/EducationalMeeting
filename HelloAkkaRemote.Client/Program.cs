using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloAkkaRemote.Actors;
using System.Threading;
namespace HelloAkkaRemote.Client
{
    class Program : ReceiveActor
    {
        public Program()
        {
            
            Receive<string>(s => s == "pong", _ =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("got pong. sending ping...");
                Sender.Tell("ping");
            });

            var player = system.ActorOf(Props.Create<Player>(), "player");
            player.Tell("ping");
        }
        static ActorSystem system;
        static void Main(string[] args)
        {
            string config = @"akka {
                                actor {
                                    provider = ""Akka.Remote.RemoteActorRefProvider,Akka.Remote""
                                    deployment {
                                        ""/player"" {
                                            remote = ""akka.tcp://MyActorSystem@127.0.0.1:8090""
                                            }
                                        }
                                }
                                remote {
                                    helios.tcp {
                                        port = 8095
                                        hostname = localhost
                                        }
                                    }
                                }
                             }";
            /*
             * remote {
                                    helios.tcp {
                                        port = 8090
                                        hostname = localhost
                                        }
                                    }
                                }
             */

            system = ActorSystem.Create("MyActorSystem", ConfigurationFactory.ParseString(config));

            system.ActorOf(Props.Create<Program>());

            Console.ReadLine();

        }
    }
}
