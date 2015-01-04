using Akka.Actor;
using HelloAkka.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HelloAkka.FaultTolerance
{
    class MessagePrinter : ReceiveActor
    {
        private int m_Counter = 0;
        public MessagePrinter()
        {
            Receive<Message>(s => s.Text.StartsWith("fail"), s => Fail(s.Text));
            Receive<Message>(message => Console.WriteLine("Received {0}-{1} in thread {2} ", message.Text,m_Counter++ ,Thread.CurrentThread.ManagedThreadId));

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

        protected override void PostStop()
        {
            base.PostStop();
            DisplayColoredMessage(ConsoleColor.DarkGreen, "MessagePrinter Stopped.");
        }

        private static void DisplayColoredMessage(ConsoleColor messageColor, string message)
        {
            ConsoleColor foregroundColor = Console.ForegroundColor;
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
            Console.ForegroundColor = foregroundColor;
        }

        private void Fail(string message)
        {
            if (message == "fail1")
                throw new Exception("Very Bad!!!");
            if (message == "fail2")
                throw new Exception("Good luck with it!");
            if (message == "fail")
                throw new Exception("Oh Well...");
        }
    }
}
