using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloAkka.Common;
using HelloAkkaRemote.Actors;
using System.Threading;
namespace HelloAkkaRemote.Client
{
    class ProgramCreateActor 
    {
        
        
        public static void Run()
        {

            string config = @"akka {
                                actor {
                                    provider = ""Akka.Remote.RemoteActorRefProvider,Akka.Remote""
                                    deployment {
                                        ""/MessagePrinter"" {
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

            ActorSystem system = ActorSystem.Create("MyActorSystem", ConfigurationFactory.ParseString(config));

            var printer = system.ActorOf(Props.Create<MessagePrinter>(), "MessagePrinter");

            string input = "";
            while (ReadUserInputUntillQuit("Enter a Message", out input))
            {
                Console.WriteLine("Sending {0} from thread {1}", input, Thread.CurrentThread.ManagedThreadId);
                printer.Tell(new Message(input));

            }

        }
        private static bool ReadUserInputUntillQuit(string message, out string input)
        {
            Console.WriteLine("Enter a message: ");
            input = Console.ReadLine();
            return !input.Equals("quit");
        }
    }
}
