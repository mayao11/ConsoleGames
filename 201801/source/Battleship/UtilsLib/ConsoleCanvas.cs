using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsLib
{
    public class ConsoleCanvas
    {
        int height;
        int width;
        char empty = ' ';//全角空格
        char[,] backBuffer;//备用buffer，用于存放变化之前的数组
        char[,] buffer;//用于存放当前要绘制的数组
        ConsoleColor[,] forecolor_buffer;//画面上字符颜色
        ConsoleColor[,] backcolor_buffer;//画面上背景颜色

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
            buffer = new char[height, width];
            backBuffer = new char[height, width];
            forecolor_buffer = new ConsoleColor[height, width];
            backcolor_buffer = new ConsoleColor[height, width];
            Console.CursorVisible = false;
            ClearBuffer();
        }
        /// <summary>
        /// 将画布内容清除
        /// </summary>
        public void ClearBuffer()
        {
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    buffer[i, j] = empty;
                    forecolor_buffer[i, j] = ConsoleColor.Gray;
                    backcolor_buffer[i, j] = ConsoleColor.Black;
                }
            }
        }
        /// <summary>
        /// 复制二维数组
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        void CopyArray2D(char[,] source, char[,] dest)
        {
            for (int i = 0; i < source.GetLength(0); ++i)
            {
                for (int j = 0; j < source.GetLength(1); ++j)
                {
                    dest[i, j] = source[i, j];
                }
            }
        }
        /// <summary>
        /// 将画布内容清除，无闪烁版本
        /// </summary>
        public void ClearBuffer_DoubleBuffer()
        {
            CopyArray2D(buffer, backBuffer);
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    buffer[i, j] = empty;
                    forecolor_buffer[i, j] = ConsoleColor.Gray;
                    backcolor_buffer[i, j] = ConsoleColor.Black;
                }
            }
        }

        public char[,] GetBuffer()
        {
            return buffer;
        }

        public ConsoleColor[,] GetForecolorBuffer()
        {
            return forecolor_buffer;
        }
        public ConsoleColor[,] GetBackcolorBuffer()
        {
            return backcolor_buffer;
        }
        /// <summary>
        /// 刷新控制台画面内容，无闪烁版本
        /// </summary>
        public void Refresh_DoubleBuffer()
        {
            for (int i = 0; i < height; i++)
            {
                // 这里的算法是去除每行最后面的空白
                int end = 0;
                for (int j = width - 1; j >= 0; --j)
                {
                    if (buffer[i, j] != empty || backBuffer[i, j] != empty)
                    {
                        end = j + 1;
                        break;
                    }
                }

                for (int j = 0; j < end; j++)
                {
                    if (buffer[i, j] != backBuffer[i, j])
                    {
                        Console.SetCursorPosition(j * 2, i);
                        ConsoleColor fc = forecolor_buffer[i, j];
                        ConsoleColor bc = backcolor_buffer[i, j];
                        Console.ForegroundColor = fc;
                        Console.BackgroundColor = bc;
                        Console.Write(buffer[i, j]);
                    }
                }
            }
        }
        /// <summary>
        /// 刷新控制台画面内容
        /// </summary>
        public void Refresh()
        {
            Console.Clear();
            for (int i = 0; i < height; ++i)
            {
                // 这里的算法是去除每行最后面的空白，空格多了，加上汉字会带来问题
                int end = 0;
                for (int j = width - 1; j >= 0; --j)
                {
                    if (buffer[i, j] != empty)
                    {
                        end = j + 1;
                        break;
                    }
                }
                for (int j = 0; j < end; ++j)
                {
                    ConsoleColor fc = forecolor_buffer[i, j];
                    ConsoleColor bc = backcolor_buffer[i, j];
                    Console.ForegroundColor = fc;
                    Console.BackgroundColor = bc;
                    if (buffer[i, j] < 127)
                    {
                        Console.Write(buffer[i, j] + " ");
                    }
                    else
                    {
                        Console.Write(buffer[i, j]);
                    }
                }
                if (end > 0)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}

