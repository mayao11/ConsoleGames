using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public struct MyRect
    {
        /// <summary>
        /// 左上角列坐标
        /// </summary>
        private int x;
        /// <summary>
        /// 左上角行坐标
        /// </summary>
        private int y;
        /// <summary>
        /// 矩形宽度
        /// </summary>
        private int width;
        /// <summary>
        /// 矩形高度
        /// </summary>
        private int height;

        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }
        public int Y { get => y; set => y = value; }
        public int X { get => x; set => x = value; }

        public MyRect(int x, int y, int width, int height)
        {
            if (width <= 0 || height <= 0) {
                throw new ArgumentOutOfRangeException();
            }
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
        public MyRect(MyPoint p, int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            x = p.X;
            y = p.Y;
            this.width = width;
            this.height = height;
        }
        public MyRect(MyPoint p1, MyPoint p2)
        {
            if (p1.X == p2.X || p1.Y == p2.Y)
                throw new ArgumentOutOfRangeException();
            if (p1.X < p2.X)
            {
                x = p1.X;
                width = p2.X - p1.X;
            }
            else
            {
                x = p2.X;
                width = p1.X - p2.X;
            }
            if (p1.Y < p2.Y)
            {
                y = p1.Y;
                height = p2.Y - p1.Y;
            }
            else
            {
                y = p2.Y;
                height = p1.Y - p2.Y;
            }
        }
        public MyRect(int x, int y, MySize s)
        {
            this.x = x;
            this.y = y;
            width = s.Width;
            height = s.Height;
        }
        public MyRect(MyPoint p, MySize s)
        {
            x = p.X;
            y = p.Y;
            width = s.Width;
            height = s.Height;
        }

        public MyRect GetMyRect()
        {
            return new MyRect(x, y, width, height);
        }

        public void AdjustRect(ref MyRect rect)
        {
            int maxX = Console.WindowWidth >> 1;
            int maxY = Console.WindowHeight;
            if (rect.X < 0)
                rect.X = 0;
            else if (rect.X > maxX)
                rect.X = maxX - 1;
            if (rect.Y < 0)
                rect.Y = 0;
            else if (rect.Y > maxY)
                rect.Y = maxY - 1;

            rect.X += rect.X;
        }

        public override string ToString()
        {
            return string.Format("[x:{0}, y:{1}, width:{2}, heigth:{3}]",x ,y, width, height);
        }
    }
}
