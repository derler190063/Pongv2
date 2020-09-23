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
        private Vector size = new Vector(61, 17);
        private Map map;

        private Ball ball;

        private Player player1;
        private Player player2;

        private DateTime nextFrame;
        private const double FRAMERATE = 10;

        public void Setup()
        {
            map = new Map(size);
            ball = new Ball(map, new Vector(size.x / 2, size.y / 2));

            player1 = new Player(map, new Vector(1, size.y / 2), 'w', 's');
            player2 = new Player(map, new Vector(size.x - 2, size.y / 2), 'o', 'l');

            Console.CursorVisible = false;
            Console.SetWindowSize(size.x, size.y);

            nextFrame = DateTime.Now.AddMilliseconds(FRAMERATE);
            while (true)
            {
                Update();  
            }
        }

        private void Update()
        {
            ball.Update();

            player1.Update();
            //player2.Update();

            CheckFrame();
        }

        private void CheckFrame()
        {
            if (nextFrame - DateTime.Now <= TimeSpan.Zero)
            {
                nextFrame = DateTime.Now.AddMilliseconds(FRAMERATE);
                FrameDrawer.DrawFrame(map);
            }
                
        }

        private void CheckWindowSize()
        {
            if(Console.WindowHeight != size.y - 1|| Console.WindowWidth != size.x - 1)
            {
                Console.SetWindowSize(size.x, size.y);
                Console.SetBufferSize(size.x, size.y);
            }
        }
    }

    class Ball
    {
        private Map map;

        private Vector pos;
        private Vector vel;

        private double rate = 150;

        private DateTime nextMove;

        public Ball(Map map, Vector pos)
        {
            this.map = map;
            this.pos = pos;

            vel = GetRandomVel();

            map[pos] = 2;

            nextMove = DateTime.Now.AddMilliseconds(rate);
        }   

        public void Update()
        {
            if(nextMove - DateTime.Now <= TimeSpan.Zero)
            {
                nextMove = DateTime.Now.AddMilliseconds(rate);
                Move();
            }
            
        }

        private void CheckCollision()
        {
            bool touchingSides = false;

            if (pos.x + 1 == map.size.x || pos.x == 0)
            {
                vel.x *= -1;
                touchingSides = true;
            }

            if (pos.y + 1 == map.size.y || pos.y == 0)
            {
                vel.y *= -1;
                touchingSides = true;
            }

            if (touchingSides)
                return;

            if (map[pos.x + 1, pos.y] == 1 || map[pos.x - 1, pos.y] == 1) 
            {
                vel.x *= -1;
            }

            if (map[pos.x, pos.y + 1] == 1 || map[pos.x, pos.y - 1] == 1)
            {
                vel.y *= -1;
            }
        }

        private void Move()
        {
            CheckCollision();

            map[pos] = -1;

            pos.Add(vel);
            map[pos] = 2;

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

    class Player
    {
        private Map map;

        private Vector pos;
        private int dir = 0;

        private char up;
        private char down;
        
        public Player(Map map, Vector pos, char up, char down)
        {
            this.map = map;
            this.pos = pos;

            this.up = up;
            this.down = down;

            map[pos] = 1;
        }

        public void Update()
        {
            Input();
            Move();
        }

        private void Input()
        {        
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if(key.KeyChar == up && pos.y != 0)
                {
                    dir = -1;
                }

                if (key.KeyChar == down && pos.y != map.size.y - 1)
                {
                    dir = 1;
                }
            }
        }

        private void Move()
        {
            map[pos] = -1;
            pos.y += dir;
            map[pos] = 1;

            dir = 0;
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
                n += "\n";
            }
            Console.Write(n);
            Console.SetCursorPosition(0, 0);
        }

    }
}
