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
            var playerOne = system.ActorOf(Props.Create<MessagePrinter>(),"MessagePrinter");

            string input = "";
            while(!(input = Console.ReadLine()).Equals("quit"))
            {
                Console.WriteLine("Sending {0} from thread {1}", input,Thread.CurrentThread.ManagedThreadId);
                playerOne.Tell(new Message(input));
                
            }
            


            
        }
    }
}
