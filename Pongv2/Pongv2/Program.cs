﻿using System;
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
        public byte[][] map;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Willkommen zu Pong BattleRoyale\n");


        }
    }
}
