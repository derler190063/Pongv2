using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pong21
{
    class Server
    {
        private Game game;

        private string hostToClient;
        private string clientToHost;

        public Server(Game game)
        {
            this.game = game;
        }

        private void Write(string path, string text)
        {
            Random rnd = new Random();

            try
            {
                File.WriteAllText(path, text);
            }
            finally
            {
                System.Threading.Thread.Sleep(rnd.Next(10));
                Write(path, text);
            }
        }

        private string Read(string path)
        {
            Random rnd = new Random();
            string n = "";

            try
            {
                n = File.ReadAllText(path);
            }
            finally
            {
                System.Threading.Thread.Sleep(rnd.Next(10));
                n = Read(path);
            }

            return n;
        }
    }
}
