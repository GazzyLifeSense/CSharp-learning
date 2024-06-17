using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06_SnakeGame.Struct
{
    internal struct Position
    {
        public int x, y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static Boolean operator ==(Position p1, Position p2)
        {
            return p1.x == p2.x && p1.y == p2.y;
        }
        public static Boolean operator !=(Position p1, Position p2)
        {
            return p1.x != p2.x || p1.y != p2.y;
        }

    }
}
