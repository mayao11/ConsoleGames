using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 放置江湖.Base;
using 放置江湖.Global;

namespace 放置江湖
{
    
    class Program
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        static bool isShow = false;
        /// <summary>
        /// 声明公共类
        /// </summary>
        static Global.Global global = new Global.Global();
        /// <summary>
        /// 获取键盘事件
        /// </summary>
        static ConsoleKeyInfo input;
        /// <summary>
        /// 玩家姓名
        /// </summary>
        static string name;
        /// <summary>
        /// 玩家角色
        /// </summary>
        static Character player = new Character();
        /// <summary>
        /// 背包
        /// </summary>
        static ItemManager Bag = new ItemManager();
        /// <summary>
        /// 怪物
        /// </summary>
        static Character monster = new Character();
        /// <summary>
        /// 怪物集合
        /// </summary>
        static List<Character> monsterList = new List<Character>();

        static void Main(string[] args)
        {
            StartScene();
            IniMap();
            while (true)
            {
                if (global.timer.Enabled==false)
                {
                  
                    MainScene();
                    BagScene();
                    Console.WriteLine("计时器关");
                    Console.ReadLine();
                }
                else
                {
                    FightScene();
                    Console.WriteLine("计时器开");
                    Console.ReadLine();
                }
            }
         
        
        }
       
        
        /// <summary>
        /// 背包界面
        /// </summary>
        private static void BagScene()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            player.itemList = Bag.GetItemList();
            Console.WriteLine("当前背包中存在下列物品 背包容量为{0}/{1}",player.itemList.Count,"100");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("物品名称/物品数量");
            for (int i = 0; i < player.itemList.Count; i++)
            {
                Console.WriteLine("{0}     /{1}", player.itemList[i].name, player.itemList[i].number);
            }
        }

        /// <summary>
        /// 战斗界面
        /// </summary>
        private static void FightScene()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("你离开了主城,来到了次元门旁边你的面前出现了无数的门，你可以输入数字去对应的地图挂机!");
            Console.WriteLine("---------------------------------------");
        }

        /// <summary>
        /// 主界面
        /// </summary>
        private static void MainScene()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("玩家姓名:{0}", player.name);
            Console.WriteLine("---------------------------------------");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("玩家属性:");
            Console.WriteLine("生命值:{0}/{1}", player.curHp, player.maxHp);
            Console.WriteLine("魔法值:{0}/{1}", player.curMp, player.maxHp);
            Console.WriteLine("经验值:{0}/{1}", player.curExp, player.maxExp);
            Console.WriteLine("当前等级:{0}", player.curLevel);
            Console.WriteLine("攻击力:{0}", player.atk);
            Console.WriteLine("防御力:{0}", player.def);

            Console.WriteLine("---------------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("拥有技能:");

            var temp = player.GetSkills();
            for (int i = 0; i < temp.Count; i++)
            {
                Console.Write(temp[i].name + "  ");
            }
            Console.Write("\n");
            Console.WriteLine("---------------------------------------");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("游戏操作方法:");
            Console.WriteLine("o键输入关卡项目进行挂机！\np键退出游戏！\n");
        }

        /// <summary>
        /// 开始界面
        /// </summary>
        static void StartScene()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------只是一个放置游戏-----------");
            Console.WriteLine("---------------------------------------");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("------XXXXXXXXXXXXXXXXXXXX---------");
            Console.WriteLine("------XXXXXXXXXXXXXXXXXXXX---------");
            Console.WriteLine("------XXXXXX(介绍不会写)XXXXX---------");
            Console.WriteLine("------XXXXXXXXXXXXXXXXXXXX---------");
            Console.WriteLine("------XXXXXXXXXXXXXXXXXXXX---------");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("游戏操作方法:");
            Console.WriteLine("o键输入关卡项目进行挂机！\np键退出游戏！\n");
            Console.WriteLine("请输入玩家姓名，按回车开始游戏");
            name = "zhulei";
            IniPlayer();
        }


        /// <summary>
        /// 初始化玩家
        /// </summary>
        static void IniPlayer()
        {
            player = player.IniPlayer(name, 500, 200, 10, 10, 1);
            player.AddSkill(Skill.SkillCreateNormalAttack("蓄力一击", 5));
            player.AddSkill(Skill.CreateDamageSkill("光速次元斩", 20, 10));
            player.AddSkill(Skill.CreateHealSkill("吃血瓶", 50, 100));
            player.AddSkill(Skill.CreateDeBuffSkill("灼烧", 10, 10, 3));
            player.AddSkill(Skill.CreateInvincibleSkill("无敌", 100, 3));

            Bag.AddItem(Item.CreateConsumable(1, "大血瓶", 100, 0, 100, "里面存放了不知名的红色液体，甚至有点发霉，喝下却能恢复100点生命值",10));
            Bag.AddItem(Item.CreateConsumable(2, "大蓝瓶", 0, 100, 100, "里面存放了不知名的蓝色液体，甚至有点发霉，喝下却能恢复100点魔法值",10));
            Bag.AddItem(Item.CreateEquip(3,"木剑",0,0,10,0,100,EquipType.Hand,"一把木剑，装备后提高10点攻击力，感觉除了鸡什么也杀不死"));
            Bag.AddItem(Item.CreateEquip(4, "草帽", 10, 0, 0, 5, 100, EquipType.Head, "一顶草帽，装备后提高10点生命，5点防御力，也就挡挡叶子"));
            Bag.AddItem(Item.CreateEquip(5, "布衣", 50, 50, 0, 20, 100, EquipType.Body, "一件布衣，装备后提高50点生命值，50点魔法值，20点防御力，非常轻，感觉奸商肯定偷工减料了"));
            Bag.AddItem(Item.CreateEquip(6, "布鞋", 10, 0, 0 , 5, 100, EquipType.Shoes, "一双布鞋，装备后提高10点生命值，5点防御力，感觉还不如光脚实在"));
 

        }

        /// <summary>
        /// 初始化关卡
        /// </summary>
        static void IniMap()
        {
            monster = new Character();
            monster = monster.IniMonster("史莱姆", 100, 10, 10, 10);
            monsterList.Add(monster);
            monster = new Character();
            monster = monster.IniMonster("野兽", 1000, 100, 100, 100);
            monsterList.Add(monster);
            monster = new Character();
            monster = monster.IniMonster("半兽人", 10000, 1000, 1000, 1000);
            monsterList.Add(monster);
            monster = new Character();
            monster = monster.IniMonster("魔王", 100000, 10000, 10000, 10000);
            monsterList.Add(monster);
            monster = new Character();
            monster = monster.IniMonster("灭世", 1000000, 100000, 100000, 100000);
            monsterList.Add(monster);

            
        }
    }
}
