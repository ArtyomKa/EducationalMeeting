using Akka.Actor;
using HelloAkka.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAkka.FaultTolerance
{
    class Forwarder : ReceiveActor
    {
        ActorRef m_Printer;


        public Forwarder()
        {
            Receive<string>(s => m_Printer.Tell(new Message(s)));
        }

        protected override void PreStart()
        {
            base.PreStart();
            m_Printer = Context.ActorOf(Props.Create<MessagePrinter>(), "MessagePrinter");
            Console.WriteLine("Starting Forwarder");
        }
        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
            Console.WriteLine("Restarting Forwarder.");
        }
        protected override void PostRestart(Exception reason)
        {
            base.PostRestart(reason);
            Console.WriteLine("Forwarder Restarted.");
        }


    }
}
