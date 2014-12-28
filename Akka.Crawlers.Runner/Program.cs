using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Crawlers.Workers;
using log4net.Config;
namespace Akka.Crawlers.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = Actor.ActorSystem.Create("crawlers");
            BasicConfigurator.Configure();
            var controller = system.ActorOf(Props.Create<Controller>(),"controller1");
            //var controller2 = system.ActorOf(Props.Create<Controller>(), "controller2");
            controller.Tell("http://www.bbc.co.uk/");
            //controller2.Tell("http://www.dailymail.co.uk/");

            Console.ReadLine();
        }
    }
}




