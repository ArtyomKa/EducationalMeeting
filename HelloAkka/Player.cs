using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HelloAkka
{
    class Player : ReceiveActor
    {
        private readonly int m_Id;
        public Player(int id)
        {
            m_Id = id;
            Receive<string>(s => s == "ping", (s)=>{
                Thread.Sleep(5000);
                Console.WriteLine("Player {0} {1}", m_Id,s);
                Sender.Tell("pong");
            });
            Receive<string>(s => s == "pong", (s) =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Player {0} {1}", m_Id, s);
                Sender.Tell("ping");
            });
            Receive<Begin>(message => message.SecondPlayer.Tell("ping"));
        }

    }
}
