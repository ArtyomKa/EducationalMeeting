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
            BasicConfigurator.Configure();
            var controller = Actor.ActorSystem.Create("crawlers").ActorOf(Props.Create<Controller>(),"controller");
            controller.Tell("http://www.bbc.co.uk/");

            Console.ReadLine();
        }
    }
}




