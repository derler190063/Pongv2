using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pongv2
{
    struct Vector
    {
        public int x, y;

        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        
        public void Add(Vector vector)
        {
            x += vector.x;
            y += vector.y;
        }
    }

    class Map
    {
        private byte[][] map;
        public Map(int y, int x)
        {
            map = new byte[y][];

            for(int i = 0; i < y; i++)
            {
                map[i] = new byte[x];
                for(int j = 0; j < x; j++)
                {
                    map[i][j] = 0;
                }
            }
        }

        public byte this[int x ,int y]
        {
            get { return map[y][x]; }
            set { map[y][x] = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Willkommen zu Pong BattleRoyale\n");
            Game game = new Game();
            game.Start();

            while (true) ;
            
        }
    }
}
