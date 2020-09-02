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

        private Map _map = new Map();
        private Ball ball;

        private void BuildMap()
        {
            for(int y = 0; y < size.y; y++)
            {
                _map.map[y] = new byte[size.x];
                for(int x = 0; x < size.x; x++)
                {
                    _map.map[y][x] = 0;
                }
            }

            for(int y = 0; y < size.y; y++)
            {
                _map.map[y][size.x / 2] = 1;
            }

            for(int y = 0; y < size.y; y++)
            {
                _map.map[y][0] = 2;
                _map.map[y][size.x - 1] = 2;
            }

            for (int x = 0; x < size.y; x++)
            {
                _map.map[0][x] = 2;
                _map.map[size.y - 1][x] = 2;
            }
        }

        public void Start()
        {
            BuildMap();
            ball = new Ball(size.x / 2, size.y / 2, _map);
        }
    }

    class Ball
    {
        private Map _map;

        private Vector pos;

        private Vector dir;
        private Vector vel;
        private Vector moves;

        
        private double rate = 100;

        private Timer timer;
        
        public Ball(int x, int y, Map map)
        {
            _map = map;

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
        }

        public void Start()
        {
            GetRandomMoves();
            timer.Start();
        }

        private void CheckCollision()
        {
            if(_map.map[pos.y][pos.x + 1] == 2 && _map.map[pos.y][pos.x - 1] == 2)
            {
                dir.x *= -1;
                GetRandomMoves();
            }

            if (_map.map[pos.y + 1][pos.x] == 3 && _map.map[pos.y - 1][pos.x] == 3)
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
}
