using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloAkkaRemote.Client
{
    class Program
    {
        public static void Main(string[ ] args)
        {
            
            if (args.Length > 0 && args[0] == "select")
            {
                ProgramSelectActor.Run();
            }
            else
            {
                ProgramCreateActor.Run();
            }

        }
    }
}
