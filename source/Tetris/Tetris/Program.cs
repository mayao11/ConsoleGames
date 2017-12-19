using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            MyTetrisGame newGame = new MyTetrisGame();
            newGame.Run();
        }
    }
}
