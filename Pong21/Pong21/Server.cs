using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
