using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAkka
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("Hello");
            var playerOne = system.ActorOf(Props.Create<Player>(()=>new Player(1)));
            var playerTwo = system.ActorOf(Props.Create<Player>(() => new Player(2)));
            playerOne.Tell(new Begin { SecondPlayer = playerTwo});


            
            Console.ReadLine();
        }
    }
}
