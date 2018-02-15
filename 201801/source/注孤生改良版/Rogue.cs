using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace 注孤生改良版
{

    public class Rogue
    {
        //缓存范围大小
        const int width = 50;
        const int height = 50;
        //地图大小
        public const int map_width = 26;
        public const int map_height = 26;
        //颜色控制
        static ConsoleCanvas canvas = null;
        static char[,] buffer = null;
        static ConsoleColor[,] color_buffer = null;
        //公共变量
        static void ClearStage()
        {
            // 公用变量每关重置
            game_over = false;
            victory = false;
            player.Reset();

            level_target = "";
            below_text = "";

        }
        //玩家
        public static Player player;   
        //怪兽
        public static Dictionary<int, Monster> monsters = new Dictionary<int, Monster>();
        //NPC
        public static Dictionary<int, NPC> npcs = new Dictionary<int, NPC>();
        //钥匙
        public static Dictionary<int, Key> keys = new Dictionary<int, Key>();
        //机关  
        public static Dictionary<int, Thorn> thorns = new Dictionary<int, Thorn>();
        //门
        public static Dictionary<int, Door> doors = new Dictionary<int, Door>();
        //电门
        public static Dictionary<int, ElectricityDoor> electricitydoors = new Dictionary<int, ElectricityDoor>();
        //电闸
        public static Dictionary<int, ElectricSwitch> electricSwitchs = new Dictionary<int, ElectricSwitch>();
        //圣剑
        public static Dictionary<int, LongPole> longpoles = new Dictionary<int, LongPole>();
        //传送
        public static Dictionary<int, Transfer> transfers = new Dictionary<int, Transfer>();
        //墙
        public static Dictionary<int, Wall> walls = new Dictionary<int, Wall>();
        // 委托，用于扩展关卡用。自定义关卡初始化函数
        public delegate void InitStage();
        static List<InitStage> stages = new List<InitStage>();
        // 委托，用于自定义怪物死亡时处理    
        public delegate bool OnMonsterDead(Monster monster);
        public static OnMonsterDead onMonsterDead = null;
        //判断是否移动
        public static string below_text = "";
        public static string level_target = "";
        //成功与失败
        public static bool game_over;
        public static bool victory;
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
        //迷雾
        static void Drawmengban()
        {
            for (int i = 2; i < map_height ; i++)
            {
                for (int j = 1; j < map_width - 1; j++)
                {
                    int d =(Math.Abs(j - 1 - player.x)+ Math.Abs(i - 2 - player.y));

                    if (d > 5)
                    {
                        buffer[i, j] = '■';
                        color_buffer[i, j] = ConsoleColor.Black;
                    }
                }
            }
        }
        // 画边界
        static void DrawBorder()
        {
            // 上边
            for (int i = 0; i < map_width; ++i)
            {
                buffer[1, i] = '□';
            }
            // 左边
            for (int i = 1; i < map_height + 1; ++i)
            {
                buffer[i, 0] = '□';
            }
            // 下边
            for (int i = 0; i < map_width; ++i)
            {
                buffer[map_height, i] = '□';
            }
            // 右边
            for (int i = 1; i < map_height + 1; ++i)
            {
                buffer[i, map_width - 1] = '□';
            }
        }
        //画角色
        static void DrawRole() 
        {
            //玩家
            color_buffer[player.y + 2, player.x + 1] = ConsoleColor.Cyan;
            buffer[player.y + 2, player.x + 1] = player.name[0];
            //NPC
            foreach (var pair in npcs)
            {
                if (pair.Value.name == "撒贝宁")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.DarkYellow;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            foreach (var pair in npcs)
            {
                if (pair.Value.name == "何炅")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.DarkGray;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            foreach (var pair in npcs)
            {
                if (pair.Value.name == "恶魔")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.Red;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            //怪兽
            foreach (var pair in monsters)
            {
                if (pair.Value.name == "〓")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.Red;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            foreach (var pair in monsters)
            {
                if (pair.Value.name == "☆")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.Red;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            foreach (var pair in monsters)
            {
                if (pair.Value.name == "★")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.Yellow;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            foreach (var pair in monsters)
            {
                if (pair.Value.name == "○")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.Blue;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            foreach (var pair in monsters)
            {
                if (pair.Value.name == "●")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.Green;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            foreach (var pair in monsters)
            {
                if (pair.Value.name == "◇")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.Cyan;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            foreach (var pair in monsters)
            {
                if (pair.Value.name == "◆")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.Magenta;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            //钥匙
            foreach (var pair in keys)
            {
                var key = pair.Value;
                color_buffer[key.y+2, key.x+1] = ConsoleColor.White;
                buffer[key.y+2, key.x+1] = key.name[0];
            }
            //地刺
            foreach (var pair in thorns)
            {
                var thorn = pair.Value;
                color_buffer[thorn.y+2, thorn.x+1] = ConsoleColor.DarkMagenta;
                buffer[thorn.y+2, thorn.x+1] =thorn.name[0];
            }
            //门
            foreach (var pair in doors)
            {
                var door = pair.Value;
                color_buffer[door.y+2, door.x+1] = ConsoleColor.White;
                buffer[door.y+2, door.x+1] = door.name[0];
            }
            //电门
            foreach (var pair in electricitydoors)
            {
                var electricitydoor = pair.Value;
                color_buffer[electricitydoor.y+2, electricitydoor.x+1] = ConsoleColor.Yellow;
                buffer[electricitydoor.y+2, electricitydoor.x+1] = electricitydoor.name[0];
            }
            //电闸
            foreach (var pair in electricSwitchs)
            {
                var electricSwitch = pair.Value;
                color_buffer[electricSwitch.y+2, electricSwitch.x+1] = ConsoleColor.Yellow;
                buffer[electricSwitch.y+2, electricSwitch.x+1] = electricSwitch.name[0];
            }
            //长杆
            foreach (var pair in longpoles)
            {
                var longpole = pair.Value;
                color_buffer[longpole.y+2, longpole.x+1] = ConsoleColor.DarkMagenta;
                buffer[longpole.y+2, longpole.x+1] = longpole.name[0];
            }
            //传送
            foreach (var pair in transfers)
            {
                if (pair.Value.name == "◎")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.DarkRed;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            foreach (var pair in transfers)
            {
                if (pair.Value.name == "¤")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.DarkCyan;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            foreach (var pair in transfers)
            {
                if (pair.Value.name == "§")
                {
                    color_buffer[pair.Value.y + 2, pair.Value.x + 1] = ConsoleColor.DarkCyan;
                    buffer[pair.Value.y + 2, pair.Value.x + 1] = pair.Value.name[0];
                }
            }
            //墙
            foreach (var pair in walls)
            {
                var walls = pair.Value;
                color_buffer[walls.y + 2, walls.x + 1] = ConsoleColor.Gray;
                buffer[walls.y + 2, walls.x + 1] = walls.name[0];
            }
        }
        // 画第一行信息
        static void DrawInfo()
        {
            string s = string.Format("救出你的女朋友，生命只有一次，且行且珍惜");
            for (int i = 0; i < s.Length; ++i)
            {
                buffer[0, i] = s[i];
            }
        }
        //画最后一行信息
        static void DrawBelowInfo()
        {
            for (int i = 0; i < below_text.Length; ++i)
            {
                buffer[map_height + 3, i] = below_text[i];
            }
            for (int i = 0; i < level_target.Length; ++i)
            {
                buffer[map_height +1, i] = level_target[i];
            }
        }
        //画所有
        static void DrawAll()
        {
            DrawBorder();
            DrawRole();
            DrawInfo();
            Drawmengban();
            DrawBelowInfo();
        }
        // 返回值： 是否需要刷新
        static bool MovePlayer()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

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
            if (next_x < 0 || next_x >= map_width-2 || next_y < 0 || next_y >= map_height-2)
            {
                return true;
            }
            int next_pos = MapPos(next_x, next_y);
            if (keys.ContainsKey(next_pos))
            {
                Key key1 = keys[next_pos];
                player.bag.Add(key1);
                string s;
                if (key1.OnTalk(player, out s))
                {
                    npcs.Remove(next_pos);
                    object o = key1.AfterDisappear();
                    if (o is Monster)
                    {
                        Monster m = o as Monster;
                        monsters[MapPos(m.x, m.y)] = m;
                    }
                }
                below_text = s;
                keys.Remove(next_pos);
            }
            else if (doors.ContainsKey(next_pos))
            {
                Door door = doors[next_pos];
                string s;
                if (door.OnTalk(player, out s))
                {
                    //npcs.Remove(next_pos);
                    object o = door.AfterDisappear();
                    if (o is Monster)
                    {
                        Monster m = o as Monster;
                        monsters[MapPos(m.x, m.y)] = m;
                    }
                }
                below_text = s;
                for (int n = 0; n < player.bag.Count; n++)
                {
                    if (player.bag[n].id == doors[next_pos].id)
                    {
                        player.bag.RemoveAt(n);
                        doors.Remove(next_pos);
                        break;
                    }
                }
                return false;
            }
            if (electricSwitchs.ContainsKey(next_pos))
            {
                
                ElectricSwitch electricSwitch = electricSwitchs[next_pos];
                player.bags.Add(electricSwitch);
                string s;
                if (electricSwitch.OnTalk(player, out s))
                {
                    npcs.Remove(next_pos);
                    object o = electricSwitch.AfterDisappear();
                    if (o is Monster)
                    {
                        Monster m = o as Monster;
                        monsters[MapPos(m.x, m.y)] = m;
                    }
                }
                below_text = s;
                electricSwitchs.Remove(next_pos);
            }
            else if (electricitydoors.ContainsKey(next_pos))
            {
                ElectricityDoor electricitydoor = electricitydoors[next_pos];
                string s;
                if (electricitydoor.OnTalk(player, out s))
                {
                    //npcs.Remove(next_pos);
                    object o = electricitydoor.AfterDisappear();
                    if (o is Monster)
                    {
                        Monster m = o as Monster;
                        monsters[MapPos(m.x, m.y)] = m;
                    }
                }
                below_text = s;
                for (int n = 0; n < player.bags.Count; n++)
                {
                    if (player.bags[n].id == electricitydoors[next_pos].id) { electricSwitchs.Remove(next_pos); electricitydoors.Remove(next_pos); }
                }
                return false;
            }
            if (walls.ContainsKey(next_pos))
            {
                return false;
            }
            else if (monsters.ContainsKey(next_pos))
            {
                Monster monster = monsters[next_pos];
                string s;
                if (monster.OnTalk(player, out s))
                {
                    //npcs.Remove(next_pos);
                    object o = monster.AfterDisappear();
                    if (o is Monster)
                    {
                        Monster m = o as Monster;
                        monsters[MapPos(m.x, m.y)] = m;
                    }
                }
                below_text = s;
                game_over = true;
                return false;
            }
            else if (longpoles.ContainsKey(next_pos))
            {
                LongPole longPole = longpoles[next_pos];
                player.bagss.Add(longPole);
                string s;
                if (longPole.OnTalk(player, out s))
                {
                    object o = longPole.AfterDisappear();
                    if (o is Monster)
                    {
                        Monster m = o as Monster;
                        monsters[MapPos(m.x, m.y)] = m;
                    }
                }
                longpoles.Remove(next_pos);
                below_text = s;
                return false;
            }
            if (transfers.ContainsKey(next_pos))
            {
                Transfer transfer = transfers[next_pos];
                string s;
                if (transfer.OnTalk(player, out s))
                {
                    object o = transfer.AfterDisappear();
                    if (o is Monster)
                    {
                        Monster m = o as Monster;
                        monsters[MapPos(m.x, m.y)] = m;
                    }
                }
                below_text = s;
                if (transfers[next_pos].name == "¤")
                {
                    player.x = 21;
                    player.y = 23;
                }
                else if (transfers[next_pos].name == "§")
                {
                    player.x = 12;
                    player.y = 17;
                }
                else if(transfers[next_pos].name == "◎")
                {
                    player.x = 3;
                    player.y = 0;
                }
                return false;
            }
            else if (npcs.ContainsKey(next_pos))
            {
                Key newkey = new Key("1",1);
                player.bag.Add(newkey);
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
                //{ npcs.Remove(next_pos); }
                return true;
            }
            else
            {
                int a, b;
                MapXY(next_pos, out a, out b);
                player.x = a;
                player.y = b;
            }

            return true;
        }
        //增加关卡
        //public static void AddLevel(InitStage init_func)
        //{
        //    stages.Add(init_func);
        //}
        ////增加怪兽
        //public static Monster AddMonster(int x, int y)
        //{
        //    if (!IsPosEmpty(MapPos(x, y)))
        //    {
        //        Debug.WriteLine("AddMonster错误");
        //        return null;
        //    }
        //    Monster m = new Monster();
        //    m.SetPos(x, y);
        //    monsters[MapPos(x, y)] = m;
        //    return m;
        //}
        ////增加门
        //public static Door Adddoor(int x, int y)
        //{
        //    if (!IsPosEmpty(MapPos(x, y)))
        //    {
        //        Debug.WriteLine("Adddoor错误");
        //        return null;
        //    }
        //    Door n = new Door();
        //    n.SetPos(x, y);
        //    doors[MapPos(x, y)] = n;
        //    return n;
        //}
        //增加钥匙
        //public static Key Addkey(int x, int y)
        //{
        //    if (!IsPosEmpty(MapPos(x, y)))
        //    {
        //        Debug.WriteLine("Addkey错误");
        //        return null;
        //    }
        //    Key l = new Key();
        //    l.SetPos(x, y);
        //    keys[MapPos(x, y)] = l;
        //    return l;
        //}
        //增加机关
        //public static Thorn Addthorns(int x, int y)
        //{
        //    if (!IsPosEmpty(MapPos(x, y)))
        //    {
        //        Debug.WriteLine("Addkey错误");
        //        return null;
        //    }
        //    Thorn k = new Thorn();
        //    k.SetPos(x, y);
        //    thorns[MapPos(x, y)] = k;
        //    return k;
        //}
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
            if (npcs.ContainsKey(pos))
            {
                return false;
            }
            if (MapPos(player.x, player.y) == pos)
            {
                return false;
            }
            if (keys.ContainsKey(pos))
            {
                return false;
            }
            if (thorns.ContainsKey(pos))
            {
                return false;
            }
            if (electricitydoors.ContainsKey(pos))
            {
                return false;
            }
            if (electricSwitchs.ContainsKey(pos))
            {
                return false;
            }
            if (longpoles.ContainsKey(pos))
            {
                return false;
            }
            return true;
        }
        //游戏结束
        public static void GameOver()
        {
            game_over = true;
            victory = false;
        }
        //static bool StageLogic()
        //{
        //    // 初始化图像
        //    canvas.ClearBuffer();
        //    //Console.WriteLine();
        //    DrawAll();
        //    canvas.Refresh();

        //    while (!game_over)
        //    {
        //        int old_level = player.level;
        //        bool need_refresh_move = MovePlayer();
        //        if (player.level > old_level)
        //        {
        //            below_text += "升了" + (player.level - old_level) + "级";
        //        }

        //        canvas.ClearBuffer();
        //        //Console.WriteLine();
        //        DrawAll();
        //        canvas.Refresh();
        //    }

        //    level_target = "";
        //    bool ret;
        //    if (victory)
        //    {
        //        below_text += "\n胜利！恭喜你。";
        //        ret = true;
        //    }
        //    else
        //    {
        //        below_text += "\n游戏结束。";
        //        ret = false;
        //    }

        //    canvas.ClearBuffer();
        //    DrawAll();
        //    canvas.Refresh();

        //    return ret;
        //}
        //游戏胜利

        //游戏胜利
        //public static void OnStageClear()
        //{
        //    game_over = false;
        //    victory = true;
        //}

        static void Main(string[] args)
        {
            get1:
            canvas = new ConsoleCanvas(width, height);
            buffer = canvas.GetBuffer();
            color_buffer = canvas.GetColorBuffer();

            player = new Player();//21,21,    
            player.x = 21;
            player.y = 21;

            //CustomOne.InitLevel1();
            MapCreate mapCreate = new MapCreate();
            
            mapCreate.PosRead(mapCreate.Mapchoose(1));

            canvas.ClearBuffer_DoubleBuffer();
            DrawAll();
            canvas.Refresh_DoubleBuffer();
            while (true)
            {
                //ClearStage();
                bool need_refresh_move = MovePlayer();
                canvas.ClearBuffer_DoubleBuffer();
                //Console.WriteLine();
                DrawAll();
                canvas.Refresh_DoubleBuffer();
                if (game_over)
                {
                    Console.Clear();
                    Console.WriteLine("你根本没有女朋友，注孤生啊！注孤生！");
                    Console.ReadKey();
                    game_over = false;
                    goto get1;
                    break;

                }
                else if (victory)
                {
                    Console.Clear();
                    Console.WriteLine("你成功救出了你的女朋友~");
                    Console.ReadKey();
                    victory = true;
                    break;
                }
            }
            Console.ReadLine();
        }
        public class MapCreate
        {
            public class Container//集合
            {
                public Dictionary<int, Wall> walls = new Dictionary<int, Wall>();//墙
            }
            public string[] Mapchoose(int levelnumb)
            {
                string[] array = new string[26];
                switch (levelnumb)
                {
                    case 1:
                        array = File.ReadAllLines(@"地图.txt");
                        break;
                }
                return array;
            }//地图文件读取（关卡数-地图）
            public char[,] PosRead(string[] levelmap)
            {
                List<char[]> charmap = new List<char[]>();
                char[,] mappos = new char[26, 26];
                string[] array = new string[levelmap.Length - 2];
                for (int i=0; i< array.Length; i++)
                {
                    array[i] = levelmap[i + 1];
                }
                
                int height = 0;
                foreach (var s in array)
                {
                    char[] a = new char[s.Length-2];
                    for (int i=0; i<a.Length; i++)
                    {
                        a[i] = s[i + 1];
                    }
                    charmap.Add(a);//char数组里面增加了一串横的坐标里的内容
                    int length = 0;//横坐标
                    for (length = 0; length < 24 && a.Length > length; length++)
                    {
                        mappos[height, length] = a[length];
                        if (a[length] == '#')
                        {
                            Wall wall = new Wall("□", 1001);
                            wall.SetPos(length, height);
                            Rogue.walls[MapPos(wall.x, wall.y)] = wall;
                        }
                        if (a[length] == 'o')
                        {
                            ElectricityDoor electricitydoor = new ElectricityDoor("×", 8);
                            electricitydoor.SetPos(length, height);
                            Rogue.electricitydoors[MapPos(electricitydoor.x, electricitydoor.y)] = electricitydoor;
                        }
                        if (a[length] == 'y')
                        {
                            Thorn thorn = new Thorn("◆", 5001);
                            thorn.SetPos(length, height);
                            Rogue.thorns[MapPos(thorn.x, thorn.y)] = thorn;
                        }
                        if (a[length] == 'w')
                        {
                            ElectricSwitch electricSwitch = new ElectricSwitch("※", 8);
                            electricSwitch.SetPos(length, height);
                            Rogue.electricSwitchs[MapPos(electricSwitch.x, electricSwitch.y)] = electricSwitch;
                        }
                        if (a[length] == 'u')
                        {
                            LongPole longPole = new LongPole("♂", 1);
                            longPole.SetPos(length, height);
                            Rogue.longpoles[MapPos(longPole.x, longPole.y)] = longPole;
                        }
                        if (a[length] == 'i')
                        {
                            Transfer transfer1 = new Transfer("¤", 1);
                            transfer1.SetPos(length, height);
                            Rogue.transfers[MapPos(transfer1.x, transfer1.y)] = transfer1;
                        }
                        if (a[length] == 'l')
                        {
                            Transfer transfer2 = new Transfer("§", 2);
                            transfer2.SetPos(length, height);
                            Rogue.transfers[MapPos(transfer2.x, transfer2.y)] = transfer2;
                        }
                        if (a[length] == ',')
                        {
                            Transfer transfer3 = new Transfer("◎", 1);
                            transfer3.SetPos(length, height);
                            Rogue.transfers[MapPos(transfer3.x, transfer3.y)] = transfer3;
                        }
                        if (a[length] == '.')
                        {
                            Transfer transfer4 = new Transfer("◎", 1);
                            transfer4.SetPos(length, height);
                            Rogue.transfers[MapPos(transfer4.x, transfer4.y)] = transfer4;
                        }
                        if (a[length] == 'p')
                        {
                            NPC oldman1 = new NPC("何炅", 1);
                            oldman1.SetPos(length, height);
                            Rogue.npcs[MapPos(oldman1.x, oldman1.y)] = oldman1;
                        }
                        if (a[length] == 'q')
                        {
                            NPC oldman2 = new NPC("恶魔", 2);
                            oldman2.SetPos(length, height);
                            Rogue.npcs[MapPos(oldman2.x, oldman2.y)] = oldman2;
                        }
                        if (a[length] == 'v')
                        {
                            NPC oldman3 = new NPC("撒贝宁", 3);
                            oldman3.SetPos(length, height);
                            Rogue.npcs[MapPos(oldman3.x, oldman3.y)] = oldman3;
                        }
                        if (a[length] == 'r')
                        {
                            Monster monster1 = new Monster("〓", 1, 5, 1);
                            monster1.SetPos(length, height);
                            Rogue.monsters[MapPos(monster1.x, monster1.y)] = monster1;
                        }
                        if (a[length] == '1')
                        {
                            Key key1 = new Key("Ⅰ", 1);
                            key1.SetPos(length, height);
                            Rogue.keys[MapPos(key1.x, key1.y)] = key1;
                        }
                        if (a[length] == '2')
                        {
                            Key key2 = new Key("Ⅱ", 2);
                            key2.SetPos(length, height);
                            Rogue.keys[MapPos(key2.x, key2.y)] = key2;
                        }
                        if (a[length] == '3')
                        {
                            Key key3 = new Key("Ⅲ", 3);
                            key3.SetPos(length, height);
                            Rogue.keys[MapPos(key3.x, key3.y)] = key3;
                        }
                        if (a[length] == '4')
                        {
                            Key key4 = new Key("Ⅳ", 4);
                            key4.SetPos(length, height);
                            Rogue.keys[MapPos(key4.x, key4.y)] = key4;
                        }
                        if (a[length] == '5')
                        {
                            Key key5 = new Key("Ⅴ", 5);
                            key5.SetPos(length, height);
                            Rogue.keys[MapPos(key5.x, key5.y)] = key5;
                        }
                        if (a[length] == '6')
                        {
                            Key key6 = new Key("Ⅵ", 6);
                            key6.SetPos(length, height);
                            Rogue.keys[MapPos(key6.x, key6.y)] = key6;
                        }
                        if (a[length] == '7')
                        {
                            Key key7 = new Key("Ⅶ", 7);
                            key7.SetPos(length, height);
                            Rogue.keys[MapPos(key7.x, key7.y)] = key7;
                        }
                        if (a[length] == 'a')
                        {
                            Door door1 = new Door("①", 1);
                            door1.SetPos(length, height);
                            Rogue.doors[MapPos(door1.x, door1.y)] = door1;
                        }
                        if (a[length] == 'b')
                        {
                            Door door2 = new Door("②", 2);
                            door2.SetPos(length, height);
                            Rogue.doors[MapPos(door2.x, door2.y)] = door2;
                        }
                        if (a[length] == 'c')
                        {
                            Door door3 = new Door("③", 3);
                            door3.SetPos(length, height);
                            Rogue.doors[MapPos(door3.x, door3.y)] = door3;
                        }
                        if (a[length] == 'd')
                        {
                            Door door4 = new Door("④", 4);
                            door4.SetPos(length, height);
                            Rogue.doors[MapPos(door4.x, door4.y)] = door4;
                        }
                        if (a[length] == 'e')
                        {
                            Door door5 = new Door("⑤", 5);
                            door5.SetPos(length, height);
                            Rogue.doors[MapPos(door5.x, door5.y)] = door5;
                        }
                        if (a[length] == 'f')
                        {
                            Door door6 = new Door("⑥", 6);
                            door6.SetPos(length, height);
                            Rogue.doors[MapPos(door6.x, door6.y)] = door6;
                        }
                        if (a[length] == 'g')
                        {
                            Door door7 = new Door("⑦", 7);
                            door7.SetPos(length, height);
                            Rogue.doors[MapPos(door7.x, door7.y)] = door7;
                        }
                        if (a[length] == 'h') 
                        {
                            Monster monster10 = new Monster("★", 1, 5, 1);
                            monster10.SetPos(length, height);
                            Rogue.monsters[MapPos(monster10.x, monster10.y)] = monster10;
                        }
                        if (a[length] == 'j') 
                        {
                            Monster monster11 = new Monster("☆", 1, 5, 1);
                            monster11.SetPos(length, height);
                            Rogue.monsters[MapPos(monster11.x, monster11.y)] = monster11;
                        }
                        if (a[length] == 'k')
                        {
                            Monster monster12 = new Monster("○", 1, 5, 1);
                            monster12.SetPos(length, height);
                            Rogue.monsters[MapPos(monster12.x, monster12.y)] = monster12;
                        }
                        if (a[length] == 't')
                        {
                            Monster monster13 = new Monster("●", 1, 5, 1);
                            monster13.SetPos(length, height);
                            Rogue.monsters[MapPos(monster13.x, monster13.y)] = monster13;
                        }
                        if (a[length] == 'm')
                        {
                            Monster monster14 = new Monster("◇", 1, 5, 1);
                            monster14.SetPos(length, height);
                            Rogue.monsters[MapPos(monster14.x, monster14.y)] = monster14;
                        }
                        if (a[length] == 'n')
                        {
                            Monster monster15 = new Monster("◆", 1, 5, 1);
                            monster15.SetPos(length, height);
                            Rogue.monsters[MapPos(monster15.x, monster15.y)] = monster15;
                        }
                    }
                    height++;
                }
                return mappos;
            }
//打印地图
            //public Container ReadChar(char[,] mappos)
            //{
            //    Container container = new Container();
            //    for (int n = 0; n < 26; n++)
            //    {
            //        for (int m = 0; m < 26; m++)
            //        {
            //            if (mappos[n, m] == '#')
            //            {
            //                Wall wall = new Wall("█",1);
            //                container.walls.Add(MapPos(n, m), wall);
            //            }
            //        }
            //        Console.WriteLine();
            //    }
            //    return container;
            //读取每一个点坐标的物品，加入相应字典}
        }

    }
}
