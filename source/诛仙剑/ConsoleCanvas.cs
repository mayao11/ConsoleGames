using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class ConsoleCanvas
    {
        int height;
        int width;
        char empty = ' ';
        char[] buffer;

        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        public ConsoleCanvas(int w, int h, char _empty = ' ')
        {
            width = w;
            height = h;
            empty = _empty;
            buffer = new char[width * height];
        }

        public void ClearBuffer()
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    buffer[i * width + j] = empty;
                }
            }
        }

        public void ClearScreen()
        {
            for (int i = 0; i < height - 1; ++i)
            {
                Console.WriteLine();
            }
        }

        public char[] GetBuffer()
        {
            return buffer;
        }

        public void Refresh()
        {
            Console.Write(new string(buffer));
        }
    }
}
