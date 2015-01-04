using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using HelloAkka.Common;

namespace HelloAkka.FaultTolerance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ActorSystem system = ActorSystem.Create("Hello");
            
            ActorRef mainActor = system.ActorOf(new Props(typeof(Forwarder)).WithSupervisorStrategy(CreateSupervisionStrategy()), "MainActor");
            
            
            string input = "";
            while (ReadUserInputUntillQuit("Enter a Message", out input))
            {
                Console.WriteLine("Sending {0} from thread {1}", input, Thread.CurrentThread.ManagedThreadId);
                mainActor.Tell(input);

            }
   
        }
        private static SupervisorStrategy CreateSupervisionStrategy()
        {
            return new OneForOneStrategy(e =>
            {

                if (e.Message.Equals("Very Bad!!!"))
                {
                    return Directive.Restart;
                }
                if (e.Message.Equals("Good luck with it!"))
                {
                    return Directive.Escalate;
                }
                else //Probably not so bad
                {
                    return Directive.Resume;
                }

            });
        }

       
        private static bool ReadUserInputUntillQuit(string message, out string input)
        {
            Console.WriteLine("Enter a message: ");
            input = Console.ReadLine();
            return !input.Equals("quit");
        }
     }

    
    

}
