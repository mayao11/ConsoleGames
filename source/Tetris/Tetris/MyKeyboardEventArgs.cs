using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public sealed class MyKeyboardEventArgs : EventArgs
    {
        private MyKeys key;
        public MyKeyboardEventArgs(MyKeys key) : base()
        {
            this.key = key;
        }

        public MyKeys Key
        {
            get { return key; }
        }
    }
}
