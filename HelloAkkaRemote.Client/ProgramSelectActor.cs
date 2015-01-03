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
    class ProgramSelectActor 
    {
        
        
        public static void Run()
        {

            string config = @"akka {
                                actor {
                                    provider = ""Akka.Remote.RemoteActorRefProvider,Akka.Remote""
                                }
                                remote {
                                    helios.tcp {
                                        port = 10085
                                        hostname = localhost
                                        }
                                    }
                                }
                             }";


            ActorSystem system = ActorSystem.Create("MyActorSystem", ConfigurationFactory.ParseString(config));

            var printer =  system.ActorSelection(@"akka.tcp://MyActorSystem@localhost:8095/user/MessagePrinter");
            
            bool shouldNotQuit = true;
            do
            {
                string input = "";
                Console.WriteLine("Enter a message: ");
                input = Console.ReadLine();
                if (!input.Equals("quit"))
                {
                    Console.WriteLine("Sending {0} from thread {1}", input, Thread.CurrentThread.ManagedThreadId);
                    printer.Tell(new Message(input));
                }
                else
                {
                    shouldNotQuit = false;
                }

            } while (shouldNotQuit);

        }
    }
}
