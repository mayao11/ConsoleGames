using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;



// Rogue类是实现整个游戏基本流程的。一开始自定义关卡时，不要修改Rogue类的实现
// 建议熟悉以后再修改
namespace Rogue
{
    public class Rogue
    {
        const int width = 100;
        const int height = 28;

        public static Guanka guanka= new Guanka(monsters, npcs, walls, equipments, princess, traps,
                doors, yaoshis, trap2s, loutis_d, loutis_u);

        public const int map_width = 30;
        public const int map_height = 15;

        static ConsoleCanvas canvas = null;
        static char[,] buffer = null;
        static ConsoleColor[,] color_buffer = null;

        public static Random random = new Random();

        public static Player player;

        
        public static Dictionary<int, Guanka> guanka2 = new Dictionary<int, Guanka>();
        
        public static Dictionary<int, Monster> monsters = new Dictionary<int, Monster>();
        public static Dictionary<int, Money> moneys = new Dictionary<int, Money>();
        public static Dictionary<int, NPC> npcs = new Dictionary<int, NPC>();
        public static Dictionary<int, Walls> walls = new Dictionary<int, Walls>();//创建一个墙的字典
        public static Dictionary<int, Equipment> equipments = new Dictionary<int, Equipment>();
        public static Dictionary<int, Base_Princess > princess = new Dictionary<int, Base_Princess>();
        public static Dictionary<int, Trap> traps = new Dictionary<int,Trap>();
        public static Dictionary<int, Door> doors = new Dictionary<int, Door>();
        public static Dictionary<int, Yaoshi > yaoshis = new Dictionary<int, Yaoshi>();
        public static Dictionary<int, Trap> trap2s = new Dictionary<int, Trap>();
        public static Dictionary<int, Loutidowm > loutis_d = new Dictionary<int, Loutidowm>();
        public static Dictionary<int, Loutiup > loutis_u = new Dictionary<int, Loutiup>();
       


        public static string below_text = "";
        public static string level_target = "";

        public static bool game_over;
        public static bool victory;

        // 委托，用于扩展关卡用。自定义关卡初始化函数
        public delegate void InitStage();
        static List<InitStage> stages = new List<InitStage>();
        // 委托，用于自定义怪物死亡时处理
        public delegate bool OnMonsterDead(Monster monster);
        public static OnMonsterDead onMonsterDead = null;

        public delegate bool OnEquipmentDead(Equipment  equipment);
        public static OnMonsterDead onEquipmentDead = null;
        public static int DirectionMove(Player a, Monster b)
        {
            int i = b.x - a.x;
            int r = b.y - a.y;
            if (i > 0 && r > 0)
            {
                int t = random.Next(0, 2);
                if (t == 0)
                {
                    b.x -= 1;
                }
                else
                {
                    b.y -= 1;
                }
            }
            if (i > 0 && r == 0)
            {

                b.x -= 1;
            }
            if (i > 0 && r < 0)
            {
                int t = random.Next(0, 2);
                if (t == 0)
                {
                    b.x -= 1;
                }
                else
                {
                    b.y += 1;
                }
            }
            if (i == 0 && r < 0)
            {
                b.y += 1;

            }
            if (i == 0 && r > 0)
            {
                b.y -= 1;

            }
            if (i < 0 && r < 0)
            {
                int t = random.Next(0, 2);
                if (t == 0)
                {
                    b.x += 1;
                }
                else
                {
                    b.y += 1;
                }
            }
            if (i < 0 && r == 0)
            {

                b.x += 1;
            }
            if (i < 0 && r > 0)
            {
                int t = random.Next(0, 2);
                if (t == 0)
                {
                    b.x += 1;
                }
                else
                {
                    b.y -= 1;
                }
            }

            int next_pos = MapPos(b.x, b.y);
            return next_pos;
        }//移动方向

        public static int AIMove(Player player, Monster b, List<int> dirs)
        {
            List<int> acts = new List<int>();
            for (int i=0; i<dirs.Count(); ++i)
            {
                int d = dirs[i];

                if (d == 1)     // 上
                {
                    if (player.y < b.y)
                    {
                        acts.Add(d);
                    }
                }
                else if (d == 2)     // 下
                {
                    if (player.y > b.y)
                    {
                        acts.Add(d);
                    }
                }
                else if (d == 3)     // 左
                {
                    if (player.x < b.x)
                    {
                        acts.Add(d);
                    }
                }
                else if (d == 4)     // 右
                {
                    if (player.x > b.x)
                    {
                        acts.Add(d);
                    }
                }
            }
            if (acts.Count() == 0)
            {
                return 0;
            }
            return acts[random.Next(0, acts.Count())];
        }
        public static int  MoveNumber(int a,Monster monster)
        { 

            if (a == 1)
            {
                monster.y -= 1;
            }
            if (a == 2)
            {
                monster.y += 1;
            }
            if (a == 3)
            {
                monster.x-= 1;
            }
            if (a == 4)
            {
                monster.x+= 1;
            }
            int next_pos = MapPos(monster.x, monster.y);
            return next_pos;
        }
        public static int ChooseAct(int a,int b,Monster monster)
        {
            int r = monster.x - a;
            int w = monster.y - b;
            if (Math.Abs(r) < 3 && Math.Abs(w) < 3)
            {
                if (Math.Abs(r) == 1 && Math.Abs(w) == 0 || Math.Abs(r) == 0 && Math.Abs(w) == 1)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            return 0;
        }
        public static void DoMove(int a,int b)
        {
            int e = 0;
            int c = 0;
            int result=0;
            
            foreach (var pair in monsters)
            {
                int r = pair.Value.x - a;
                int w = pair.Value.y - b;

                if (Math.Abs(r) < 4 && Math.Abs(w) < 4)
                {
                    c = pair.Key;
                    if (Math.Abs(r) == 1 && Math.Abs(w) == 0 || Math.Abs(r) == 0 && Math.Abs(w) == 1)
                    {
                        e = 1;

                    }
                    else
                    {
                        e = DirectionMove(player, pair.Value);
                    }
                   
                }
            }
            if (e == 3)
            {


            }

            if (e==2)
            {
                int m;
                Monster monster2 = new Monster(monsters[c].level);
                monster2.x = monsters[c].x;
                monster2.y = monsters[c].y;
                m = MapPos(monster2.x, monster2.y);
                monsters.Remove(c);
                monsters.Add(m,monster2);
                int r = monster2.x - a;
                int w = monster2.y - b;
                if (Math.Abs(r) == 1 && Math.Abs(w) == 0 || Math.Abs(r) == 0 && Math.Abs(w) == 1)
                {
                    string info;
                    result = monster2.Attack(player, out info);
                    below_text = info;
                    if (result == -1)
                    {
                        GameOver();
                    }
                    else if (result == 1)
                    {
                        bool ret = onMonsterDead(monster2);
                        if (ret)
                        {
                            below_text += "怪物死亡。";
                            player.AddExp(monster2.level);
                            monsters.Remove(m);
                        }
                    }

                }

            }
            if (e==1)
            {
                string info;
                result = player.Attack2(monsters[c], out info);
                below_text = info;
                if (result == -1)
                {
                    GameOver();
                }
                else if (result == 1)
                {
                    bool ret = onMonsterDead(monsters[c]);
                    if (ret)
                    {
                        below_text += "怪物死亡。";
                        player.AddExp(monsters[c].level);
                        monsters.Remove(c);
                    }
                }
            }
        }//是否移动

        public static Guanka Save_guanka()
        {
           


            guanka = new Guanka(monsters, npcs, walls, equipments, princess, traps,
                doors, yaoshis, trap2s, loutis_d, loutis_u);
            return guanka;

        }
      

        // X、Y坐标转成唯一ID
        public static int MapPos(int map_x, int map_y)
        {
            return map_y * map_width + map_x;
        }

        public static void MapXY(int pos, out int map_x, out int map_y)
        {
            map_y = pos / map_width;
            map_x = pos % map_width;
        }

        // 画边界
        static void DrawBorder()
        {
            // 上边
            for (int i=0; i< map_width+2; ++i)
            {
                buffer[1,i] = '■';
            }
            /*for (int i = 0; i < map_width / 4; ++i)
            {

                buffer[6, i] = '#';

            }*/
            // 左边
            for (int i = 1; i < map_height+2; ++i)
            {
                buffer[i, 0] = '■';
            }
            
            // 下边
            for (int i = 0; i < map_width+2; ++i)
            {
                buffer[map_height+2, i] = '■';
            }
            // 右边
            for (int i = 1; i < map_height+2; ++i)
            {
                buffer[i, map_width+1] = '■';
            }
            //buffer[8, 28]= '■';
            
        }

        // 画NPC和其他东西
        static void Drawmengban()
        {
            for (int r = 2; r < map_height + 2; r++)
            {
                for (int i = 2; i < player.x + 2-4; i++)
                {
                    buffer[r, i] = '■';
                    color_buffer[r, i] = ConsoleColor.Black ;
                }
            }
             for (int r = 2; r < map_height + 2; r++)
            {
                for (int i = map_width; i>player.x + 2+4; i--)
                {
                    buffer[r, i] = '■';
                    color_buffer[r, i] = ConsoleColor.Black ;
                }
            }


        }
        static void DrawOther()
        {
            color_buffer[player.y + 2, player.x + 1] = ConsoleColor.Blue;
            buffer[player.y+2, player.x+1] = 'o';

            foreach (var pair in monsters)
            {
                char ch;
                if (pair.Value.level <= 9)
                {
                    ch = pair.Value.level.ToString()[0];
                }
                else
                {
                    ch = '!';
                }
                var monster = pair.Value;
                color_buffer[monster.y + 2, monster.x + 1] = ConsoleColor.Red;
                buffer[monster.y+2, monster.x+1] = ch;
            }
            foreach (var pair in moneys)
            {
                var money = pair.Value;
                color_buffer[money.y + 2, money.x + 1] = ConsoleColor.Yellow;
                buffer[money.y+2, money.x+1] = '$';
            }
            foreach (var pair in npcs)
            {
                var npc = pair.Value;
                color_buffer[npc.y + 2, npc.x + 1] = ConsoleColor.Green;
                buffer[npc.y+2, npc.x+1] = 'Ψ';
            }
            foreach (var pair in walls)
            {
                var wall = pair.Value;
                color_buffer[wall.y + 2, wall.x + 1] = ConsoleColor.Gray;
                buffer[wall.y + 2, wall.x + 1] = '■';
            }
            foreach (var pair in equipments)
            {
                var equipment = pair.Value;
                color_buffer[equipment.y + 2, equipment.x + 1] = ConsoleColor.Magenta;
                buffer[equipment.y + 2, equipment.x + 1] = '剑';
            }
            foreach (var pair in princess)
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.Cyan;
                buffer[princess.y + 2, princess.x + 1] = '⊙';
            }
            foreach (var pair in traps)//第一类陷阱
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.Black;
                buffer[princess.y + 2, princess.x + 1] = '^';
            }
            foreach (var pair in trap2s)//第二类陷阱
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.Black;
                buffer[princess.y + 2, princess.x + 1] = '*';
            }
            foreach (var pair in doors)
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.White;
                buffer[princess.y + 2, princess.x + 1] = '〓';
            }
            foreach (var pair in yaoshis)
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.Yellow ;
                buffer[princess.y + 2, princess.x + 1] = '♀';
            }
            foreach (var pair in loutis_d )
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.DarkYellow;
                buffer[princess.y + 2, princess.x + 1] = '↓';
            }
            foreach (var pair in loutis_u )
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.Yellow;
                buffer[princess.y + 2, princess.x + 1] = '↑';
            }
        }
        static void DrawOther2()
        {
            
            color_buffer[player.y + 2, player.x + 1] = ConsoleColor.Blue;
            buffer[player.y + 2, player.x + 1] = 'o';

            foreach (var pair in guanka.monsters)
            {
                char ch;
                if (pair.Value.level <= 9)
                {
                    ch = pair.Value.level.ToString()[0];
                }
                else
                {
                    ch = '!';
                }
                var monster = pair.Value;
                color_buffer[monster.y + 2, monster.x + 1] = ConsoleColor.Red;
                buffer[monster.y + 2, monster.x + 1] = ch;
            }
            foreach (var pair in moneys)
            {
                var money = pair.Value;
                color_buffer[money.y + 2, money.x + 1] = ConsoleColor.Yellow;
                buffer[money.y + 2, money.x + 1] = '$';
            }
            foreach (var pair in guanka.npcs)
            {
                var npc = pair.Value;
                color_buffer[npc.y + 2, npc.x + 1] = ConsoleColor.Green;
                buffer[npc.y + 2, npc.x + 1] = 'Ψ';
            }
            foreach (var pair in guanka.walls)
            {
                var wall = pair.Value;
                color_buffer[wall.y + 2, wall.x + 1] = ConsoleColor.Gray;
                buffer[wall.y + 2, wall.x + 1] = '■';
            }
            foreach (var pair in guanka.equipments)
            {
                var equipment = pair.Value;
                color_buffer[equipment.y + 2, equipment.x + 1] = ConsoleColor.Magenta;
                buffer[equipment.y + 2, equipment.x + 1] = '剑';
            }
            foreach (var pair in guanka.princess)
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.Cyan;
                buffer[princess.y + 2, princess.x + 1] = '⊙';
            }
            foreach (var pair in guanka.traps)//第一类陷阱
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.Black;
                buffer[princess.y + 2, princess.x + 1] = '^';
            }
            foreach (var pair in guanka.trap2s)//第二类陷阱
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.Black;
                buffer[princess.y + 2, princess.x + 1] = '*';
            }
            foreach (var pair in guanka.doors)
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.White;
                buffer[princess.y + 2, princess.x + 1] = '〓';
            }
            foreach (var pair in guanka.yaoshis)
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.Yellow;
                buffer[princess.y + 2, princess.x + 1] = '♀';
            }
            foreach (var pair in guanka.loutis_d)
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.DarkYellow;
                buffer[princess.y + 2, princess.x + 1] = '↓';
            }
            foreach (var pair in guanka.loutis_u)
            {
                var princess = pair.Value;
                color_buffer[princess.y + 2, princess.x + 1] = ConsoleColor.Yellow;
                buffer[princess.y + 2, princess.x + 1] = '↑';
            }
        }


        // 画第一行信息
        static void DrawInfo()
        {
            string s = string.Format("HP:{0}  LEVEL:{1}  ATK:{2}  $:{3}  ♀:{4}", player.hp, player.level, player.attack, player.money,player.yaoshi);
            for (int i=0; i<s.Length; ++i)
            {
                buffer[0, i] = s[i];
            }
        }
        static void Map_save()
        {

            FileStream fs = new FileStream("../../map2.txt", FileMode.Open, FileAccess.Read);
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
                    if (strReadline[x] == '$')
                    {
                        Money monry = new Money(100);
                        monry.SetPos(x, y);
                        Rogue.moneys [MapPos(monry.x, monry.y)] = monry;
                    }
                    if (strReadline[x] == '!')
                    {
                        Rogue.AddMonster(x, y, 1);
                    }
                    if (strReadline[x] == '8')
                    {
                        Rogue.AddMonster(x, y, 160, 100);
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
                    if (strReadline[x] == '@')
                    {
                        OldMan oldman = new OldMan("公主", 101);
                        oldman.SetPos(x, y);
                        Rogue.npcs[MapPos(oldman.x, oldman.y)] = oldman;
                    }
                    if (strReadline[x] == '&')
                    {
                        Merchant merchant2 = new Merchant("商人", 200);
                        merchant2.SetPos(x, y);
                        Rogue.npcs[MapPos(merchant2.x, merchant2.y)] = merchant2;
                    }



                }
                y += 1;
                // strReadline即为按照行读取的字符串

            }
            /*Guanka guanka1 = new Guanka(Rogue.monsters, Rogue.npcs, Rogue.walls, Rogue.equipments, Rogue.princess, Rogue.traps, Rogue.doors, Rogue.yaoshis
              , Rogue.trap2s, Rogue.loutis_d, Rogue.loutis_u);*/

        }
            // 画最下面一行的信息
            static void DrawBelowInfo()
        {
            for (int i=0;  i<below_text.Length; ++i)
            {
                buffer[map_height+3, i] = below_text[i];
            }
            for (int i=0;  i<level_target.Length; ++i)
            {
                buffer[map_height+4, i] = level_target[i];
            }
        }

        static void DrawAll()
        {
            DrawBorder();
            DrawOther();
            DrawInfo();
            Drawmengban();
            DrawBelowInfo();
        }

        public static void GameOver()
        {
            game_over = true;
            victory = false;
        }

        public static void OnStageClear()
        {
            game_over = true;
            victory = true;
        }

        public static int MonsterMove(Monster monster)
        {
            int monster_x = monster.x;
            int monster_y = monster.y;
            int m = MapPos(monster_x, monster_y);

            int act = ChooseAct(player.x, player.y, monster);
            if (act == -1)//移动
            {
                // [上，下，左，右]
                List<int> dirs = new List<int>();
                int mx, my;
                MapXY(m, out mx, out my);
                if (IsXYEmpty(mx, my - 1))
                {
                    dirs.Add(1);
                }
                if (IsXYEmpty(mx, my + 1))
                {
                    dirs.Add(2);
                }
                if (IsXYEmpty(mx - 1, my))
                {
                    dirs.Add(3);
                }
                if (IsXYEmpty(mx + 1, my))
                {
                    dirs.Add(4);
                }

                // 排除不能用的走法
                int dir = AIMove(player, monster, dirs);
                int next_pos2 = MoveNumber (dir,monster);
                MapXY(next_pos2, out monster.x, out monster.y);
                
            }
            else if (act == 1)//攻击
            {
              string s = " ";
              monster.Attack(player, out s);
              below_text = s;
            }
            return MapPos(monster.x, monster.y);
        }

        // 返回值： 是否需要刷新
        static bool MovePlayer()//移动玩家 
        {
            ConsoleKeyInfo key = Console.ReadKey();
            
            int next_x = player.x;
            int next_y = player.y;
            if (key.Key == ConsoleKey.UpArrow)
            {
                next_y -= 1;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                next_y += 1;
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                next_x -= 1;
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                next_x += 1;
            }
            else
            {
                return true;
            }

            if (next_x<0 || next_x>=map_width || next_y<0 || next_y>=map_height)
            {
                return true;
            }
            int next_pos = MapPos(next_x, next_y);

            if (moneys.ContainsKey(next_pos))//如果遇到金币
            {
                Money money = moneys[next_pos];
                below_text = "捡到 " + money.money + " 颗金币。";
                player.money += money.money;
                moneys.Remove(next_pos);
            }
            else if (loutis_d.ContainsKey(next_pos))//如果遇到往下走的楼梯
            {

                
                Rogue.monsters.Clear();
                Rogue.walls.Clear();
                Rogue.yaoshis.Clear();
                Rogue.doors.Clear();
                Rogue.trap2s.Clear();
                Rogue.traps.Clear();
                Rogue.loutis_u.Clear();
                Map_save();
                DrawOther();
            }
          
            else if (yaoshis.ContainsKey(next_pos))
            {
                Yaoshi yaoshi = yaoshis[next_pos];
                below_text = "捡到一把钥匙";
                player.yaoshi += 1;
                yaoshis.Remove(next_pos);
            }
            else if (doors.ContainsKey(next_pos))
            {
                if (player.yaoshi >= 1)
                {
                    Door door = doors[next_pos];
                    below_text = "门打开了";
                    player.yaoshi -= 1;
                    doors.Remove(next_pos);
                }
                else
                {
                    below_text = "寻找一把钥匙，方可打开此门";
                }
            }
            else if (monsters.ContainsKey(next_pos))//如果遇到怪兽
            {
                Monster monster = monsters[next_pos];
                string info;
                int result = player.Attack(monster, out info);
                below_text = info;
                if (result == -1)
                {
                    GameOver();
                    return true;
                }
                else if (result == 1)
                {
                    bool ret = onMonsterDead(monster);
                    if (ret)
                    {
                        below_text += "怪物死亡。";
                        player.AddExp(monster.level);
                        monsters.Remove(next_pos);
                    }
                }

            }
            else if (npcs.ContainsKey(next_pos))//如果遇到NPC
            {
                NPC npc = npcs[next_pos];
                string s;
                if (npc.OnTalk(player, out s))
                {
                    npcs.Remove(next_pos);
                    object o = npc.AfterDisappear();
                    if (o is Monster)
                    {
                        Monster m = o as Monster;
                        monsters[MapPos(m.x, m.y)] = m;
                    }
                }
                below_text = s;
            }
            else if (walls.ContainsKey(next_pos))//如果遇到墙
            {

            }
            else if (trap2s.ContainsKey(next_pos))//如果遇到陷阱2
            {
                Trap trap = trap2s[next_pos];
                trap2s.Remove(next_pos);
                Rogue.AddMonster(11, 8, 4);
                Rogue.AddMonster(12, 8, 4);
                Rogue.AddMonster(13, 8, 4);

            }
            else if (traps.ContainsKey(next_pos))//如果遇到陷阱
            {
                Trap trap1 = traps[next_pos];
                traps.Remove(next_pos);

                player.x = 15;
                player.y = 0;

            }
            else if (equipments.ContainsKey(next_pos))//如果遇到装备
            {
                Equipment equipment = equipments[next_pos];
                equipments.Remove(next_pos);
                player.AddAtk(100);
                object o = equipment.AfterDisappear();
                if (o is Trap)
                {
                    Trap m = o as Trap;
                    traps[MapPos(m.x, m.y)] = m;
                }
            }
            else if (princess.ContainsKey(next_pos))//如果遇到公主
            {
                Base_Princess princess1 = princess[next_pos];
                string s;
                if (princess1.OnTalk(player, out s))
                {
                    princess.Remove(next_pos);
                    object o = princess1.AfterDisappear();
                    if (o is NPC)
                    {
                        NPC m = o as NPC;
                        npcs[MapPos(m.x, m.y)] = m;
                    }
                }
                below_text = s;
            }

            else
            {
                MapXY(next_pos, out player.x, out player.y);

            }
            // 玩家行动结束


            // 所有怪物行动
            List<int> remove_keys = new List<int>();
            Dictionary<int, Monster> new_monsters = new Dictionary<int, Monster>();
            foreach (var pair in monsters)
            {
                int new_pos = MonsterMove(pair.Value);
                if (new_pos != pair.Key)
                {
                    // 更新Key
                    remove_keys.Add(pair.Key);
                    new_monsters[new_pos] = pair.Value;
                }
            }
            
            for (int i=0; i<remove_keys.Count(); ++i)
            {
                monsters.Remove(remove_keys[i]);
            }
            foreach(var new_pair in new_monsters)
            {
                monsters[new_pair.Key] = new_pair.Value;
            }

            return true;
        }

        public static int RandPos()
        {
            while (true)
            {
                int map_pos = random.Next(0, map_width * map_height);
                int x, y;
                MapXY(map_pos, out x, out y);
                if (buffer[y, x] == ' ')
                {
                    return map_pos;
                }
            }
        }
        public static int RandPos2()
        {
            while (true)
            {
                int map_pos = random.Next(0, map_width * map_height);
                int x, y;
                MapXY(map_pos, out x, out y);
                if (buffer[y, x] == ' '&& x>9 && x<21)
                {
                    return map_pos;
                }
                /*else if(buffer[y, x] == ' ' && y>8 && y<10)
                {
                    return map_pos;
                }*/
            }


        }

        static bool IsXYEmpty(int x, int y)
        {
            return IsPosEmpty(MapPos(x, y));
        }

        static bool IsPosEmpty(int pos)
        {
            if (pos < 0 || pos >= map_width * map_height)
            {
                return false;
            }
            if (monsters.ContainsKey(pos))
            {
                return false;
            }
            if (moneys.ContainsKey(pos))
            {
                return false;
            }
            if (npcs.ContainsKey(pos))
            {
                return false;
            }
            if (walls.ContainsKey(pos))
            {
                return false;
            }
            if (yaoshis.ContainsKey(pos))
            {
                return false;
            }
            if (MapPos(player.x, player.y) == pos)
            {
                return false;
            }
            return true;
            
        }

        public static Monster AddMonster(int x, int y, int _level, int money = 0)
        {
            if (!IsPosEmpty(MapPos(x,y)))
            {
                Debug.WriteLine("AddMonster错误");
                return null;
            }
            Monster m = new Monster(_level);
            m.SetPos(x, y);
            monsters[MapPos(x,y)] = m;
            m.drop_money = money;
            return m;
        }


        static void ClearStage()
        {
            // 公用变量每关重置
            game_over = false;
            victory = false;
            player.Reset();
            player.x = 15;
            player.y = Rogue.map_height - 1;

            level_target = "";
            below_text = "";

        }

        static bool StageLogic()
        {
            // 初始化图像
            canvas.ClearBuffer();
            //Console.WriteLine();
            DrawAll();
            canvas.Refresh();

            while (!game_over)
            {
                int old_level = player.level;
                bool need_refresh_move = MovePlayer();
                if (player.level > old_level)
                {
                    below_text += "升了" + (player.level - old_level) + "级";
                }

                canvas.ClearBuffer();
                //Console.WriteLine();
                DrawAll();
                canvas.Refresh();
            }

            level_target = "";
            bool ret;
            if (victory)
            {
                below_text += "\n胜利！恭喜你。";
                ret = true;
            }
            else
            {
                below_text += "\n游戏结束。";
                ret = false;
            }

            canvas.ClearBuffer();
            DrawAll();
            canvas.Refresh();

            return ret;
        }


        public static void AddLevel(InitStage init_func)
        {
            stages.Add(init_func);
        }

        static void Main(string[] args)
        {
           
            CustomLevels.AddAllLevels();
            canvas = new ConsoleCanvas(width, height);
            buffer = canvas.GetBuffer();
            color_buffer = canvas.GetColorBuffer();

            player = new Player();

            int level = 1;
            if (args.Length > 1)
            {
                level = int.Parse(args[1]);
            }
            bool game_ok = true;
            while (game_ok)
            {
                ClearStage();
                if (level-1 < stages.Count)
                {
                    stages[level-1]();
                    game_ok = StageLogic();
                }
                else
                {
                    game_ok = false;
                    below_text += "已完成全部关卡。";
                }
                if (game_ok)
                {
                    below_text += "按回车键进入下一关";
                    level += 1;
                }
                else
                {
                    below_text += "按回车键结束游戏";
                }
                canvas.ClearBuffer();
                DrawAll();
                canvas.Refresh();

                Console.ReadLine();
            }
        }
    }
}
