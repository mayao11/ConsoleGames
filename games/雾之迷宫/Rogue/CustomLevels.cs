using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text;
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
            Rogue.AddLevel(InitLevel6);
            Rogue.AddLevel(InitLevel7);
            //Rogue.AddLevel(InitLevel5);
            //Rogue.AddLevel(InitLevel4);
            //Rogue.AddLevel(InitLevel1);
            //Rogue.AddLevel(InitLevel2);
            //Rogue.AddLevel(InitLevel3);
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
        
        
        static void InitLevel7()
        {
            Rogue.monsters.Clear();
            Rogue.walls.Clear();
            Rogue.yaoshis.Clear();
            Rogue.doors.Clear();
            Rogue.trap2s.Clear();
            Rogue.traps.Clear();

            FileStream fs = new FileStream("C:\\Users\\meta42games\\Desktop\\Map地图游戏\\Map地图游戏\\Map地图游戏\\map2.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline;
            int y = 0;

            while ((strReadline = read.ReadLine()) != null)
            {

                Console.WriteLine(strReadline);
                for (int x = 0; x < strReadline.Length; ++x)
                {
                    if (strReadline[x] == '#')
                    {
                        Walls wall = new Walls();
                        wall.SetPos(x, y);
                        Rogue.walls[MapPos(wall.x, wall.y)] = wall;
                    }
                    if (strReadline[x] == '!')
                    {
                        Rogue.AddMonster(x, y, 1, 1);
                    }
                    if (strReadline[x] == '%')
                    {
                        Yaoshi yaoshi = new Yaoshi();
                        yaoshi.SetPos(x, y);
                        Rogue.yaoshis[MapPos(yaoshi.x, yaoshi.y)] = yaoshi;
                    }
                    if (strReadline[x] == '=')
                    {
                        Door door = new Door();
                        door.SetPos(x, y);
                        Rogue.doors[MapPos(door.x, door.y)] = door;
                    }
                    if (strReadline[x] == '^')
                    {
                        Trap trap = new Trap();
                        trap.SetPos(x, y);
                        Rogue.traps[MapPos(trap.x, trap.y)] = trap;
                    }
                    if (strReadline[x] == '*')
                    {
                        Trap trap = new Trap();
                        trap.SetPos(x, y);
                        Rogue.trap2s[MapPos(trap.x, trap.y)] = trap;
                    }

                }
                y += 1;
                // strReadline即为按照行读取的字符串

            }
            Rogue.onMonsterDead = (Monster monster) =>
            {

                if (Rogue.monsters.Count() <= 1)
                {
                    Rogue.OnStageClear();
                }
                return true;
            };
           
        }
        static void InitLevel6()
        {
           
            FileStream fs = new FileStream("../../map.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline;
            int y = 0;

            while ((strReadline = read.ReadLine()) != null)
            {

                Console.WriteLine(strReadline);
                for (int x = 0; x < strReadline.Length; ++x)
                {
                    if (strReadline[x] == '#')
                    {
                        Walls wall = new Walls();
                        wall.SetPos(x, y);
                        Rogue.walls[MapPos(wall.x, wall.y)] = wall;
                    }
                    if (strReadline[x] == '!')
                    {
                        Rogue.AddMonster(x, y, 1, 1);
                    }
                    if (strReadline[x] == '%')
                    {
                        Yaoshi yaoshi = new Yaoshi();
                        yaoshi.SetPos(x, y);
                        Rogue.yaoshis[MapPos(yaoshi.x, yaoshi.y)] = yaoshi;
                    }
                    if (strReadline[x] == '=')
                    {
                        Door door = new Door();
                        door.SetPos(x, y);
                        Rogue.doors[MapPos(door.x, door.y)] = door;
                    }
                    if (strReadline[x] == '^')
                    {
                        Trap trap = new Trap();
                        trap.SetPos(x, y);
                        Rogue.traps[MapPos(trap.x, trap.y)] = trap;
                    }
                    if (strReadline[x] == '↓')
                    {
                        Loutidowm  loutidown = new Loutidowm();
                        loutidown.SetPos(x, y);
                        Rogue.loutis_d [MapPos(loutidown.x, loutidown.y)] = loutidown;
                    }
                    if (strReadline[x] == '*')
                    {
                        Trap trap = new Trap();
                        trap.SetPos(x, y);
                        Rogue.trap2s[MapPos(trap.x, trap.y)] = trap;
                    }
                    if (strReadline[x] == '剑')
                    {
                        Equipment  baojian = new Equipment ("宝剑",100);
                        baojian.SetPos(x, y);
                        Rogue.equipments[MapPos(baojian.x, baojian.y)] = baojian;
                    }

                }
                y += 1;
                // strReadline即为按照行读取的字符串
            }
            Rogue.onMonsterDead = (Monster monster) =>
            {
                
                if (Rogue.doors.Count() <= 1)
                {
                    Rogue.OnStageClear();
                  
                }
                return true;
            };
        }
        static void InitLevel5()
        {

            Rogue.AddMonster(13, 5, 1);
            Rogue.AddMonster(13, 3, 1);
            Rogue.AddMonster(8, 3, 1);
            Rogue.AddMonster(8, 5, 3);
            Rogue.AddMonster(21, 5, 12);
            Rogue.AddMonster(8, 10, 75);
            OldMan oldman = new OldMan("传说中的人物", 101);
            oldman.SetPos(8, 6);
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
        static void InitLevel4()
        {
            Rogue.level_target = "找到正真的公主";

            Rogue.AddMonster(13, 0, 1);
           

            Rogue.AddMonster(8, 5, 3);
            Rogue.AddMonster(21, 5, 12,100);
            Rogue.AddMonster(8, 10, 80,100);
            //Rogue.AddMonster(21, 10, 13);

            Rogue.AddMonster(21, 8, 1000);
            Rogue.AddMonster(8, 8, 90);

            for (int i = 0; i < 9; i++)
            {
                Walls wall = new Walls();
                wall.SetPos(i, 6);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 0; i < 5; i++)
            {
                Walls wall = new Walls();
                wall.SetPos(8, i);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 0; i < 9; i++)
            {
                Walls wall = new Walls();
                wall.SetPos(i, 9);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 11; i < 15; i++)
            {
                Walls wall = new Walls();
                wall.SetPos(8, i);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 21; i <27; i++)
            {
                Walls wall = new Walls();
                wall.SetPos(i, 6);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 28; i < 30; i++)
            {
                Walls wall = new Walls();
                wall.SetPos(i, 6);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 21; i < 30; i++)
            {
                Walls wall = new Walls();
                wall.SetPos(i, 9);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 0; i < 5; i++)
            {
                Walls wall = new Walls();
                wall.SetPos(21, i);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }
            for (int i = 11; i < 15; i++)
            {
                Walls wall = new Walls();
                wall.SetPos(21, i);
                Rogue.walls[MapPos(wall.x, wall.y)] = wall;
            }


            Walls walls = new Walls();
            walls.SetPos(21, 7);
            Rogue.walls[MapPos(walls.x, walls.y)] = walls;

            Walls walls2 = new Walls();
            walls2.SetPos(8, 7);
            Rogue.walls[MapPos(walls2.x, walls.y)] = walls2;


            OldMan oldman = new OldMan("传说中的人物", 101);
            oldman.SetPos(0, 0);
            Rogue.npcs[MapPos(oldman.x, oldman.y)] = oldman;

            Merchant merchant = new Merchant("奇怪的东西", 201);
            merchant.SetPos(21, 10);//21
            Rogue.npcs[MapPos(merchant.x, merchant.y)] = merchant;

            Merchant merchant2 = new Merchant("商人", 200);
            merchant2.SetPos(Rogue.map_width - 1, 0);
            Rogue.npcs[MapPos(merchant2.x, merchant2.y)] = merchant2;

            Princess gongzhu = new Princess("美丽善良的公主", 300);
            gongzhu.SetPos(Rogue.map_width - 1, 7);
            Rogue.princess[MapPos(gongzhu.x, gongzhu.y)] = gongzhu;

            Princess2 gongzhu2 = new Princess2("邪恶的公主", 300);
            gongzhu2.SetPos(0, 7);
            Rogue.princess[MapPos(gongzhu2.x, gongzhu2.y)] = gongzhu2;

            Dabaojian  equipment  = new Dabaojian("宝剑", 300);
            equipment.SetPos(Rogue.map_width - 1, Rogue.map_height - 1);
            Rogue.equipments[MapPos(equipment.x, equipment.y)] = equipment;

            Monster boss1 = Rogue.AddMonster(0, Rogue.map_height - 1, 140);
            boss1.attack = 50;

     

            Rogue.onMonsterDead = (Monster monster) =>
            {
                
                if (Rogue.monsters.Count() <= 1)
                {
                    Rogue.OnStageClear();
                }

                if (monster == boss1)
                {
                    merchant.SetState(2);
                    return true;
                }

               
              
                MonsterDropMoney(monster);
                int pos = Rogue.RandPos2();
                int x, y;
                MapXY(pos, out x, out y);
                if (Rogue.player.level < 2 )
                {
                    Rogue.AddMonster(x, y, Rogue.player.level + 1);
                }

                return true;
            };

        }
        static void InitLevel1()
        {
            Rogue.level_target = "目标：消灭所有怪物。";

            Rogue.AddMonster(1, 1, 1) ;
            Rogue.AddMonster(10, 5, 2);
            Rogue.AddMonster(12, 14, 3);
        
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
        static void InitLevel2()
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
        static void InitLevel3()
        {
            Rogue.level_target = "目标：消灭Boss!";
            Rogue.AddMonster(3, 3, Rogue.random.Next(1, 3), 10);
            Rogue.AddMonster(15, 20, Rogue.random.Next(1, 3), 10);
            Rogue.AddMonster(20, 1, Rogue.random.Next(2, 4), 10);
            Monster boss1 = Rogue.AddMonster(15, 10, 90);
            boss1.attack = 50;

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
            text = name + "：" + "勇士 我终于等到你了";
            return true;
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
                    text = name + "：" + "勇者，虽然我很尊敬您。但是这把剑成本价要100块，我也是要吃饭的啊。";
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
            text = name + "：" + "哈哈哈，你帮助我把我的仇人都干掉了，至于你想要的尚方宝剑，我是不会给你的";
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
    public class TimeOldman : NPC
    {
        public TimeOldman(string _name, int _id) : base(_name, _id)
        {

        }
        public override bool OnTalk(Player player, out string text)
        {

            text = name + "：" + "勇者，要想找到正真的公主，其实是有捷径的，我相信爱情的力量一定能使你找到那条捷径";
            return true;
        }
    }
    public class Princess : Base_Princess
    {
        public Princess(string _name, int _id) : base(_name, _id)
        {

        }
        public override bool OnTalk(Player player, out string text)
        {

            text = name + "：" + "勇者，我终于等到你了，我们开始新的生活吧";
            return true;
        }
    }
    public class Princess2 : Base_Princess
    {
        public Princess2(string _name, int _id) : base(_name, _id)
        {

        }
        public override bool OnTalk(Player player, out string text)
        {

            text = name + "：" + "勇者，我终于等到你了，不过我想告诉你 我不是真正的公主,我要拿走你所有的钱，吸干你的攻击力，只剩下1";
            player.attack = 1;
            player.money = 0;
            return true;
        }
        override public object AfterDisappear()
        {
            TimeOldman m = new TimeOldman("时光老人", 999);
            m.x = 15;
            m.y = Rogue.map_height - 1;

            return m;
        }
    }
    public class Dabaojian : Equipment
    {
        public Dabaojian(string _name, int _id) : base(_name, _id)
        {

        }
        override public object AfterDisappear()
        {
            Trap m = new Trap() ;
            m.x = 22;
            m.y = 10;
            return m;
        }
    }
}

  

