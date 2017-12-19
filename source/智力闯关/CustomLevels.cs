using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

// 关卡逻辑在本文件中自定义


namespace Rogue
{
    // 这部分是关卡自定义
    public class CustomLevels
    {
        // 利用Delegate，简化MapPos函数的使用。否则每次用MapPos都要写Rogue.MapPos
        delegate int DelegateMapPos(int x, int y);
        static DelegateMapPos MapPos = Rogue.MapPos;
        delegate void DelegateMapXY(int pos, out int x, out int y);
        static DelegateMapXY MapXY = Rogue.MapXY;

        // 自定义关卡流程，新加关卡可以再后面加，也可以删除关卡或者调整顺序
        public static void AddAllLevels()
        {
            Rogue.AddLevel(InitLevel1);
            Rogue.AddLevel(InitLevel2);
            Rogue.AddLevel(InitLevel3);
        }

        public static void BuildWall()
        {
            // 左上
            for (int i = 0; i < 4; i++)
            {
                Wall wall = new Wall();
                wall.SetPos(5, i);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 0; i < 6; i++)
            {
                Wall wall = new Wall();
                wall.SetPos(i, 5);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            // 右上
            for (int i = 0; i < 4; i++)
            {
                Wall wall = new Wall();
                wall.SetPos(24, i);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 24; i < 28; i++)
            {
                Wall wall = new Wall();
                wall.SetPos(i, 5);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            // 左下
            for (int i = 11; i < 15; i++)
            {
                Wall wall = new Wall();
                wall.SetPos(5, i);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 0; i < 6; i++)
            {
                Wall wall = new Wall();
                wall.SetPos(i, 9);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            // 右下
            for (int i = 11; i < 15; i++)
            {
                Wall wall = new Wall();
                wall.SetPos(24, i);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 24; i < 30; i++)
            {
                Wall wall = new Wall();
                wall.SetPos(i, 9);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            Wall wall1 = new Wall();
            wall1.SetPos(5,6);
            Rogue.walls[MapPos(wall1.x, wall1.y)] = wall1;

            Wall wall2 = new Wall();
            wall2.SetPos(5, 8);
            Rogue.walls[MapPos(wall2.x, wall2.y)] = wall2;

            Wall wall3 = new Wall();
            wall3.SetPos(24, 6);
            Rogue.walls[MapPos(wall3.x, wall3.y)] = wall3;

            Wall wall4 = new Wall();
            wall4.SetPos(24, 8);
            Rogue.walls[MapPos(wall4.x, wall4.y)] = wall4;

            Wall wall5 = new Wall();
            wall5.SetPos(27, 6);
            Rogue.walls[MapPos(wall5.x, wall5.y)] = wall5;

            Wall wall6 = new Wall();
            wall6.SetPos(27, 8);
            Rogue.walls[MapPos(wall6.x, wall6.y)] = wall6;

            Wall wall7 = new Wall();
            wall7.SetPos(29, 5);
            Rogue.walls[MapPos(wall7.x, wall7.y)] = wall7;

            Wall wall8 = new Wall();
            wall8.SetPos(28, 5);
            Rogue.walls[MapPos(wall8.x, wall7.y)] = wall8;
        }

        static void MonsterDropMoney(Monster monster)
        {
            if (monster.drop_money <= 0)
            {
                return;
            }

            Money money = new Money(monster.drop_money);
            int pos = MapPos(monster.x, monster.y);

            Rogue.moneys[pos] = money;
            MapXY(pos, out money.x, out money.y);
        }

        static void InitLevel1()
        {
            OldMan1 o = new OldMan1("采药老者",5);
            o.SetPos(29, 14);
            Rogue.npcs[MapPos(o.x, o.y)] = o;

            Rogue.AddGear(2, 7);
        }

        static void InitLevel2()
        {
            OldMan2 o = new OldMan2("守门者", 5);
            o.SetPos(3,7);
            Rogue.npcs[MapPos(o.x, o.y)] = o;
            Rogue.ReadMap("../../map13.txt");
        }
        static void InitLevel3()
        {
            Rogue.level_target = "目标：消灭大魔王，救出公主。";

            BuildWall();

            Rogue.AddMonster(5, 4, 21 , 50);
            Rogue.AddMonster(24, 4, 37);
            Rogue.AddMonster(24, 10, 120);
            Rogue.AddMonster(5, 10, 21, 50);
            Rogue.AddMonster(5, 7, 21 , 50);
            Rogue.AddMonster(24, 7, 121);
            Rogue.AddMonster(27, 7, 150);
            for (int i = 0; i < 15; i++)
            {
                Rogue.AddMonster(10, i, 34, 10);
            }
            for (int i = 0; i < 15; i++)
            {
                Rogue.AddMonster(15, i, 34, 10);
            }
            for (int i = 0; i < 15; i++)
            {
                Rogue.AddMonster(20, i, 34, 10);
            }

            Equipment equipment = new Equipment("利刃", 20);
            equipment.SetPos(3, 7);
            Rogue.equipments[MapPos(equipment.x, equipment.y)] = equipment;

            Equipment equipment1 = new Equipment("屠魔剑", 8);
            equipment1.SetPos(26, 6);
            Rogue.equipments[MapPos(equipment1.x, equipment1.y)] = equipment1;

            Prop prop = new Prop("人参", 96);
            prop.SetPos(29, 14);
            Rogue.props[MapPos(prop.x, prop.y)] = prop;

            Businessman1 businessman1 = new Businessman1("被关在地牢中的冒险家", 1);
            businessman1.SetPos(0,0);
            Rogue.npcs[MapPos(businessman1.x, businessman1.y)] = businessman1;

            Businessman2 businessman2 = new Businessman2("商人", 2);
            businessman2.SetPos(0, 14);
            Rogue.npcs[MapPos(businessman2.x, businessman2.y)] = businessman2;

            Pangolin pangolin = new Pangolin("盗贼", 3);
            pangolin.SetPos(29,0);
            Rogue.npcs[MapPos(pangolin.x, pangolin.y)] = pangolin;

            Princess princess = new Princess("公主", 10);
            princess.SetPos(29, 7);
            Rogue.npcs[MapPos(princess.x, princess.y)] = princess;

            Rogue.onMonsterDead = (Monster monster) =>
            {
                MonsterDropMoney(monster);
                return true;
            };
            //// 匿名函数还可以像下面这样写：

            //Rogue.onMonsterDead = delegate (Monster monster)
            //{
            //    if (Rogue.monsters.Count() <= 1)
            //    {
            //        Rogue.OnStageClear();
            //    }
            //    return true;
            //};
        }

        static void InitLevel4()
        {
            Rogue.level_target = "目标：升到9级。";
            Rogue.AddMonster(3, 3, 1);

            Rogue.onMonsterDead = (Monster monster) =>
            {
                if (Rogue.player.level == 8)
                {
                    Rogue.OnStageClear();
                    return true;
                }

                int pos = Rogue.RandPos();
                int x, y;
                MapXY(pos, out x, out y);
                Rogue.AddMonster(x, y, 1);

                Rogue.level_target = string.Format("目标：升到9级。({0}/9)", Rogue.player.level + 1);
                return true;
            };
        }

        static void InitLevel5()
        {
            Rogue.level_target = "目标：消灭Boss!";
            Rogue.AddMonster(3, 3, Rogue.random.Next(1, 3), 10);
            Rogue.AddMonster(15, 20, Rogue.random.Next(1, 3), 10);
            Rogue.AddMonster(20, 1, Rogue.random.Next(2, 4), 10);
            Monster boss1 = Rogue.AddMonster(Rogue.map_width / 2, Rogue.map_height - 1, 90);
            boss1.attack = 8;

            Merchant merchant = new Merchant("尖嘴猴腮的胖子", 200);
            merchant.SetPos(Rogue.map_width - 1, Rogue.map_height - 1);
            Rogue.npcs[MapPos(merchant.x, merchant.y)] = merchant;

            Rogue.onMonsterDead = (Monster monster) =>
            {
                if (monster.id == 202)
                {
                    Rogue.OnStageClear();
                    return true;
                }

                if (monster == boss1)
                {
                    merchant.SetState(2);
                    return true;
                }

                MonsterDropMoney(monster);

                int pos = Rogue.RandPos();
                int x, y;
                MapXY(pos, out x, out y);
                if (Rogue.player.level < 9 && Rogue.player.money < 100)
                {
                    Rogue.AddMonster(x, y, Rogue.player.level + 2, (Rogue.player.level + 1) * 10);
                }

                return true;
            };
        }

        static void InitLevel6()
        {
            Rogue.level_target = "目标：消灭所有怪物。";

            Rogue.AddMonster(1, 1, 1);
            Rogue.AddMonster(10, 5, 2);
            Rogue.AddMonster(15, 14, 3);

            Rogue.AddMonster(0, 10, 9);

            OldMan oldman = new OldMan("戴黑框眼镜的老者", 100);
            oldman.SetPos(Rogue.map_width - 1, Rogue.map_height - 1);
            Rogue.npcs[MapPos(oldman.x, oldman.y)] = oldman;

            Rogue.onMonsterDead = (Monster monster) =>
            {
                if (Rogue.monsters.Count() <= 1)
                {
                    Rogue.OnStageClear();
                }
                return true;

            };
        }
    }
}

namespace Rogue
{
    // 这部分可以自定义NPC、怪物等
    public class OldMan : NPC
    {
        public OldMan(string _name, int _id) : base(_name, _id)
        {
        }

        public override bool OnTalk(Player player, out string text)
        {
            if (player.level <= 3)
            {
                text = name + "：" + "勇者，你还没有掌握面向对象的技术，等你3级以后再来找我吧。";
                return false;
            }
            text = name + "：" + "我将把我平生所学全部传授给你。";
            for (int i = 0; i < 5; ++i)
            {
                player.AddExp(1);
            }
            return true;
        }
    }

    public class OldMan1 : NPC
    {
        public OldMan1(string _name, int _id) : base(_name, _id)
        {
        }

        public override bool OnTalk(Player player, out string text)
        {
            if (true)
            {
                text = name + "：" + "这只是魔王巢穴最外层的小迷宫，好好探索一下吧";
            }
            return true;
        }
    }

    public class OldMan2 : NPC
    {
        public OldMan2(string _name, int _id) : base(_name, _id)
        {
        }

        public override bool OnTalk(Player player, out string text)
        {
            if (true)
            {
                text = name + "：" + "这是一串神奇的数字，其中隐藏着开门的密码"+"\n"
                    +"273--42--8 248--64--24--8 378--【?】--48--32--6"+"\n"
                    +"请操控箭头点击正确的数字";
            }
            return true;
        }
    }
    public class Pangolin : NPC
    {
        public Pangolin(string _name, int _id) : base(_name, _id)
        {
        }

        public override bool OnTalk(Player player, out string text)
        {
            if (player.attack < 130)
            {
                text = name + "：" + "勇士你好，我是一个勤劳的盗贼，听说明天大魔王就要强迫公主嫁给他了";
                return false;
            }
            text = name + "：" + "好吧，我挖了一个洞通往大魔王的房间，自己找找看吧";
            return true;
        }
    }
    public class Businessman1 : NPC
    {
        public Businessman1(string _name, int _id) : base(_name, _id)
        {
        }

        public override bool OnTalk(Player player, out string text)
        {
            if (player.money < 100)
            {
                text = name + "：" + "勇者，初入此地，一定对周围的一切感到陌生吧，只要一百块，我讲告诉你我知道的一切";
                return false;
            }
            text = name + "：" + "×21级怪物 ⊙34级怪物 ¤37级怪物 ￠120级怪物 ∮121级怪物 №150级大魔王。对了，地牢左下方有个商人，不过他比我更黑";
            player.money -= 100;
            return true;
        }
    }
    public class Businessman2 : NPC
    {
        public Businessman2(string _name, int _id) : base(_name, _id)
        {
        }

        public override bool OnTalk(Player player, out string text)
        {
            if (player.money < 100)
            {
                text = name + "：" + "给我100块，我可以给你增加10点攻击力";
                return false;
            }
            else if (player.money >= 100)
            {
                text = name + "：" + "攻击力增加10，欢迎下次光临";
                player.attack += 10;
                player.money -= 100;
                return false;
            }
            else
            {
                text = name + "：" + "我已经无法满足你的要求了";
                return true;
            }
        }
    }
    public class Princess : NPC
    {
        public Princess(string _name, int _id) : base(_name, _id)
        {
        }

        public override bool OnTalk(Player player, out string text)
        {
            if (player.attack > 129)
            {
                text = name + "：" + "谢谢骑士你把我就救出去，我会让我父王重金酬谢你的";
                Rogue.OnStageClear();
                return true;
            }
            text = name + "：" + "你也太弱了吧，哼，天知道你是怎么到这的";
            return false;
        }
    }

    public class Merchant : NPC
    {
        public Merchant(string _name, int _id) : base(_name, _id)
        {
        }

        int state = 0;

        public void SetState(int s)
        {
            state = s;
        }

        public override bool OnTalk(Player player, out string text)
        {
            if (state == 0)
            {
                if (player.money < 100)
                {
                    text = name + "：" + "勇者，虽然我很尊敬您。但是这把龙渊剑成本价要100块，我也是要吃饭的啊。";
                    return false;
                }
                text = name + "：" + "这把剑就交给你了，一定要为民除害！";
                player.attack += 65;
                player.money -= 100;
                state = 1;
                return false;
            }
            else if (state == 1)
            {
                text = name + "：" + "这把剑就交给你了，一定要为民除害！";
                return false;
            }

            // state == 2
            text = name + "：" + "哈哈哈，你帮助我把讨厌的企鹅爸爸干掉了，非常好。现在让我来送你下地狱吧……";
            return true;
        }

        override public object AfterDisappear()
        {
            Monster m = new Monster(91);
            m.x = x;
            m.y = y;
            m.id = 202;
            m.attack = 1;
            return m;
        }
    }
}

