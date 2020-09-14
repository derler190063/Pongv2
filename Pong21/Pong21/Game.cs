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

        private Ball ball;


        public void Setup()
        {
            map = new Map(size);
            ball = new Ball(map);

            Console.CursorVisible = false;

            Console.SetWindowSize(size.x, size.y);
            Console.SetBufferSize(size.x, size.y);

            ball.Start();
            FrameDrawer.DrawFrame(map);

            while (true) ;
        }
    }

    class Ball
    {
        private Map map;

        private Vector pos;
        private Vector vel;

        private double rate = 100;
        private Timer timer;

        public Ball(Map map)
        {
            this.map = map;

            pos.x = map.size.x / 2;
            pos.y = map.size.y / 2;

            vel = GetRandomVel();

            map[pos] = 2;
        }

        public void Start()
        {
            SetTimer();
        }

        private void Move(Object o, ElapsedEventArgs e)
        {
            if(pos.x + 1 == map.size.x - 1 || pos.x - 1 == 0)
            {
                vel.x *= -1;
            }

            if (pos.y + 1 == map.size.y - 1 || pos.y - 1 == 0)
            {
                vel.y *= -1;
            }

            map[pos] = -1;

            pos.Add(vel);
            map[pos] = 2;

            FrameDrawer.DrawFrame(map);
            SetTimer();
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

        private void SetTimer()
        {
            timer = new Timer(rate);
            timer.Enabled = true;
            timer.Elapsed += Move;

            timer.Start();
        }
    }

    static class FrameDrawer
    {
        public static void DrawFrame(Map map)
        {
            Console.SetCursorPosition(0, 0);
            Console.Beep(1000, 500);
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
