using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public sealed class MyDraw
    {
        private MySymbol symbol;
        private ConsoleColor backColor;

        public MySymbol Symbol { get => symbol; set => symbol = value; }
        public ConsoleColor BackColor { get => backColor; set => backColor = value; }

        public MyDraw()
        {
            Symbol = MySymbol.RECT_EMPTY;
            BackColor = ConsoleColor.Black;
        }
        public MyDraw(MySymbol symbol)
        {
            Symbol = symbol;
        }
        public MyDraw(ConsoleColor color)
        {
            BackColor = color;
        }
        public MyDraw(MySymbol symbol, ConsoleColor color)
        {
            Symbol = symbol;
            BackColor = color;
        }

        /// <summary>
        /// 按照给定的颜色和在给定矩形范围内绘制指定字符串
        /// </summary>
        /// <param name="text">要绘制的字符串</param>
        /// <param name="rect">绘制的范围矩形</param>
        /// <param name="color">颜色</param>
        public void DrawText(string text, MyRect rect, ConsoleColor color)
        {
            rect.AdjustRect(ref rect);
            if (text == "")
            {
                return;
            }

            //如果选择的颜色和背景色相容，取反色
            Console.BackgroundColor = BackColor;
            Console.ForegroundColor = BackColor == color ? (ConsoleColor)(15 - (int)BackColor) : color;
            Console.SetCursorPosition(rect.X, rect.Y);

            int charLen = (rect.Width * rect.Height) << 1;
            int textLen = MyText.GetLength(text);

            text = MyText.CutText(text, textLen > charLen ? charLen : textLen);
            text = MyText.BrText(text, rect.Width);

            string[] texts = text.Split(Environment.NewLine.ToCharArray());
            int i = 0;
            //打印字符串，每次指定光标的位置到矩形区域的下一行，因为矩形区域不一定从0，0开始，所以不能用WriteLine
            foreach (string s in texts)
            {
                if (s != "")
                {
                    Console.SetCursorPosition(rect.X, rect.Y + i);
                    Console.Write(s);
                    i++;
                }
            }
            Console.SetCursorPosition(0, 0);
        }

        ///其他绘制方法的重载
        public void DrawText(string text, Int32 x, Int32 y, Int32 width, Int32 height, ConsoleColor color)
        {
            DrawText(text, new MyRect(x, y, width, height), color);
        }
        public void DrawText(string text, MyPoint p, Int32 width, Int32 height, ConsoleColor color)
        {
            DrawText(text, new MyRect(p, width, height), color);
        }
        public void DrawText(string text, MyPoint p, MyPoint p2, ConsoleColor color)
        {
            DrawText(text, new MyRect(p, p2), color);
        }
        public void DrawText(string text, int x, int y, MySize s, ConsoleColor color)
        {
            DrawText(text, new MyRect(x, y, s), color);
        }
        public void DrawText(string text, MyPoint p, MySize s, ConsoleColor color)
        {
            DrawText(text, new MyRect(p, s), color);
        }
        public void DrawText(string text, int x, int y, ConsoleColor color)
        {
            if (text == "")
                return;
            Console.BackgroundColor = BackColor;
            Console.ForegroundColor = BackColor == color ? (ConsoleColor)(15 - (int)BackColor) : color;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
            Console.SetCursorPosition(0, 0);
        }
        public void DrawText(string text, MyPoint p, ConsoleColor color)
        {
            DrawText(text, p.X, p.Y, color);
        }

        /// <summary>
        /// 限定显示宽度绘制字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width">显示宽度，几行</param>
        /// <param name="color"></param>
        public void DrawText(string text, int x, int y, int width, ConsoleColor color)
        {
            if (text == "")
                return;
            Console.BackgroundColor = BackColor;
            Console.ForegroundColor = BackColor == color ? (ConsoleColor)(15 - (int)BackColor) : color;
            Console.SetCursorPosition(x, y);
            text = MyText.BrText(text, width);
            string[] texts = text.Split(Environment.NewLine.ToCharArray());
            int i = 0;
            foreach (string s in texts)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(s);
                i++;
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 按照指定颜色在指定区域绘制矩形
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        public void DrawRect(MyRect rect, ConsoleColor color)
        {
            rect.AdjustRect(ref rect);
            int x = rect.X;
            int y = rect.Y;
            int width = rect.Width;
            int height = rect.Height;

            if (Symbol == MySymbol.DEFAULT)
            {
                Console.BackgroundColor = color;
            }
            else
            {
                Console.BackgroundColor = BackColor;
                Console.ForegroundColor = BackColor == color ? (ConsoleColor)(15 - (int)color) : color;
            }

            string charSymbol = MySymbolHelper.GetStringFromSymbol(Symbol);
            //绘制矩形列  
            for (int i = 0; i < width; i++)
            {
                int ix = x + (i << 1);
                if (ix >= Console.WindowWidth)
                    ix = Console.WindowWidth - 1;

                Console.SetCursorPosition(ix, y);
                Console.Write(charSymbol);
                Console.SetCursorPosition(ix, y + height - 1);
                Console.Write(charSymbol);
            }

            //绘制矩形行  
            for (int i = 0; i < height; i++)
            {
                int ix = x + (width << 1) - 2;
                if (ix >= Console.WindowWidth)
                    ix = Console.WindowWidth - 1;

                Console.SetCursorPosition(x, y + i);
                Console.Write(charSymbol);
                Console.SetCursorPosition(ix, y + i);
                Console.Write(charSymbol);
            }

            Console.SetCursorPosition(0, 0);
        }
        public void DrawRect(int x, int y, int width, int height, ConsoleColor color)
        {
            DrawRect(new MyRect(x, y, width, height), color);
        }
        public void DrawRect(MyPoint p, int width, int height, ConsoleColor color)
        {
            DrawRect(new MyRect(p, width, height), color);
        }
        public void DrawRect(MyPoint p, MyPoint p2, ConsoleColor color)
        {
            DrawRect(new MyRect(p, p2), color);
        }
        public void DrawRect(int x, int y, MySize s, ConsoleColor color)
        {
            DrawRect(new MyRect(x, y, s), color);
        }
        public void DrawRect(MyPoint p, MySize s, ConsoleColor color)
        {
            DrawRect(new MyRect(p, s), color);
        }

        /// <summary>
        /// 用默认的特殊符号填充一个矩形
        /// </summary>
        /// <param name="rect">指定的矩形区域</param>
        /// <param name="color">指定的颜色</param>
        public void FillRect(MyRect rect, ConsoleColor color)
        {
            rect.AdjustRect(ref rect);
            int x = rect.X;
            int y = rect.Y;
            int width = rect.Width;
            int height = rect.Height;

            if (Symbol == MySymbol.DEFAULT)
                Console.BackgroundColor = color;
            else
            {
                Console.BackgroundColor = BackColor;
                Console.ForegroundColor = BackColor == color ? (ConsoleColor)(15 - (int)color) : color;
            }

            string charSymbol = MySymbolHelper.GetStringFromSymbol(Symbol);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < width; i++)
                sb.Append(charSymbol);
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(sb.ToString());
            }
            Console.SetCursorPosition(0, 0);
        }
        public void FillRect(int x, int y, int width, int height, ConsoleColor color)
        {
            FillRect(new MyRect(x, y, width, height), color);
        }
        public void FillRect(MyPoint p, int width, int height, ConsoleColor color)
        {
            FillRect(new MyRect(p, width, height), color);
        }
        public void FillRect(MyPoint p, MyPoint p2, ConsoleColor color)
        {
            FillRect(new MyRect(p, p2), color);
        }
        public void FillRect(MyPoint p, MySize s, ConsoleColor color)
        {
            FillRect(new MyRect(p, s), color);
        }
        public void FillRect(int x, int y, MySize s, ConsoleColor color)
        {
            FillRect(new MyRect(x, y, s), color);
        }

        /// <summary>
        /// 在指定的位置绘制矩阵，此处为俄罗斯方块的地图，1显示方框，0不显示方框
        /// </summary>
        /// <param name="maxtrix"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void DrawMatrix(int[,] maxtrix, int pointX, int pointY)
        {
            int cursorX = pointX;
            int cursorY = pointY;
            Console.BackgroundColor = BackColor;
            Console.SetCursorPosition(pointX, pointY);
            string charSymbol = MySymbolHelper.GetStringFromSymbol(MySymbol.RECT_SOLID);
            string emptySymbol = MySymbolHelper.GetStringFromSymbol(MySymbol.DEFAULT);
            for (int y = 4; y < maxtrix.GetLength(1); y++)
            {
                for (int x = 0; x < maxtrix.GetLength(0); x++)
                {
                    if (maxtrix[x, y] != -1)
                    {
                        Console.ForegroundColor = (ConsoleColor)maxtrix[x, y];
                        Console.Write(charSymbol);
                    }
                    else
                    {
                        Console.Write(emptySymbol);

                    }
                    cursorY += 2;
                    Console.SetCursorPosition(pointX + cursorY, pointY + cursorX);
                }
                cursorX++;
                cursorY = 0;
                Console.SetCursorPosition(pointX + cursorY, pointY + cursorX);
            }
            Console.SetCursorPosition(0, 0);
        }

        public void DrawMatrix(MyMatrix matrix, int x, int y)
        {
            DrawMatrix(matrix.Matrix, x, y);
        }

        public void DrawTetrisBlock(TetrisBlock tetrisBlock)
        {
            Console.ForegroundColor = tetrisBlock.Color;
            string charSymbol = MySymbolHelper.GetStringFromSymbol(MySymbol.RECT_SOLID);
            foreach (MyPoint p in tetrisBlock.Blocks)
            {
                if (p.Y >= 0)
                {
                    Console.SetCursorPosition(p.X * 2, p.Y);
                    Console.Write(charSymbol);
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void EraserTetrisBlock(TetrisBlock tetrisBlock)
        {
            string emptySymbol = MySymbolHelper.GetStringFromSymbol(MySymbol.DEFAULT);
            foreach (MyPoint p in tetrisBlock.Blocks)
            {
                if (p.Y >= 0)
                {
                    Console.SetCursorPosition(p.X * 2, p.Y);
                    Console.Write(emptySymbol);
                }
            }
            Console.SetCursorPosition(0, 0);
        }
    }
}
