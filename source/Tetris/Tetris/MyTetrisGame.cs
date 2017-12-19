using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace Tetris
{
    class MyTetrisGame : MyGame
    {
        private int loops;
        private bool bkeyDown = false;
        private MyDraw myDraw;
        private MyMap map;
        private MyScore score;
        private TetrisBlock block;
        private TetrisBlock nextBlock;

        public bool BkeyDown { get => bkeyDown; set => bkeyDown = value; }

        protected override void GameInit()
        {
            SetTitle("俄罗斯方块");
            //设置游戏画面刷新率 每毫秒一次  
            SetUpdateRate(50);
            //设置光标隐藏  
            SetCursorVisible(false);     
            Console.SetWindowSize(48, 20);
            Console.CursorVisible = false;
            myDraw = new MyDraw(MySymbol.RECT_SOLID, ConsoleColor.Black);
            map = new MyMap();
            score = new MyScore(myDraw);
            myDraw.DrawMatrix(map.Map, 0, 0);
            block = TetrisBlock.GetRandom();
            nextBlock = TetrisBlock.GetRandomNext();
            myDraw.DrawTetrisBlock(block);
            myDraw.DrawText("下一个", 28, 9, ConsoleColor.White);
            myDraw.DrawTetrisBlock(nextBlock);
            score.DrawScore();
        }

        protected override void GameLoop()
        {
            //base.GameLoop();
            loops++;
            if (loops == 20)
            {
                loops = 0;
                if (map.IsDownTouch(block))
                {
                    map.AddBlocks(block);
                    int line = map.FullCheck();
                    if (line > 0)
                    {
                        score.AddScore(line);
                    }
                    if (map.CheckOver())
                    {
                        SetIsOver(true);
                    }
                    myDraw.DrawMatrix(map.Map, 0, 0);
                    myDraw.EraserTetrisBlock(nextBlock);
                    block = TetrisBlock.GetBlocks(nextBlock.Type);
                    nextBlock = TetrisBlock.GetRandomNext();
                    myDraw.DrawTetrisBlock(nextBlock);
                    return;
                }
                myDraw.EraserTetrisBlock(block);
                block.Move(new MyPoint(0, 1));
                myDraw.DrawTetrisBlock(block);
            }
            score.DrawScore();
        }

        protected override void GameExit()
        {
            map = new MyMap();
            string str = string.Format("游戏结束，你的得分为{0}", score.Score);
            myDraw.DrawText(str, 0, 22, ConsoleColor.White);
            Console.SetCursorPosition(0, 23);
            Console.CursorVisible = true;
            Console.ReadLine();
        }

        protected override void GameKeyDown(MyKeyboardEventArgs e)
        {
            base.GameKeyDown(e);
            if (BkeyDown)
                return;
            if (e.Key == MyKeys.Down)
            {
                if (map.IsDownTouch(block))
                {
                    map.AddBlocks(block);
                    int line = map.FullCheck();
                    if (line > 0)
                    {
                        score.AddScore(line);
                    }
                    if (map.CheckOver())
                    {
                        SetIsOver(true);
                    }
                    myDraw.DrawMatrix(map.Map, 0, 0);
                    myDraw.EraserTetrisBlock(nextBlock);
                    block = TetrisBlock.GetBlocks(nextBlock.Type);
                    nextBlock = TetrisBlock.GetRandomNext();
                    myDraw.DrawTetrisBlock(nextBlock);
                    return;
                }
                myDraw.EraserTetrisBlock(block);
                block.Move(new MyPoint(0, 1));
                myDraw.DrawTetrisBlock(block);
            }
            if (e.Key == MyKeys.Up)
            {
                BkeyDown = true;
                TetrisBlock temp = TetrisBlock.RotateBlocks(block.Type, block.Origin);
                if (map.CanNotRotate(temp))
                    return;
                myDraw.EraserTetrisBlock(block);
                block = temp;
                myDraw.DrawTetrisBlock(block);
            }
            if (e.Key == MyKeys.Left)
            {
                BkeyDown = true;
                if (map.IsLeftTouch(block))
                {
                    return;
                }
                myDraw.EraserTetrisBlock(block);
                block.Move(new MyPoint(-1, 0));
                myDraw.DrawTetrisBlock(block);
            }
            if (e.Key == MyKeys.Right)
            {
                BkeyDown = true;
                if (map.IsRightTouch(block))
                {
                    return;
                }
                myDraw.EraserTetrisBlock(block);
                block.Move(new MyPoint(1, 0));
                myDraw.DrawTetrisBlock(block);
            }
            if (e.Key == MyKeys.Escape)
            {
                SetIsOver(true);
            }
        }

        protected override void GameKeyUp(MyKeyboardEventArgs e)
        {
            base.GameKeyUp(e);
            if (BkeyDown)
            {
                BkeyDown = false;
            }
        }
    }
}
