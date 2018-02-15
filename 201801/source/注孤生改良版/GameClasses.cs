using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 注孤生改良版
{
    public class Player
    {

        public string name = "王";
        public int hp=5;
        public int attack=1;
        public List<Key> bag = new List<Key>();
        public List<ElectricSwitch> bags = new List<ElectricSwitch>();
        public List<LongPole> bagss = new List<LongPole>();
        public int x;
        public int y;

        public void Reset()
        {
            hp = 10;
            attack = 1;
        }

        public int Attack(Monster m, out string s)
        {
            s = "你拿到了假的道具受到了致命伤！";
            if (hp <= 0)
            {
                s += "你已经死亡。";
                return -1;
            }
            return 1;          
        }
    }
    public class Monster
    {
        public string name = "假道具";
        public int hp;
        public int attack;

        public int id = 0;

        public Monster(string _name, int _hp, int _attack, int _id)
        {
            name = _name;
            hp = _hp;
            attack = _attack;
            id = _id;
        }

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x; y = _y;
        }
        virtual public bool OnTalk(Player player, out string text)
        {
            text = "我不能碰！你已经死了。";
            return false;
        }
        virtual public object AfterDisappear()
        {
            return null;
        }
    }
    public class NPC
    {
        public string name ;
        public int id;
        public String Dialogue;

        public NPC(string _name, int _id)
        {
            name = _name;
            id = _id;
        }


        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        virtual public bool OnTalk(Player player, out string text)
        {
            text = name+":"+"这是一把Ⅰ型钥匙，拿着这把钥匙开启我背后的门，去救出你的女朋友吧";
            foreach (var npc in Rogue.npcs)
            {
                if (id == 3)
                {
                    text = name + ":" + "每个角落都有不同的道具，这里没有哪个道具呢";
                    continue;
                }
                if (id == 2)
                {
                    if(!(player.bagss==null))
                    {
                        Rogue.victory = true;

                    }
                    else if(player.bagss == null)
                    text = name + ":" + "你哪里有女朋友，下地狱去吧！";
                    Rogue.game_over = true;
                    return true;
                }
            }
            return true;
        }
        virtual public object AfterDisappear()
        {
            return null;
        }
    }
    public class Key
    {
        public string name ;
        public int id;

        public Key(string _name, int _id)
        {
            name = _name;
            id = _id;
        }

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        virtual public bool OnTalk(Player player, out string text)
        {
            text = "你拿到了"+id+"号钥匙";
            return false;
        }
        virtual public object AfterDisappear()
        {
            return null;
        }
    }
    public class Thorn
    {
        public string name = "◆";
        public int id;

        public Thorn(string _name, int _id)
        {
            name = _name;
            id = _id;
        }

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
    public class Door
    {
        public string name = "〓";
        public int id;

        public Door(string _name,int _id)
        {
            name = _name;
            id = _id;
        }

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        virtual public bool OnTalk(Player player, out string text)
        {
            text = "缺少这个门的钥匙";
            foreach (var key in player.bag)
            {
                if(key.id==id)
                {
                    text = "开启了" + id + "号大门";
                    break;
  
                }
                
                
            }
            return false;

        }
        virtual public object AfterDisappear()
        {
            return null;
        }
    }
    public class ElectricSwitch
    {
        public string name = "※";
        public int id;

        public ElectricSwitch(string _name, int _id)
        {
            name = _name;
            id = _id;
        }

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        virtual public bool OnTalk(Player player, out string text)
        {
            text = "你开启了电闸！";
            return false;
        }
        virtual public object AfterDisappear()
        {
            return null;
        }
    }
    public class LongPole
    {
        public string name = "￡";
        public int id;

        public LongPole(string _name, int _id)
        {
            name = _name;
            id = _id;
        }

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        virtual public bool OnTalk(Player player, out string text)
        {
            text = "你获得了圣剑！";
            return false;
        }
        virtual public object AfterDisappear()
        {
            return null;
        }
    }
    public class ElectricityDoor
    {
        public string name ;
        public int id;

        public ElectricityDoor(string _name, int _id)
        {
            name = _name;
            id = _id;
        }

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        virtual public bool OnTalk(Player player, out string text)
        {
            text = "这个大门没有电！";
            foreach (var key in player.bags)
            {
                if (key.id == id)
                {
                    text = "你开启了电闸，打开了这个大门！";
                    break;
                }
            }
            return false;
        }
        virtual public object AfterDisappear()
        {
            return null;
        }
    }
    public class Transfer
    {
        public string name = "¤";
        public int id;

        public Transfer(string _name, int _id)
        {
            name = _name;
            id = _id;
        }

        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
        virtual public object AfterDisappear()
        {
            return null;
        }
        virtual public bool OnTalk(Player player, out string text)
        {
            text = "你被传送到一个未知的地方！";
            return false;
        }
    }
    public class Wall
    {
        public string name = "█";
        public int id;

        public Wall(string _name, int _id)
        {
            name = _name;
            id = _id;
        }
        public int x;
        public int y;

        public void SetPos(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }
}
