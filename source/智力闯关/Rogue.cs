using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;


// Rogue类是实现整个游戏基本流程的。一开始自定义关卡时，不要修改Rogue类的实现
// 建议熟悉以后再修改
namespace Rogue
{
    public class Rogue
    {
        const int width = 100;
        const int height = 28;

        public const int map_width = 30;
        public const int map_height = 15;

        static ConsoleCanvas canvas = null;
        static char[,] buffer = null;
        static ConsoleColor[,] color_buffer = null;

        public static Random random = new Random();

        public static Player player;
        public static Dictionary<int, Monster> monsters = new Dictionary<int, Monster>();
        public static Dictionary<int, Money> moneys = new Dictionary<int, Money>();
        public static Dictionary<int, NPC> npcs = new Dictionary<int, NPC>();
        public static Dictionary<int, Wall> walls = new Dictionary<int, Wall>();
        public static Dictionary<int, Equipment> equipments = new Dictionary<int, Equipment>();
        public static Dictionary<int, Prop> props = new Dictionary<int, Prop>();
        public static Dictionary<int, Gear> gears = new Dictionary<int, Gear>();
        public static Dictionary<int, Block> blocks = new Dictionary<int, Block>();

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
            for (int i = 0; i < map_width + 2; ++i)
            {
                buffer[1, i] = '■';
            }

            // 左边
            for (int i = 1; i < map_height + 2; ++i)
            {
                buffer[i, 0] = '■';
            }

            // 下边
            for (int i = 0; i < map_width + 2; ++i)
            {
                buffer[map_height + 2, i] = '■';
            }
            // 右边
            for (int i = 1; i < map_height + 2; ++i)
            {
                buffer[i, map_width + 1] = '■';
            }
        }

        // 画NPC和其他东西
        static void DrawOther()
        {
            color_buffer[player.y + 2, player.x + 1] = ConsoleColor.Yellow;
            buffer[player.y + 2, player.x + 1] = '♂';

            foreach (var pair in monsters)
            {
                char ch;
                if (pair.Value.level <= 9)
                {
                    ch = pair.Value.level.ToString()[0];
                }
                else if (pair.Value.level == 21)
                {
                    ch = '×';
                }
                else if (pair.Value.level == 34)
                {
                    ch = '⊙';
                }
                else if (pair.Value.level == 37)
                {
                    ch = '¤';
                }
                else if (pair.Value.level == 120)
                {
                    ch = '￠';
                }
                else if (pair.Value.level == 121)
                {
                    ch = '∮';
                }
                else if (pair.Value.level == 150)
                {
                    ch = '№';
                }
                else
                {
                    ch = ' ';
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
            foreach (var pair in npcs)
            {
                var npc = pair.Value;
                color_buffer[npc.y + 2, npc.x + 1] = ConsoleColor.Green;
                buffer[npc.y + 2, npc.x + 1] = '☆';
            }
            foreach (var pair in walls)
            {
                var wall = pair.Value;
                color_buffer[wall.y + 2, wall.x + 1] = ConsoleColor.DarkGray;
                buffer[wall.y + 2, wall.x + 1] = '■';
            }
            foreach (var pair in equipments)
            {
                var equipment = pair.Value;
                color_buffer[equipment.y + 2, equipment.x + 1] = ConsoleColor.Cyan;
                buffer[equipment.y + 2, equipment.x + 1] = '？';
            }
            foreach (var pair in props)
            {
                var prop = pair.Value;
                color_buffer[prop.y + 2, prop.x + 1] = ConsoleColor.Cyan;
                buffer[prop.y + 2, prop.x + 1] = '※';
            }
            foreach (var pair in gears)
            {
                var gear = pair.Value;
                color_buffer[gear.y + 2, gear.x + 1] = ConsoleColor.Cyan;
                buffer[gear.y + 2, gear.x + 1] = '∏';
            }
            foreach (var pair in blocks)
            {
                var gear = pair.Value;
                color_buffer[gear.y + 2, gear.x + 1] = ConsoleColor.Cyan;
                buffer[gear.y + 2, gear.x + 1] = '→';
            }
        }

        // 画第一行信息
        static void DrawInfo()
        {
            string s = string.Format("HP:{0}  LEVEL:{1}  ATK:{2}  $:{3}", player.hp, player.level, player.attack, player.money);
            for (int i = 0; i < s.Length; ++i)
            {
                buffer[0, i] = s[i];
            }
        }

        // 画最下面一行的信息
        static void DrawBelowInfo()
        {
            for (int i = 0; i < below_text.Length; ++i)
            {
                buffer[map_height + 3, i] = below_text[i];
            }
            for (int i = 0; i < level_target.Length; ++i)
            {
                buffer[map_height + 4, i] = level_target[i];
            }
        }

        static void DrawAll()
        {
            DrawBorder();
            DrawOther();
            DrawInfo();
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
            npcs.Clear();
            monsters.Clear();
            walls.Clear();
            gears.Clear();
            blocks.Clear();

        }

        // 返回值： 是否需要刷新
        static bool MovePlayer()
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

            if (next_x < 0 || next_x >= map_width || next_y < 0 || next_y >= map_height)
            {
                return true;
            }
            int next_pos = MapPos(next_x, next_y);

            if (moneys.ContainsKey(next_pos))
            {
                Money money = moneys[next_pos];
                below_text = "捡到 " + money.money + " 颗金币。";
                player.money += money.money;
                moneys.Remove(next_pos);
            }
            else if (equipments.ContainsKey(next_pos))
            {
                Equipment equipment = equipments[next_pos];
                below_text = "捡到" + equipment.name + "  攻击力增加" + equipment.atk;
                player.attack += equipment.atk;
                equipments.Remove(next_pos);
            }
            else if (props.ContainsKey(next_pos))
            {
                Prop prop = props[next_pos];
                below_text = "捡到" + prop.name + "  血量增加" + prop.hp;
                player.hp += prop.hp;
                props.Remove(next_pos);
            }
            else if (monsters.ContainsKey(next_pos))
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
            else if (npcs.ContainsKey(next_pos))
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
                if (next_pos == 213)
                {
                    walls.Clear();
                    int j = 1;
                    ReadMap("../../map12.txt");
                    AddBlock(9, 7);
                    for (int i = 3; i < 13; i++)
                    {
                        AddMonster(14, i, j);
                        j++;
                    }
                    for (int i = 1; i < 14; i++)
                    {
                        AddWall(10, i);
                    }
                    for (int i = 1; i < 14; i++)
                    {
                        AddWall(18, i);
                    }
                    for (int i = 11; i < 18; i++)
                    {
                        AddWall(i, 13);
                    }
                    for (int i = 11; i < 18; i++)
                    {
                        AddWall(i, 1);
                    }
                }
            }
            else if (walls.ContainsKey(next_pos))
            {
                Wall wall = walls[next_pos];
                if (next_pos == 178)
                {
                    walls.Remove(next_pos);
                }
            }
            else if (gears.ContainsKey(next_pos))
            {
                if (next_pos == 212)
                {
                    gears.Remove(next_pos);
                    ReadMap("../../map2.txt");
                }
                else if (next_pos == 185)
                {
                    gears.Remove(next_pos);
                    ReadMap("../../map3.txt");
                }
                else if (next_pos == 96)
                {
                    gears.Remove(next_pos);
                    walls.Remove(157);
                    ReadMap("../../map4.txt");
                } 
                else if (next_pos == 157)
                {
                    gears.Remove(next_pos);
                    ReadMap("../../map5.txt");
                }
                else if (next_pos == 164)
                {
                    gears.Remove(next_pos);
                    ReadMap("../../map6.txt");
                }
                else if (next_pos == 245)
                {
                    gears.Remove(next_pos);
                    ReadMap("../../map7.txt");
                }
                else if (next_pos == 427)
                {
                    gears.Remove(next_pos);
                    ReadMap("../../map8.txt");
                }
                else if (next_pos == 342)
                {
                    gears.Remove(next_pos);
                    ReadMap("../../map9.txt");
                }
                else if (next_pos == 350)
                {
                    gears.Remove(next_pos);
                    ReadMap("../../map10.txt");
                }
                else if (next_pos == 381)
                {
                    gears.Remove(next_pos);
                    ReadMap("../../map11.txt");
                }
                else if (next_pos == 299)
                {
                    gears.Remove(next_pos);
                    AddGear(29, 0);
                }
                else if (next_pos == 29)
                {
                    OnStageClear();
                }


                else if (next_pos == 182)
                {
                    if (!monsters.ContainsKey(104) && !monsters.ContainsKey(254) && !monsters.ContainsKey(314)
                            && monsters.ContainsKey(134)
                            && monsters.ContainsKey(164)
                            && monsters.ContainsKey(194)
                            && monsters.ContainsKey(224)
                            && monsters.ContainsKey(284)
                            && monsters.ContainsKey(344))
                    {
                        OnStageClear();
                    }
                    Dictionary<int, Block> temp = new Dictionary<int, Block>(blocks);
                    foreach (var bs in temp)
                    {
                        int new_pos = MapPos(bs.Value.x,bs.Value.y-1);
                        Block b = new Block();
                        b.x = bs.Value.x;
                        b.y = bs.Value.y - 1;
                        if (b.x >= 0 && b.x <= 29 && b.y >=0 && b.y <= 14)
                        {
                            blocks.Remove(bs.Key);
                            blocks[new_pos] = b;
                        }
                        if (b.x == 14 && b.y == 3)
                        {
                            monsters.Remove(104);
                        }
                        if (b.x == 14 && b.y == 4)
                        {
                            monsters.Remove(134);
                        }
                        if (b.x == 14 && b.y == 5)
                        {
                            monsters.Remove(164);
                        }
                        if (b.x == 14 && b.y == 6)
                        {
                            monsters.Remove(194);
                        }
                        if (b.x == 14 && b.y == 7)
                        {
                            monsters.Remove(224);
                        }
                        if (b.x == 14 && b.y == 8)
                        {
                            monsters.Remove(254);
                        }
                        if (b.x == 14 && b.y == 9)
                        {
                            monsters.Remove(284);
                        }
                        if (b.x == 14 && b.y == 10)
                        {
                            monsters.Remove(314);
                        }
                        if (b.x == 14 && b.y == 11)
                        {
                            monsters.Remove(344);
                        }
                    }

                }
                else if (next_pos == 242)
                {
                    if (!monsters.ContainsKey(104) && !monsters.ContainsKey(254) && !monsters.ContainsKey(314)
                            && monsters.ContainsKey(134)
                            && monsters.ContainsKey(164)
                            && monsters.ContainsKey(194)
                            && monsters.ContainsKey(224)
                            && monsters.ContainsKey(284)
                            && monsters.ContainsKey(344))
                    {
                        OnStageClear();
                    }
                    Dictionary<int, Block> temp = new Dictionary<int, Block>(blocks);
                    foreach (var bs in temp)
                    {
                        int new_pos = MapPos(bs.Value.x, bs.Value.y + 1);
                        Block b = new Block();
                        b.x = bs.Value.x;
                        b.y = bs.Value.y + 1;
                        if (b.x >= 0 && b.x <= 29 && b.y >= 0 && b.y <= 14)
                        {
                            blocks.Remove(bs.Key);
                            blocks[new_pos] = b;
                        }
                        if (b.x == 14 && b.y == 3)
                        {
                            monsters.Remove(104);
                        }
                        if (b.x == 14 && b.y == 4)
                        {
                            monsters.Remove(134);
                        }
                        if (b.x == 14 && b.y == 5)
                        {
                            monsters.Remove(164);
                        }
                        if (b.x == 14 && b.y == 6)
                        {
                            monsters.Remove(194);
                        }
                        if (b.x == 14 && b.y == 7)
                        {
                            monsters.Remove(224);
                        }
                        if (b.x == 14 && b.y == 8)
                        {
                            monsters.Remove(254);
                        }
                        if (b.x == 14 && b.y == 9)
                        {
                            monsters.Remove(284);
                        }
                        if (b.x == 14 && b.y == 10)
                        {
                            monsters.Remove(314);
                        }
                        if (b.x == 14 && b.y == 11)
                        {
                            monsters.Remove(344);
                        }
                        
                    }

                }
                else if (next_pos == 211)
                {
                    if (!monsters.ContainsKey(104) && !monsters.ContainsKey(254) && !monsters.ContainsKey(314)
                            && monsters.ContainsKey(134)
                            && monsters.ContainsKey(164)
                            && monsters.ContainsKey(194)
                            && monsters.ContainsKey(224)
                            && monsters.ContainsKey(284)
                            && monsters.ContainsKey(344))
                    {
                        OnStageClear();
                    }
                    Dictionary<int, Block> temp = new Dictionary<int, Block>(blocks);
                    foreach (var bs in temp)
                    {
                        int new_pos = MapPos(bs.Value.x-1, bs.Value.y);
                        Block b = new Block();
                        b.x = bs.Value.x-1;
                        b.y = bs.Value.y;
                        if (b.x >= 0 && b.x <= 29 && b.y >= 0 && b.y <= 14)
                        {
                            blocks.Remove(bs.Key);
                            blocks[new_pos] = b;
                        }
                        if (b.x == 14 && b.y == 3)
                        {
                            monsters.Remove(104);
                        }
                        if (b.x == 14 && b.y == 4)
                        {
                            monsters.Remove(134);
                        }
                        if (b.x == 14 && b.y == 5)
                        {
                            monsters.Remove(164);
                        }
                        if (b.x == 14 && b.y == 6)
                        {
                            monsters.Remove(194);
                        }
                        if (b.x == 14 && b.y == 7)
                        {
                            monsters.Remove(224);
                        }
                        if (b.x == 14 && b.y == 8)
                        {
                            monsters.Remove(254);
                        }
                        if (b.x == 14 && b.y == 9)
                        {
                            monsters.Remove(284);
                        }
                        if (b.x == 14 && b.y == 10)
                        {
                            monsters.Remove(314);
                        }
                        if (b.x == 14 && b.y == 11)
                        {
                            monsters.Remove(344);
                        }
                    }
                }
                else if (next_pos == 213)
                {
                    if (!monsters.ContainsKey(104) && !monsters.ContainsKey(254) && !monsters.ContainsKey(314)
                            && monsters.ContainsKey(134)
                            && monsters.ContainsKey(164)
                            && monsters.ContainsKey(194)
                            && monsters.ContainsKey(224)
                            && monsters.ContainsKey(284)
                            && monsters.ContainsKey(344))
                    {
                        OnStageClear();
                    }
                    Dictionary<int, Block> temp = new Dictionary<int, Block>(blocks);
                    foreach (var bs in temp)
                    {
                        int new_pos = MapPos(bs.Value.x+1, bs.Value.y);
                        Block b = new Block();
                        b.x = bs.Value.x+1;
                        b.y = bs.Value.y;
                        if (b.x >= 0 && b.x <= 29 && b.y >= 0 && b.y <= 14)
                        {
                            blocks.Remove(bs.Key);
                            blocks[new_pos] = b;
                        }
                        if (b.x == 14 && b.y == 3)
                        {
                            monsters.Remove(104);
                        }
                        if (b.x == 14 && b.y == 4)
                        {
                            monsters.Remove(134);
                        }
                        if (b.x == 14 && b.y == 5)
                        {
                            monsters.Remove(164);
                        }
                        if (b.x == 14 && b.y == 6)
                        {
                            monsters.Remove(194);
                        }
                        if (b.x == 14 && b.y == 7)
                        {
                            monsters.Remove(224);
                        }
                        if (b.x == 14 && b.y == 8)
                        {
                            monsters.Remove(254);
                        }
                        if (b.x == 14 && b.y == 9)
                        {
                            monsters.Remove(284);
                        }
                        if (b.x == 14 && b.y == 10)
                        {
                            monsters.Remove(314);
                        }
                        if (b.x == 14 && b.y == 11)
                        {
                            monsters.Remove(344);
                        }
                    }
                }
            }
            else
            {
                MapXY(next_pos, out player.x, out player.y);
            }

            /*Dictionary<int, Monster> temp = new Dictionary<int, Monster>(monsters);
            foreach (var ms in temp)
            {
                int new_pos = MonsterMove(ms.Value);
                if (next_pos!=ms.Key)
                {
                    monsters.Remove(ms.Key);
                    monsters[new_pos] = ms.Value;
                }
            }*/

            return true;

        }

        public static int AIMove(Player player, Monster b, List<int> dirs)
        {
            List<int> acts = new List<int>();
            for (int i = 0; i < dirs.Count(); ++i)
            {
                int d = dirs[i];

                if (d == 1)     // 上
                {
                    if (player.y < b.y)                //判断传入的 List(可以移动的方向) 然后将其增加到新建的 List（随机选择一个方向移动）
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

        public static int ChooseAct(int a, int b, Monster monster)//怪物 选择动作
        {
            int r = monster.x - a;
            int w = monster.y - b;
            if (Math.Abs(r) < 4 && Math.Abs(w) < 4) //如果玩家与怪物距离小于4  执行
            {

                if ((Math.Abs(r) == 1 && Math.Abs(w) == 0) || (Math.Abs(r) == 0 && Math.Abs(w) == 1))
                {
                    return 1;   // 如果怪物就在玩家四周 返回 1

                }
                else
                {
                    return -1;  // 否则 返回 -1
                }

            }
            return 0;//  都不符合 返回 0


        }

        public static int MonsterMove(Monster monster)//******************怪物移动
        {
            int monster_x = monster.x;
            int monster_y = monster.y;
            int m = MapPos(monster_x, monster_y);  // 将怪物的唯一坐标赋给 m

            int act = ChooseAct(player.x, player.y, monster);  // 将怪物选择动作的返回值 赋给 act
            if (act == -1)//移动
            {
                // [上，下，左，右]
                List<int> dirs = new List<int>();
                int mx, my;
                MapXY(m, out mx, out my);
                if (IsXYEmpty(mx, my - 1))  // 如果能往上移动 即上面无对象 执行 
                {
                    dirs.Add(1);
                }
                if (IsXYEmpty(mx, my + 1)) //
                {
                    dirs.Add(2);
                }
                if (IsXYEmpty(mx - 1, my)) //
                {
                    dirs.Add(3);
                }
                if (IsXYEmpty(mx + 1, my)) //
                {
                    dirs.Add(4);
                }

                // 排除不能用的走法
                int dir = AIMove(player, monster, dirs);
                int b;

                if (dir == 1)
                {
                    b = MapPos(monster_x, monster_y - 1);
                }
                else if (dir == 2)
                {
                    b = MapPos(monster_x, monster_y + 1);
                }
                else if (dir == 3)
                {
                    b = MapPos(monster_x - 1, monster_y);
                }
                else if (dir == 4)
                {
                    b = MapPos(monster_x + 1, monster_y);
                }
                else
                {
                    b = MapPos(monster_x, monster_y);
                }

                int next_pos2 = b;

                if (npcs.ContainsKey(next_pos2))
                {
                    MapXY(m, out monster_x, out monster_y);
                }
                else if (monsters.ContainsKey(next_pos2))
                {
                    MapXY(m, out monster_x, out monster_y);
                }
                else
                {
                    MapXY(next_pos2, out monster.x, out monster.y);
                }

            }
            else if (act == 1)//攻击
            {

            }
            else if (act == 0)//什么事都不敢
            {

            }

            return MapPos(monster.x, monster.y);
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
            if (MapPos(player.x, player.y) == pos)
            {
                return false;
            }
            return true;
        }

        public static void ReadMap(string s)
        {
            FileStream fs = new FileStream(s, FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline;
            int y = 0;
            while ((strReadline = read.ReadLine()) != null)
            {
                //Console.WriteLine(strReadline);

                for (int x = 0; x < strReadline.Length; ++x)
                {
                    if (strReadline[x] == '#')
                    {
                        Rogue.AddWall(x, y);
                    }
                    else if (strReadline[x] == 'j')
                    {
                        Rogue.AddGear(x, y);
                    }
                }
                y += 1;

            }

            fs.Close();

            read.Close();
        }

        public static Monster AddMonster(int x, int y, int _level, int money = 0)//添加怪物
        {
            if (!IsPosEmpty(MapPos(x, y)))
            {
                Debug.WriteLine("AddMonster错误");
                return null;
            }
            Monster m = new Monster(_level);
            m.SetPos(x, y);//怪物坐标
            monsters[MapPos(x, y)] = m;//将怪物M的引用赋给字典monsters x*y键对应的键值；
            m.drop_money = money;//怪物掉落的金币数
            return m;
        }

        public static Wall AddWall(int x, int y)//添加墙壁
        {
            if (!IsPosEmpty(MapPos(x, y)))
            {
                Debug.WriteLine("AddWall错误");
                return null;
            }
            Wall w = new Wall();
            w.SetPos(x, y);//墙体坐标
            walls[MapPos(x, y)] = w;
            return w;
        }

        public static Gear AddGear(int x, int y)//添加墙壁
        {
            if (!IsPosEmpty(MapPos(x, y)))
            {
                Debug.WriteLine("AddGear错误");
                return null;
            }
            Gear g = new Gear();
            g.SetPos(x, y);
            gears[MapPos(x, y)] = g;
            return g;
        }

        public static Block AddBlock(int x, int y)//添加墙壁
        {
            if (!IsPosEmpty(MapPos(x, y)))
            {
                Debug.WriteLine("AddBlock错误");
                return null;
            }
            Block g = new Block();
            g.SetPos(x, y);
            blocks[MapPos(x, y)] = g;
            return g;
        }


        static void ClearStage()
        {
            // 公用变量每关重置
            game_over = false;
            victory = false;

            player.Reset();
            player.x = 0;
            player.y = 7;

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
