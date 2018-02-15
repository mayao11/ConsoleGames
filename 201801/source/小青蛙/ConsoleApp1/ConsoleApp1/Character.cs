using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    public class Frog //可移动生物-青蛙
    {
        public int y = 19;
        public int x=0;
        public int turn = 0;//朝向

        public int resurrection = 3;
        public int hp = 92;
        public int level = 1;

        public int timecoin = 0;//金币数

        public bool jump = false;//是否跳跃
        public int starty;//初始跳跃坐标
        public int startx;//初始跳跃坐标
        public int airmove;
        public DateTime jumpstart;
        public DateTime jumpduring;

        public bool arm = false;//是否持有武器
        public bool attack = false;//是否攻击

        public Frog(int y, int x)
        {
            this.y = y;
            this.x = x;
        }

        public bool Be_Brick(int _x,int _y,Container container)
        {
            return container.br.ContainsKey(_y * 100 + _x) ? true : false;
        }
        public bool Be_Water(int _x, int _y, Container container)
        {
            return container.wa.ContainsKey(_y * 100 + _x) ? true : false;
        }
        public bool Be_Reporter(int _x, int _y, Container container)
        {
            return container.re.ContainsKey(_y * 100 + _x) ? true : false;
        }
        public bool Be_Glasses(int _x, int _y, Container container)
        {
            return container.gl.ContainsKey(_y * 100 + _x) ? true : false;
        }
        public bool Be_Gate(int _x, int _y, Container container)
        {
            return container.ga.ContainsKey(_y * 100 + _x) ? true : false;
        }
        public bool Be_Timecoin(int _x, int _y, Container container)
        {
            return container.ti.ContainsKey(_y * 100 + _x) ? true : false;
        }
    }
    public class Reporter
    {
        public int hp = 1;
        public int x;
        public int y;
        public int turn=0;

        public bool Be_Brick(int _x, int _y, Container container) 
        {
            return container.br.ContainsKey(_y * 100 + _x) ? true : false;
        }
        public bool Be_Water(int _x, int _y, Container container)
        {
            return container.wa.ContainsKey(_y * 100 + _x) ? true : false;
        }
        public bool Be_Reporter(int _x, int _y, Container container)
        {
            return container.re.ContainsKey(_y * 100 + _x) ? true : false;
        }
    } 
    public class Brick
    {
        public int x;
        public int y;
    }
    public class Wall 
    {
        public int x;
        public int y;       
    }
    public class Bullet
    {
        public int x;
        public int y;
        public int startx;
        public bool Be_Brick(int _x, int _y, Container container)
        {
            return container.br.ContainsKey(_y * 100 + _x) ? true : false;
        }
        public bool Be_Reporter(int _x, int _y, Container container)
        {
            return container.re.ContainsKey(_y * 100 + _x) ? true : false;
        }
    }
    public class Water
    {
        public int x;
        public int y;      
    }
    public class TimeCoin
    {
        public int level = 0;
        public bool move = false;

        public int x;
        public int y;
    }
    public class Glasses 
    {  
        public int x;
        public int y;
    }

    
}
