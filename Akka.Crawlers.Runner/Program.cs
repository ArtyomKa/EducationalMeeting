using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Crawlers.Workers;
namespace Akka.Crawlers.Runner
{
    class Program
    {
        static void Main(string[] args)
        {

            var controller = Actor.ActorSystem.Create("crawlers").ActorOf(Props.Create<Controller>(),"controller");
            controller.Tell("http://www.bbc.co.uk/");

            Console.ReadLine();
        }
    }
}




