using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Pongv2
{
    class Game
    {
        private Vector size = new Vector(41,11);
        private UInt32 rounds = 5;

        private byte[][] map;
        private Ball ball;

        private void BuildMap()
        {
            for(int y = 0; y < size.y; y++)
            {
                map[y] = new byte[size.x];
                for(int x = 0; x < size.x; x++)
                {
                    map[y][x] = 0;
                }
            }

            for(int y = 0; y < size.y; y++)
            {
                map[y][size.x / 2] = 1;
            }
        }

        public void Start()
        {
            BuildMap();
            ball = new Ball(size.x / 2, size.y / 2);
        }
    }

    class Ball
    {
        private Vector pos;
        private double speed = 100;

        Timer timer;
        
        public Ball(int x, int y)
        {
            pos.x = x;
            pos.y = y;

            timer.Enabled = true;
        }

        
    }
}
