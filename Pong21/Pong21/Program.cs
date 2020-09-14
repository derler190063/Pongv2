using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong21
{
    struct Vector
    {
        public int x,y;

        public Vector(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Add(Vector vector)
        {
            x = vector.x;
            y = vector.y;
        }
    }

    class Map
    {
        private int[][] map;
        public Vector size;

        public Map(int x, int y)
        {
            map = new int[y][];

            for (int i = 0; i < y; i++)
            {
                map[i] = new int[x];
                for (int j = 0; j < x; j++)
                {
                    map[i][j] = 0;
                }
            }
            size.x = x;
            size.y = y;
        }

        public Map(Vector size)
        {
            map = new int[size.y][];

            for (int i = 0; i < size.y; i++)
            {
                map[i] = new int[size.x];
                for (int j = 0; j < size.x; j++)
                {
                    map[i][j] = 0;
                }
            }
            this.size.x = size.x;
            this.size.y = size.y;
        }

        public int this[int x, int y]
        {
            get { return map[y][x]; }
            set { map[y][x] = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
