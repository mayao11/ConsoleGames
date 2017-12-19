using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace Tetris
{
    public abstract class MyGame
    {
        private int updateRate;
        private int FPS;
        private int lastTime;
        private int loopsCount;//用于计算FPS，记录1S内游戏循环执行的次数
        private bool isOver;

        //用于搜索一个指定的窗口
        [DllImport("User32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        //当前的窗口句柄
        private IntPtr hwnd = IntPtr.Zero;
        private MyInput keyboardInput;

        public MyGame()
        {
            isOver = false;
            hwnd = FindWindow(null, GetTitle());
            keyboardInput = new MyInput();
            keyboardInput.AddKeyDownEvent(GameKeyDown);
            keyboardInput.AddKeyUpEvent(GameKeyUp);
            lastTime = Environment.TickCount;
        }

        protected void SetTitle(string title)
        {
            Console.Title = title;
        }

        protected string GetTitle()
        {
            return Console.Title;
        }

        protected void SetUpdateRate(int updateRate)
        {
            this.updateRate = updateRate;
        }
        private void SetFPS()
        {
            int ticks = Environment.TickCount;
            loopsCount++;
            if (ticks - lastTime >= 1000)
            {
                FPS = loopsCount;
                loopsCount = 0;
            }
        }

        protected int GetFPS()
        {
            return FPS;
        }

        protected void SetCursorVisible(bool visible)
        {
            Console.CursorVisible = visible;
        }

        protected void SetIsOver(bool isOver)
        {
            this.isOver = isOver;
        }

        protected bool IsOver()
        {
            return isOver;
        }

        protected void Delay(int n)
        {
            Thread.Sleep(n);
        }

        private void Delay()
        {
            Delay(1);
        }

        private void GameInput()
        {
            GetKeyboardDevice().KeyboardEventsHandler();
        }

        internal MyInput GetKeyboardDevice()
        {
            return keyboardInput;
        }

        protected virtual void GameKeyDown(MyKeyboardEventArgs e) { }
        protected virtual void GameKeyUp(MyKeyboardEventArgs e) { }
        protected virtual void GameInit() {
            SetTitle("测试游戏框架！");
            //设置游戏画面刷新率 每毫秒一次  
            SetUpdateRate(30);
            //设置光标隐藏  
            SetCursorVisible(false);
            Console.WriteLine("游戏初始化成功!");
        }

        protected virtual void GameLoop() {
            if (loopsCount < 15)
            {
                Console.WriteLine(string.Format("  游戏运行中,第{0}帧,耗时{1}ms", loopsCount, Environment.TickCount - lastTime));
                lastTime = Environment.TickCount;
            }
            else
            {
                SetIsOver(true);
            }
        }

        protected virtual void GameExit() {
            Console.WriteLine("游戏结束！！按任意键退出！");
            Console.ReadLine();
        }

        public void Run()
        {
            GameInit();
            int startTime = 0;
            while (!isOver)
            {
                startTime = Environment.TickCount;
                SetFPS();
                GameInput();
                GameLoop();
                while (Environment.TickCount - startTime < updateRate)
                {
                    Delay();
                }
            }
            GameExit();
            Close();
        }
        private void Close()
        {
        }
    }
}
