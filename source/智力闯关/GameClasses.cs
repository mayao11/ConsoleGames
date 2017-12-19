using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


// 这些是游戏逻辑的基类 玩家、NPC和怪物，扩展class最好写到CustomLevels文件中
// 当然也可以修改Player的基本逻辑
namespace Rogue
{
    public class Player
    {
        public int hp;
        public int level;
        public int attack;

        public int money;

        public int x;
        public int y;

        public void Reset()
        {
            hp = 3;
            level = 1;
            attack = 1;
            money = 0;
        }

        public int Attack(Monster m, out string s)
        {
            int cost_hp = attack;
            m.hp -= cost_hp;
            s = "对怪物造成" + cost_hp + "点伤害。";
            if (m.hp < 0) { m.hp = 0; }
            if (m.hp == 0)
            {
                return 1;
            }

            int cost_hp2 = m.attack;
            hp -= cost_hp2;
            s += "怪物对玩家造成" + cost_hp2 + "点伤害。";
            if (hp < 0) { hp = 0; }
            if (hp == 0)
            {
                // 玩家死亡
                s += "玩家死亡。";
                return -1;
            }

            // 均未死亡
            return 0;
        }

        public int AddExp(int _exp)
        {
            level += 1;
            hp += 1;
            attack += 1;
            return 1;
        }
    }

    public class Monster
    {
        public int hp = 10;
        public int attack = 1;

        public int level = 1;

        public int drop_money = 0;

        public int id = 0;

        public Monster(int _level)
        {
            hp = _level;
            attack = _level;
            level = _level;
        }

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x; y = _y;
        }
    }

    public class Wall
    {
        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    public class Equipment 
    {
        public string name = "装备";
        public int id = 0;
        public int atk = 0;


        public Equipment(string _name , int _atk)
        {
            name = _name;
            atk = _atk;
        }
        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
    public class Prop
    {
        public string name = "道具";
        public int id = 0;
        public int hp = 0;


        public Prop(string _name, int _hp)
        {
            name = _name;
            hp = _hp;
        }
        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    public class Gear
    {
        public string name = "机关";
        public int id = 0;

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    public class Block
    {
        public string name = "方块";
        public int id = 0;

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

    public class Money
    {
        public int money = 0;

        public Money(int _money)
        {
            money = _money;
        }

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public int x;
        public int y;

        public int id = 0;
    }

    public class NPC
    {
        public string name = "NPC";
        public int id;

        public NPC(string _name, int _id)
        {
            name = _name;
            id = _id;
        }

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        virtual public bool OnTalk(Player player, out string text)
        {
            text = "";
            return false;
        }

        public int x;
        public int y;

        virtual public object AfterDisappear()
        {
            return null;
        }
    }
}
