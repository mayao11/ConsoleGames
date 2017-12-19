using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class MyPoint
    {
        private int x;
        private int y;

        public MyPoint(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public MyPoint() {
            x = 0;
            y = 0;
        }

        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

        public static bool operator ==(MyPoint p1, MyPoint p2) {
            return (p1.X == p2.X) && (p1.Y == p2.Y);
        }

        public static bool operator !=(MyPoint p1, MyPoint p2) {
            return (p1.X != p2.X) || (p1.Y != p2.Y);
        }

        public override bool Equals(object obj)
        {
            return this == (MyPoint)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static MyPoint operator -(MyPoint p1, MyPoint p2) {
            return new MyPoint(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static MyPoint operator +(MyPoint p1, MyPoint p2)
        {
            return new MyPoint(p1.X + p2.X, p1.Y + p2.Y);
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", x, y);
        }
    }
}
