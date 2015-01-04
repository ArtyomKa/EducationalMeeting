using Akka.Actor;
using System;
using System.Threading;
using HelloAkka.Common;

namespace HelloAkka
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem system = ActorSystem.Create("Hello");
            ActorRef printer = system.ActorOf(Props.Create<MessagePrinter>(),"MessagePrinter");
            
            
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
