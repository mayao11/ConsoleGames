using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Tetris
{
    class MyInput
    {
        internal delegate void KeyboardHadler<EventArgs>(EventArgs e);
        private event KeyboardHadler<MyKeyboardEventArgs> KeyDown;
        private event KeyboardHadler<MyKeyboardEventArgs> KeyUp;
        private MyKeys oldKey = MyKeys.None;
        internal const int KEY_STATE = 0x8000;

        public MyInput()
        {

        }

        /// 调用系统自带的api来获取用户的键盘输入
        [DllImport("User32.dll")]
        protected static extern short GetAsyncKeyState(int vKey);

        private bool IsKeyDown(MyKeys k)
        {
            return 0 != (GetAsyncKeyState((int)k) & KEY_STATE);
        }

        private MyKeys GetCurretDownKey()
        {
            MyKeys vkey = MyKeys.None;
            foreach (var key in Enum.GetValues(typeof(MyKeys)))
            {
                if (IsKeyDown((MyKeys)key))
                {
                    vkey = (MyKeys)key;
                    break;
                }
            }
            return vkey;
        }

        private void OnKeyDown(MyKeyboardEventArgs e)
        {
            KeyboardHadler<MyKeyboardEventArgs> temp = KeyDown;
            if (temp != null) {
                temp.Invoke(e);
            }
        }

        private void OnKeyUp(MyKeyboardEventArgs e)
        {
            KeyboardHadler<MyKeyboardEventArgs> temp = KeyUp;
            if (temp != null) {
                temp.Invoke(e);
            }
        }

        public void AddKeyDownEvent(KeyboardHadler<MyKeyboardEventArgs> func)
        {
            KeyDown += func;
        }

        public void AddKeyUpEvent(KeyboardHadler<MyKeyboardEventArgs> func)
        {
            KeyUp += func;
        }

        public void KeyboardEventsHandler()
        {
            MyKeyboardEventArgs e;
            MyKeys vKeyDown = GetCurretDownKey();
            if (vKeyDown != MyKeys.None)
            {
                oldKey = vKeyDown;
                e = new MyKeyboardEventArgs(vKeyDown);
                OnKeyDown(e);
            }
            else if (oldKey != MyKeys.None && !IsKeyDown(oldKey)) {
                e = new MyKeyboardEventArgs(oldKey);
                OnKeyUp(e);
                oldKey = MyKeys.None;
            }
        }
    }
}
