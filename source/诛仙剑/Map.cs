using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGame
{
    class Player
    {
        public int hp = 10;
        public int level = 1;
        public int attack = 1;

        public int money = 0;

        public int x;
        public int y;
    }

    class Monster
    {
        public int hp = 10;
        public int attack = 1;

        public int x;
        public int y;
    }

    class Money
    {
        public int money = 0;

        public int x;
        public int y;

        public int id = 0;
    }

    class NPC
    {
        public string name = "NPC";
    }
}

namespace ConsoleGame
{
    class TimeEvent
    {
        public string event_type;
        public int event_id;

        public TimeEvent(string t, int id=0)
        {
            event_type = t;
            event_id = id;
        }
    }

    class Map
    {
        const int width = 120;
        const int height = 29;

        const int map_width = 60;
        const int map_height = 25;

        static char[] buffer = null;

        static int time = 0;

        static Random random = new Random();

        static Player player = new Player();
        static Dictionary<int, Monster> monsters = new Dictionary<int, Monster>();
        static Dictionary<int, Money> moneys = new Dictionary<int, Money>();
        static Dictionary<int, NPC> npcs = new Dictionary<int, NPC>();

        static string below_text = "";

        // X、Y坐标转成buffer下标
        static int Pos(int x, int y)
        {
            return width * y + x;
        }

        // Buffer下标转成x和y，注意这里用了out参数
        static void XY(int pos, out int x, out int y)
        {
            x = pos % width;
            y = pos / width;
            return;
        }

        // 地图pos(从0到 (width-2)*(height-2)-1 )转buffer下标
        static int MapPosToPos(int map_pos)
        {
            int row = map_pos / (map_width - 2);
            int col = map_pos % (map_width - 2);

            int pos = (row + 2) * width + (col + 1);
            return pos;
        }
        
        // 画边界
        static void DrawBorder()
        {
            // 上边
            for (int i=0; i< map_width; ++i)
            {
                buffer[width+i] = '#';
            }

            // 左边
            for (int i = 0; i < map_height; ++i)
            {
                buffer[(i+1)*width] = '#';
            }
            
            // 下边
            for (int i = 0; i < map_width; ++i)
            {
                buffer[(map_height)* width + i] = '#';
            }
            // 右边
            for (int i = 0; i < map_height; ++i)
            {
                buffer[(i+1) * width + (map_width-1)] = '#';
            }
        }

        // 画NPC和其他东西
        static void DrawOther()
        {
            buffer[Pos(player.x, player.y)] = 'o';
            foreach (var pair in monsters)
            {
                buffer[pair.Key] = 'M';
            }
            foreach (var pair in moneys)
            {
                buffer[pair.Key] = '$';
            }
            foreach (var pair in npcs)
            {
                buffer[pair.Key] = 'N';
            }
        }

        // 画第一行信息
        static void DrawInfo()
        {
            string s = string.Format("HP:{0}  LEVEL:{1}  ATK:{2}  $:{3}  TIME:{4}", player.hp, player.level, player.attack, player.money, Timer.GetTimeStamp() - Timer.begin_time);
            for (int i=0; i<s.Length; ++i)
            {
                buffer[i] = s[i];
            }
        }

        // 画最下面一行的信息
        static void DrawBelowInfo()
        {
            int beg = (1 + map_height) * width;
            for (int i=0;  i<below_text.Length; ++i)
            {
                buffer[beg+i] = below_text[i];
            }
        }

        static void DrawAll()
        {
            DrawBorder();
            DrawOther();
            DrawInfo();
            DrawBelowInfo();
        }

        // 返回值： 是否需要刷新
        static bool MovePlayer()
        {
            ConsoleKeyInfo key = Console.ReadKey();

            int next_pos = -1;
            if (key.Key == ConsoleKey.UpArrow)
            {
                next_pos = Pos(player.x, player.y-1);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                next_pos = Pos(player.x, player.y+1);
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                next_pos = Pos(player.x-1, player.y);
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                next_pos = Pos(player.x+1, player.y);
            }
            else
            {
                return true;
            }

            if (buffer[next_pos] == '#')
            {
                return true;
            }

            if (moneys.ContainsKey(next_pos))
            {
                Money money = moneys[next_pos];
                below_text = "捡到 " + money.money + " 颗金币。";
                player.money += money.money;
                moneys.Remove(next_pos);

                if (money.id == 1)
                {
                    Timer.AddTimer(1, new TimeEvent("捡到金币后"));
                }
            }
            else if (monsters.ContainsKey(next_pos))
            {
                Monster monster = monsters[next_pos];
            }
            else if (npcs.ContainsKey(next_pos))
            {
                NPC npc = npcs[next_pos];
                below_text = "你好，冒险者。";
            }
            else
            {
                XY(next_pos, out player.x, out player.y);
            }

            return true;
        }

        static int RandPos()
        {
            while (true)
            {
                int map_pos = random.Next(0, (map_width - 2) * (map_height - 2));
                int pos = MapPosToPos(map_pos);
                if (buffer[pos] == ' ')
                {
                    return pos;
                }
            }
        }

        static bool ProcessTimeEvent()
        {
            object e = Timer.CheckTimer();
            if (e == null)
            {
                return false;
            }

            TimeEvent time_event = (TimeEvent)e;
            switch(time_event.event_type)
            {
                case "开始":
                    below_text = "这是一个空旷的地牢，四周除了墙壁，什么都没有。你觉得非常孤独。";
                    Timer.AddTimer(10, new TimeEvent("刷钱"));
                    break;
                case "刷钱":
                    below_text = "地上凭空出现了一些金币，闪闪发光。你好奇的过去看。";
                    int pos = RandPos();
                    Money m = new Money();
                    m.id = 1;
                    m.money = 10;
                    XY(pos, out m.x, out m.y);
                    moneys.Add(pos, m);
                    Timer.AddTimer(10, new TimeEvent("金币消失"));
                    break;
                case "金币消失":
                    if (moneys.Count() == 0)
                    {
                        break;
                    }
                    below_text = "你视金钱如粪土。";
                    moneys.Clear();
                    for (int i=0; i<(map_width-2)*(map_height-2); ++i)
                    {
                        int p = MapPosToPos(i);
                        if (buffer[p] == ' ')
                        {
                            
                        }
                    }
                    break;

                case "捡到金币后":

                    break;

                default:
                    break;
            }

            return true;
        }

        static void Main(string[] args)
        {
            Timer.begin_time = Timer.GetTimeStamp();
            ConsoleCanvas canvas = new ConsoleCanvas(width, height);
            buffer = canvas.GetBuffer();

            // 逻辑初始化
            player.x = 15;
            player.y = 10;
            Timer.Init();
            Timer.AddTimer(1, new TimeEvent("开始"));

            // 初始化图像
            DrawAll();
            canvas.Refresh();

            while (true)
            {
                bool need_refresh_move = false;
                bool need_refresh_timer = false;
                if (Console.KeyAvailable)
                {
                    need_refresh_move = MovePlayer();
                }

                int new_time = Timer.GetTimeStamp();
                if (new_time != time)
                {
                    time = new_time;
                    need_refresh_timer = ProcessTimeEvent();

                    if (!need_refresh_timer && time%10==0)
                    {
                        need_refresh_timer = true;
                    }
                }

                if (need_refresh_move || need_refresh_timer)
                {
                    canvas.ClearBuffer();
                    Console.WriteLine();
                    DrawAll();
                    canvas.Refresh();
                }
                Thread.Sleep(10);
            }

        }
    }
}
