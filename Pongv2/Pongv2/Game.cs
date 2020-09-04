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
            ball = new Ball(size.x / 2, size.y / 2, map, this);

            player1 = new Player(2, size.y / 2, map, this);

            BuildMap();
        }

        private void BuildMap()
        {

            for(int y = 0; y < size.y; y++)
            {
                map[0, y] = 2;
                map[size.x - 1, y] = 2;
            }

            for (int x = 0; x < size.y; x++)
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

            DrawMap();
            while(true)
                Update();
        }

        private void Update()
        {
 
        }

        public void DrawMap()
        {
            string n = "";

            for(int y = 0; y < size.y; y++)
            {
                for(int x = 0; x < size.x; x++)
                {
                    switch (map[x, y])
                    {
                        case 1:
                            n += '|';
                            break;
                        case 3:
                            n += 'o';
                            break;
                        case 4:
                            n += 'I';
                            break;
                        default:
                            n += ' ';
                            break;
                    }
                }
            }

            Console.WriteLine(n);
        }
    }

    class Ball
    {
        private Map map;
        private Game game;

        private Vector pos;

        private Vector dir;
        private Vector vel;
        private Vector moves;
        
        private double rate = 100;

        private Timer timer;
        
        public Ball(int x, int y, Map map, Game game)
        {
            this.map = map;
            this.game = game;

            pos.x = x;
            pos.y = y;

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
            {
                dir.x = -1;
            }
            else
            {
                dir.x = 1;
            }

            if (rnd.Next(2) == 0)
            {
                dir.y = -1;
            }
            else
            {
                dir.y = 1;
            }
        }

        private void Move()
        {
            if(moves.x > 0)
            {
                moves.x--;
                pos.x += dir.x;
            }
            else if(moves.y > 0)
            {
                moves.y--;
                pos.y += dir.y;
            }
            
            if(moves.y == 0)
            {
                moves = vel;
            }

            map[pos.x, pos.y] = 3;
            game.DrawMap();
        }

        public void Start()
        {
            GetRandomDir();
            GetRandomMoves();
            timer.Start();
        }

        private void CheckCollision()
        {
            if (map[pos.x, pos.y] == 2 && map[pos.x, pos.y] == 2)
            {
                dir.x *= -1;
                GetRandomMoves();
            }

            if (map[pos.x, pos.y] == 3 && map[pos.x, pos.y] == 3)
            {
                dir.y *= -1;
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
        private Game game;

        private Vector pos;
        private SByte dir;

        private double maxSpeed = 100;

        private Timer nextMove;

        public Player(int x, int y, Map map, Game game) 
        {
            this.map = map;
            this.game = game;

            pos.x = x;
            pos.y = y;

            nextMove = new Timer(maxSpeed);

            nextMove.Enabled = true;
            nextMove.AutoReset = true;
            nextMove.Elapsed += Move;          
        }
        
        private SByte Input()
        {
            if (Console.KeyAvailable)
            {
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

        private void Move(object source, ElapsedEventArgs e)
        {
            if (map[pos.x, pos.y - 1] != 2 && dir == 1)
            {
                map[pos.x, pos.y] = 0;
                pos.y--;
                map[pos.x, pos.y] = 4;
            }

            if (map[pos.x, pos.y + 1] != 2 && dir == -1)
            {
                map[pos.x, pos.y] = 0;
                pos.y++;
                map[pos.x, pos.y] = 4;
            }

            game.DrawMap();
        }
    }
}
