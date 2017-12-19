using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tower
{
    public enum ConsoleKey2
    {
        Yellow,
        red,
        Blue,
    }

    public enum Dict
    {
        Up,
        Down,
    }

}


namespace Tower
{



    public class Container//容器  封装怪物 装备 npc 地图
    {
        public Dictionary<int, Monster> ms = new Dictionary<int, Monster>();//怪物
        public Dictionary<int, Npc> npc = new Dictionary<int, Npc>();//Npc
        public Dictionary<int, Prop> eq = new Dictionary<int, Prop>();//道具
        public Map map1s;
        public Dictionary<int, Stairs> stair = new Dictionary<int, Stairs>();//楼梯
        public Dictionary<int, Gate> gate = new Dictionary<int, Gate>();//门
        public Dictionary<int, Key> key = new Dictionary<int, Key>();//钥匙
        public Dictionary<int, Wall> wall = new Dictionary<int, Wall>();//墙壁
        public Dictionary<int, Func_Prop> func = new Dictionary<int, Func_Prop>();//特殊功能性道具
        public int Up_x;
        public int Up_y;
        public int Down_x;
        public int Down_y;
    }

    public class Play //玩家
    {
        public string Name = "勇者";
        public int Hp = 400;
        public int Atk = 15;
        public int Dfs = 10;
        public int Level = 1;
        public int Exp = 0;
        public int Money = 0;
        public int x = 11;
        public int y = 6;
        public int relKey = 10;
        public int BlueKey = 0;
        public int RedKey = 0;
        public bool Jump = false;
        public bool See = false;

    }

    public class Monster//怪物
    {
        public string Name;
        public int Hp;
        public int Atk;
        public int Dfs;
        public int Level;
        public int x;
        public int y;
        public int Slime(int _x, int _y)
        {
            Name = "史莱姆";
            Hp = 50;
            Atk = 20;
            Dfs = 1;
            Level = 1;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Skeleton(int _x, int _y)
        {
            Name = "骷髅怪";
            Hp = 110;
            Atk = 25;
            Dfs = 5;
            Level = 2;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Skeleton2(int _x, int _y)
        {
            Name = "骷髅将军";
            Hp = 150;
            Atk = 50;
            Dfs = 20;
            Level = 3;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Bat(int _x, int _y)
        {
            Name = "小蝙蝠";
            Hp = 100;
            Atk = 20;
            Dfs = 5;
            Level = 1;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Master(int _x, int _y)
        {
            Name = "暗黑法师";
            Hp = 125;
            Atk = 60;
            Dfs = 20;
            Level = 3;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Giant(int _x, int _y)
        {
            Name = "巨人";
            Hp = 450;
            Atk = 150;
            Dfs = 90;
            Level = 3;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Devil(int _x, int _y)
        {
            Name = "魔王";
            Hp = 1000;
            Atk = 400;
            Dfs = 300;
            Level = 5;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }

        public bool Battle(Play play)
        {
            bool c = true;
            while (c)
            {
                if (play.Hp < 0 || Hp < 0)
                {
                    c = false;
                }
                int x, y;
                x = play.Atk - Dfs;
                y = Atk - play.Dfs;
                if (x < 0)
                {
                    x = 0;
                }
                if (y < 0)
                {
                    y = 0;
                }
                play.Hp = play.Hp - y;
                Hp = Hp - x;

                //Console.ReadKey();
                // 信息
            }
            if (Hp < 0)
            {
                return true;
            }
            else if (play.Hp < 0)
            {
                return false;
            }
            return true;
        }

        public int Battle2(int x,int y,int z)//x指攻击，y防御，z血量！
        {
            int a, s, d;
            a = Hp;
            s = Atk;
            d = Dfs;
            int Tmp_z = z;
            bool c = true;
            while (c)
            {
                if (z<0||a<0)
                {
                    c = false;
                }
                int tmp_x, tmp_y;
                tmp_x = x - d;
                tmp_y = s - y;
                if (tmp_x<0)
                {
                    tmp_x = 0;
                }
                if (tmp_y<0)
                {
                    tmp_y = 0;
                }
                z = z - tmp_y;
                a = a - tmp_x;

            }
            if (z<0)
            {
                return 9999;
            }

            if (a<0)
            {
                return Tmp_z - z;
            }
            return 0;

        }
    }

    public class Prop//道具
    {
        public string Name;
        public int Addatk;
        public int Adddfs;
        public int AddHp;
        public int x;
        public int y;
        public int Sword(int _x, int _y)//圣剑
        {
            Name = "圣剑";
            Addatk = 50;
            Adddfs = 0;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Shield(int _x, int _y)//圣盾
        {
            Name = "圣盾";
            Addatk = 0;
            Adddfs = 50;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int PotionRed(int _x, int _y)//红血瓶
        {
            Name = "红血瓶";
            AddHp = 50;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int PotionBlue(int _x, int _y)//蓝血瓶
        {
            Name = "蓝血瓶";
            AddHp = 100;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int GemstoneBlue(int _x, int _y)//蓝宝石
        {
            Name = "蓝宝石";
            Adddfs = 5;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int GemstoneRed(int _x, int _y)//红宝石
        {
            Name = "红宝石";
            Addatk = 5;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
    }

    public class Map//地图
    {
        public char[,] maps = new char[13, 13];
        public ConsoleColor[,] color_buffer = new ConsoleColor[13, 13];

        public void Fill_Map()//地图填充
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    maps[i, j] = ' ';
                }
            }
        }
        public void Fill_Map2()//地图颜色填充
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    color_buffer[i, j] = ConsoleColor.Gray;
                }
            }
        }

        public void Boundary_Map()//地图边界填充
        {
            for (int i = 0; i < 13; i++)
            {
                maps[i, 0] = '█';
                maps[0, i] = '█';
                maps[12, i] = '█';
                maps[i, 12] = '█';
            }

        }
        public void Print()//打印地图
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (j == 12)
                    {
                        Console.WriteLine(maps[i, j]);
                        break;
                    }
                    if (maps[i, j] < 128)
                    {


                        Console.Write(maps[i, j]);
                        Console.Write(" ");
                        continue;
                    }
                    Console.Write(maps[i, j]);
                }
            }
        }

    }

    public class Npc//Npc
    {
        public String Name;
        public String Dialogue;
        public int x;
        public int y;

        public int Devil_Shop(int _x,int _y)
        {
            Name = "恶魔商店";
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Exp_Shop(int _x, int _y)
        {
            Name = "经验商人";
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Money_Shop(int _x, int _y)
        {
            Name = "钥匙商人";
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Oldman1(int _x, int _y)
        {
            Name = "老人";
            Dialogue = "老人：感谢勇士救了我，这3把钥匙就送给你了";
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Oldman2(int _x, int _y)
        {
            Name = "富商";
            Dialogue = "富商：感谢勇士救了我，这200金币就送给你了";
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
        public int Oldman3(int _x, int _y)
        {
            Name = "祭师";
            Dialogue = "祭师：感谢勇士救了我，我只能祝福你救出公主了";
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }

    }

    public class Stairs//楼梯
    {
        public Dict type;
        public int x;
        public int y;
        public int TypeStairs(Dict s, int _x, int _y)
        {
            type = s;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
    }

    public class Gate//门
    {
        public ConsoleKey2 type;
        public int x;
        public int y;

        public int TypeGate(ConsoleKey2 s, int _x, int _y)
        {
            type = s;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }

    }

    public class Key//钥匙
    {
        public ConsoleKey2 type;
        public int x;
        public int y;

        public int TypeKey(ConsoleKey2 s, int _x, int _y)
        {
            type = s;
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
    }

    public class Wall//墙壁
    {
        public int x;
        public int y;
        public int Wallpos(int _x, int _y)
        {

            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
    }

    public class Func_Prop//功能道具
    {
        public String Name;
        public int x;
        public int y;

        public int Jump_Prop(int _x,int _y)
        {
            Name = "楼层跳跃魔杖";
            x = _x;
            y = _y;
            return Program.MapPos(x,y);
        }
        public int Memory_Prop(int _x, int _y)
        {
            Name = "怪物字典";
            x = _x;
            y = _y;
            return Program.MapPos(x, y);
        }
    }
}
