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
            Props props = new Props(typeof(MessagePrinter)).WithSupervisorStrategy(CreateSupervisionStrategy());
            ActorRef printer = system.ActorOf(props, "MessagePrinter");
            
            
            string input = "";
            while (ReadUserInputUntillQuit("Enter a Message", out input))
            {
                Console.WriteLine("Sending {0} from thread {1}", input, Thread.CurrentThread.ManagedThreadId);
                printer.Tell(new Message(input));

            }
   
        }

        private static SupervisorStrategy CreateSupervisionStrategy()
        {
            return new OneForOneStrategy(exception =>
            {
                if(exception is )
            });
        }
        private static bool ReadUserInputUntillQuit(string message, out string input)
        {
            Console.WriteLine("Enter a message: ");
            input = Console.ReadLine();
            return !input.Equals("quit");
        }
     }

    class MainActor : ReceiveActor
    {
        public MainActor()
        {
            Receive<string>(s => s.Equals("Begin"), _ =>
            {
                ActorRef actorRef = Context.ActorOf(Props.Create<MessagePrinter>(), "MessagePrinter");
            });
        }
    }
    class MessagePrinter : ReceiveActor
    {
        public MessagePrinter()
        {
            Receive<Message>(s => s.Text.Equals("fail") , s => Fail());
            Receive<Message>(message => Console.WriteLine("Received {0} in thread {1} ", message.Text, Thread.CurrentThread.ManagedThreadId));

        }
        
        protected override void PreStart()
        {
            base.PreStart();
            DisplayColoredMessage(ConsoleColor.DarkGreen, "Starting MessagePrinter.");
        }


        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
            DisplayColoredMessage(ConsoleColor.DarkGreen, "Restarting MessagePrinter.");

        }

        protected override void PostRestart(Exception reason)
        {
            base.PostRestart(reason);
            DisplayColoredMessage(ConsoleColor.DarkGreen, "MessagePrinter Restarted.");
        }


        private static void DisplayColoredMessage(ConsoleColor messageColor, string message)
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
            Console.ForegroundColor = foregroundColor;
        }

        private void Fail()
        {
            throw new Exception("Message Printer Failed");
        }
    }

}
