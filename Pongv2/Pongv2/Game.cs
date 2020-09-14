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
        private Vector size = new Vector(41, 11);
        private UInt32 rounds = 5;

        private Map map;
        private Ball ball;

        private Player player1;
        private Player player2;

        public Game()
        {
            map = new Map(size.y, size.x);
            ball = new Ball(size.x / 2, size.y / 2 + 1, map, false);

            player1 = new Player(2, size.y / 2 + 1, map);
            player2 = new Player(size.x - 3, size.y / 2 + 1, map);

            BuildMap();
        }

        private void BuildMap()
        {
            for (int x = 0; x < size.x; x++)
            {
                map[x, size.y - 1] = 2;
            }
        }

        public void Start()
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.SetWindowSize(size.x, size.y);
            Console.SetBufferSize(size.x, size.y);

            FrameDrawer.DrawFrame(map);
            ball.Start();
            while(true)
                Update();
        }

        private void Update()
        {
            player1.Update();
            player2.Update();

            CheckWindowSize();
        }

        private void CheckWindowSize()
        {
            if(Console.WindowWidth != size.x && Console.WindowHeight != size.y)
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

        private Vector dir;
        private Vector vel;
        private Vector moves;
        
        private double rate = 400;

        private Timer timer;

        private bool advMove;
        
        public Ball(int x, int y, Map map, bool advMove)
        {
            this.map = map;

            pos.x = x;
            pos.y = y;

            map[pos.x, pos.y] = 3;
            this.advMove = advMove;

            timer = new Timer(rate);
            timer.Enabled = true;
            timer.Elapsed += OnTimedEvent;
            
        }

        private void GetRandomMoves()
        {
            Random rnd = new Random();

            moves.x = rnd.Next(-4, 4);
            if (moves.x == 0)
                moves.x++;

            moves.y = 1;
            vel = moves;
        }

        private void GetRandomDir()
        {
            Random rnd = new Random();

            if (rnd.Next(2) == 0)
                dir.x = -1;
            else
                dir.x = 1;
            

            if (rnd.Next(2) == 0)
                dir.y = -1;
            else
                dir.y = 1;
            
        }

        private void Move()
        {
            map[pos.x, pos.y] = 0;
            CheckCollision();

            if (advMove)
            {
                if (moves.x > 0)
                {
                    moves.x--;
                    pos.x += dir.x;
                }
                else if (moves.y > 0)
                {
                    moves.y--;
                    pos.y += dir.y;
                }

                if (moves.y == 0)
                {
                    moves = vel;
                }
            }
            else
            {
                pos.Add(dir);
            }

            map[pos.x, pos.y] = 3;
            FrameDrawer.DrawFrame(map);
        }

        public void Start()
        {
            GetRandomDir();
            if(advMove)
                GetRandomMoves();
            timer.Start();
        }

        private void CheckCollision()
        {
            if (pos.x == 0 || pos.x == map.size.x - 1)
            {
                dir.x *= -1;
                if(advMove)
                    GetRandomMoves();
            }

            if (map[pos.x, pos.y - 1] == 2 && map[pos.x, pos.y + 1] == 2)
            {
                dir.y *= -1;
                if(advMove)
                    GetRandomMoves();
            }
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Move();
            timer.Interval = rate;
            timer.Start();
        }

    }

    class Player
    {
        private Map map;

        private Vector pos;
        private int dir;

        private double maxSpeed = 100;

        private Timer nextMove;

        public Player(int x, int y, Map map) 
        {
            this.map = map;

            pos.x = x;
            pos.y = y;

            map[pos.x, pos.y] = 4;

            nextMove = new Timer(maxSpeed);

            nextMove.Enabled = true;
            nextMove.AutoReset = true;
            nextMove.Elapsed += Move;          
        }
        
        private int Input()
        {
            Console.Beep(20000,1);
            if (Console.KeyAvailable)
            {
                Console.Beep(2000, 1);
                ConsoleKeyInfo key = Console.ReadKey(true);

                if(key.KeyChar == 'w')
                {
                    return 1;
                }

                if (key.KeyChar == 's')
                {
                    return -1;
                }
            }

            return 0;
        }

        public void Update()
        {
            dir = Input();
        }

        private void Move(object source, ElapsedEventArgs e)
        {
            if (pos.y != 0 && dir == 1)
            {
                map[pos.x, pos.y] = 0;
                pos.y--;
                map[pos.x, pos.y] = 4;
            }

            if (pos.y != map.size.y - 1 && dir == -1)
            {
                map[pos.x, pos.y] = 0;
                pos.y++;
                map[pos.x, pos.y] = 4;
            }

            FrameDrawer.DrawFrame(map);
        }
    }

    static class FrameDrawer
    {
        public static void DrawFrame(Map map)
        {
            Console.SetCursorPosition(0, 0);

            string n = "";

            for (int y = 0; y < map.size.y; y++)
            {
                for (int x = 0; x < map.size.x; x++)
                {
                    switch (map[x, y])
                    {
                        case 1: n += '|'; break;
                        case 2: n += '_'; break;
                        case 3: n += 'o'; break;
                        case 4: n += 'I'; break;

                        default: n += ' '; break;
                    }
                }
            }

            Console.Write(n);
        }
    }

}
