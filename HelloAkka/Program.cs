using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HelloAkka
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = ActorSystem.Create("Hello");
            ActorRef printer = system.ActorOf(Props.Create<MessagePrinter>(),"MessagePrinter");
            
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
