using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// 这些是游戏逻辑的基类 玩家、NPC和怪物，扩展class最好写到CustomLevels文件中
// 当然也可以修改Player的基本逻辑
namespace Rogue
{

    public class Guanka
    {
        public  Dictionary<int, Monster> monsters;
        public  Dictionary<int, Money> moneys;
        public  Dictionary<int, NPC> npcs;
        public  Dictionary<int, Walls> walls;
        public  Dictionary<int, Equipment> equipments;
        public  Dictionary<int, Base_Princess> princess;
        public  Dictionary<int, Trap> traps;
        public  Dictionary<int, Door> doors;
        public  Dictionary<int, Yaoshi> yaoshis;
        public  Dictionary<int, Trap> trap2s;
        public  Dictionary<int, Loutidowm> loutis_d;
        public  Dictionary<int, Loutiup> loutis_u;

        public Guanka()
        {
        }

        public Guanka(Dictionary<int, Monster> d1, Dictionary<int, NPC> d2, Dictionary<int, Walls> d3, Dictionary<int, Equipment> d4,
        Dictionary<int, Base_Princess> d5, Dictionary<int, Trap> d6, Dictionary<int, Door> d7, Dictionary<int, Yaoshi> d8, Dictionary<int, Trap> d9,
        Dictionary<int, Loutidowm> d10, Dictionary<int, Loutiup> d11)
        {
            monsters = d1; npcs = d2; walls = d3; equipments = d4; princess = d5; traps = d6; doors = d7; yaoshis = d8; trap2s = d9;
            loutis_d = d10; loutis_u = d11;
        }

    }

     
    public class Player
    {
        public int hp;
        public int level;
        public int attack;
        public int yaoshi;
        public int money;

        public int x;
        public int y;

        public void Reset()
        {
            hp = 9;
            level = 1;
            attack = 1;
            money = 0;
        }
        public void AddAtk(int i)
        {
            attack += i;
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
        public int Attack2(Monster m, out string s)
        {
            int a = 0;
            while (a == 0)
            {
                int cost_hp = attack;
                m.hp -= cost_hp;
                s = "对怪物造成" + cost_hp + "点伤害。";
                if (m.hp < 0) { m.hp = 0; }
                if (m.hp == 0)
                {
                    a = 1;
                    return a;
                }

                int cost_hp2 = m.attack;
                hp -= cost_hp2;
                s += "怪物对玩家造成" + cost_hp2 + "点伤害。";
                if (hp < 0) { hp = 0; }
                if (hp == 0)
                {
                    // 玩家死亡
                    s += "玩家死亡。";
                    a = -1;
                    return a;
                }

            }
            s = " ";
            return a;

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
        public static Random random = new Random();
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
        public int Attack(Player m, out string s)
        {
            int cost_hp = attack;
            m.hp -= cost_hp;
            s = "对玩家造成" + cost_hp + "点伤害。";
            if (m.hp < 0) { m.hp = 0; }
            if (m.hp == 0)
            {
                s += "玩家死亡。";
                return -1;
            }
            int cost_hp2 = m.attack;
            hp -= cost_hp2;
            s += "对怪兽造成" + cost_hp2 + "点伤害。";
            if (hp < 0) { hp = 0; }
            if (hp == 0)
            {
                return 1;
            }
            return 0;
        }
        public void SetPos(int _x, int _y)
        {
            x = _x; y = _y;
        }
        /*public bool Logic()
        {

        }*/

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
    public class Walls
    {


        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }


        public int x;
        public int y;

        virtual public object AfterDisappear()
        {
            return null;
        }
    }
    public class Equipment
    {
        public string name;
        public int id;

        public Equipment(string _name, int _id)
        {
            name = _name;
            id = _id;
        }

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }


        public int x;
        public int y;


        virtual public object AfterDisappear()
        {
            return null;
        }
    }
    public class Base_Princess
    {
        public string name;
        public int id;

        public Base_Princess(string _name, int _id)
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
    public class Trap
    {
        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }


        public int x;
        public int y;

        virtual public object AfterDisappear()
        {
            return null;
        }

    }
    public class Door
    {


        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public int x;
        public int y;

        virtual public bool OnTalk(Player player, out string text)
        {
            text = "";
            return false;
        }

    }
    public class Yaoshi
    {
        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public int x;
        public int y;
    }
    public class Loutidowm
    {
        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public int x;
        public int y;
    }
    public class Loutiup
    {
        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        public int x;
        public int y;

    }
}
