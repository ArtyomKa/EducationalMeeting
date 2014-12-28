using Akka.Actor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akka.Crawlers.Workers
{
    class Persister : ReceiveActor
    {
        private readonly string m_FileName;
        public Persister(string filename)
        {
            m_FileName = filename;
            DeleteFileIfExists();
            Receive<Persist>(message =>
                {
                    WriteMesssgeToFile(message);
                });
        }

        private void WriteMesssgeToFile(Persist message)
        {
            using(StreamWriter streamWriter = new StreamWriter(m_FileName,true))
            {
                foreach(string imageLink in message.ImgLinks)
                {
                    streamWriter.WriteLine("{0}, {1}", message.PageUrl, imageLink);
                }
            }
        }

        private void DeleteFileIfExists()
        {            
            if(File.Exists(m_FileName))
            {
                File.Delete(m_FileName); 
            }
        }


    }
}
