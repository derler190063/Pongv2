using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Pong21
{
    class Game
    {
        private Vector size = new Vector(21, 9);
        private Map map;

        private void Setup()
        {
            map = new Map(size);

            Console.CursorVisible = false;

            Console.SetWindowSize(size.x, size.y);
            Console.SetBufferSize(size.x, size.y);
        }
    }

    class Ball
    {
        private Map map;

        private Vector pos;
        private Vector vel;

        private double rate = 120;
        private Timer timer;

        public Ball(Vector pos, Map map)
        {
            this.map = map;
            this.pos = pos;

            timer = new Timer(rate);
            timer.Enabled = true;
            timer.Elapsed += Move;

            vel = GetRandomVel();
        }

        private void Move(Object o, ElapsedEventArgs e)
        {
            bool isValidMove = false;



            if(isValidMove)
                pos.Add(vel);
        }

        private Vector GetRandomVel()
        {
            Vector v = new Vector();
            Random rnd = new Random();

            if (rnd.Next(2) == 0)
                v.x = -1;
            else
                v.x = 1;

            if (rnd.Next(2) == 0)
                v.y = -1;
            else
                v.y = 1;

            return v;
        }
    }

    static class FrameDrawer
    {
        public static void DrawFrame(Map map)
        {
            Console.SetCursorPosition(0, 0);

            string n = "";
            
            for(int y = 0; y < map.size.y; y++)
            {
                for(int x = 0; x < map.size.x; x++)
                {
                    switch (map[x, y])
                    {
                        case 0: n += '|'; break;
                        case 1: n += 'I'; break;
                        case 2: n += 'o'; break;
                        default: n += ' '; break;
                    }
                }
            }
            Console.Write(n);
        }
    }
}
