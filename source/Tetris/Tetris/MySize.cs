using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public struct MySize
    {
        private int width;
        private int height;

        public MySize(int width, int height) {
            if (width <= 0 || height <= 0) {
                throw new ArgumentOutOfRangeException("输入的尺寸不合法！！");
            }
            this.width = width;
            this.height = height;
        }

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
    }
}
