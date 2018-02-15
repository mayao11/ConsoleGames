using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using System.Media;

namespace ConsoleApp1
{
    public class Container
    {
        public List<Bullet> bu = new List<Bullet>();
        public Dictionary<int, Reporter> re = new Dictionary<int, Reporter>();//敌人
        public Dictionary<int, TimeCoin> ti = new Dictionary<int, TimeCoin>();//金币
        public Dictionary<int, Glasses> gl = new Dictionary<int, Glasses>();//强化道具
        public Dictionary<int, Brick> br = new Dictionary<int, Brick>();//砖块
        public Dictionary<int, Wall> ga = new Dictionary<int, Wall>();//城门
        public Dictionary<int, Water> wa = new Dictionary<int, Water>();//水池
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowHeight = 30;
            Console.WindowWidth = 100;

            string namespaceName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
            Assembly assembly = Assembly.GetExecutingAssembly();
            SoundPlayer sp = new SoundPlayer(assembly.GetManifestResourceStream(namespaceName + ".Resources" + ".180.wav"));
            sp.PlayLooping();

            Frog frog = new Frog(19, 0);
            frog.resurrection = 3;
            int levelnum = 1;
            bool win = false;
            while (frog.resurrection > 0 && win == false)//只要还有复活次数，就重新读取地图
            {
                frog.y = 19;//重新定义坐标
                frog.x = 0;
                MapCreate map = new MapCreate();//创建新地图
                Container container = new Container();//创建新存储类
                char[,] mappos = null;//初始化临时地图
                container = map.ReadChar(map.PosRead(map.Mapchoose(levelnum)));//存储类储存数据
                mappos = map.PosRead(map.Mapchoose(levelnum));//临时地图存储坐标
                int test = 0;
                while (true)
                {
                    Console.SetCursorPosition(0, 0);
                    mappos[frog.y, frog.x] = 'm';
                    map.PrintDraw(mappos);
                    Console.WriteLine(test);
                    test++;//刷新帧数测试
                    if (Console.KeyAvailable)//有键值输入
                    {
                        mappos = FrogMove(container, mappos, frog);//操作输入
                    }
                    if (test % 5 == 0)
                    {
                        FrogUpdate(container, mappos, frog);//青蛙更新
                    }
                    if (test % 10 == 0)
                    {
                        ReporterUpdate(container, mappos);//怪物更新
                    }
                    if (frog.hp == 0)//死亡跳出
                    {
                        frog.arm = false;
                        frog.resurrection--;//如果失败，则消耗复活次数
                        frog.hp = 92;//回血
                        break;
                    }
                    if (frog.x + 1 == 99)//碰到城门或者右边界，进入下一关
                    {
                        levelnum++;
                        break;
                    }
                    if (frog.Be_Gate(frog.x, frog.y, container))
                    {
                        win = true;
                        break;
                    }
                    for (int n = 0; n < frog.resurrection; n++)
                    {
                        mappos[1, 12 + n] = 'M';
                    }
                }
            }
            if (frog.resurrection == 0)
            {
                sp.Stop();
                sp = new SoundPlayer(assembly.GetManifestResourceStream(namespaceName + ".Resources" + ".1756.wav"));
                sp.Play();

                MapCreate map = new MapCreate();//创建新地图
                char[,] mappos = map.PosRead(map.Mapchoose(7));//临时地图存储坐标
                Console.SetCursorPosition(0, 0);
                map.PrintDraw(mappos);
            }
            else if (win == true)
            {
                sp.Stop();
                sp = new SoundPlayer(assembly.GetManifestResourceStream(namespaceName + ".Resources" + ".1761.wav"));
                sp.Play();
                
                MapCreate map = new MapCreate();//创建新地图
                char[,] mappos = map.PosRead(map.Mapchoose(6));//临时地图存储坐标
                Console.SetCursorPosition(0, 0);
                map.PrintDraw(mappos);
            }

            Console.ReadLine();
        }
        static char[,] FrogMove(Container container, char[,] mappos, Frog frog)//判断青蛙运动
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            int y = frog.y;
            int x = frog.x;
            if (keyInfo.Key == ConsoleKey.RightArrow || keyInfo.Key == ConsoleKey.LeftArrow)//如果往左或者往右运动，就计算按键时差
            {
                float oversecond = 0, startsecond = 0;
                startsecond = DateTime.Now.Millisecond;
                if (System.Math.Abs(startsecond - oversecond) < 100) { return mappos; }
                else
                {
                    if (!frog.Be_Brick(frog.x, frog.y + 1, container) && frog.airmove <= 0) { return mappos; }
                    else
                    {
                        frog.airmove--;
                        oversecond = startsecond;
                        if (keyInfo.Key == ConsoleKey.RightArrow)
                        {
                            if (frog.Be_Brick(frog.x + 1, frog.y, container)) { return mappos; }//往左撞墙
                            else
                            {
                                frog.x++;
                                Clear(mappos);
                                mappos[frog.y, frog.x] = 'm';
                            }
                        }
                        else if (keyInfo.Key == ConsoleKey.LeftArrow)
                        {
                            if (frog.Be_Brick(frog.x - 1, frog.y, container)) { return mappos; }//往右撞墙
                            else if (frog.x - 1 < 0) { frog.x++; return mappos; }
                            else
                            {
                                frog.x--;
                                Clear(mappos);
                                mappos[frog.y, frog.x] = 'm';
                            }
                        }
                    }
                }
            }
            if (keyInfo.Key == ConsoleKey.UpArrow && frog.Be_Brick(frog.x, frog.y + 1, container))//如果按键跳跃就计算是否站在地面上，是则跳跃，否则不跳跃
            {
                frog.jump = true;
                frog.starty = frog.y;
                frog.airmove = 5;
                frog.jumpstart = DateTime.Now;
            }
            if (frog.Be_Glasses(frog.x, frog.y, container))//如果碰到眼镜，则捡到武器
            {
                frog.arm = true;
            }
            if (keyInfo.Key == ConsoleKey.Spacebar && frog.arm == true)//如果持有武器且按键，则射击
            {
                frog.attack = true;
                Bullet bullet = new Bullet();
                container.bu.Add(bullet);
                bullet.x = frog.x;
                bullet.y = frog.y;
                bullet.startx = frog.x;
            }
            if (frog.Be_Timecoin(frog.x, frog.y, container))//碰到硬币
            {
                container.ti.Remove((y + 1) * 100 + x);
                frog.timecoin++;
            }
            if(frog.Be_Brick(frog.x, frog.y + 1, container))
            {
                frog.airmove=5;
            }
            return mappos;
        }
        static char[,] FrogUpdate(Container container, char[,] mappos, Frog frog)
        {
            int y = frog.y;
            int x = frog.x;
            frog.jumpduring = DateTime.Now;//跳跃时间计算
            if (frog.jump == true)
            {
                if (frog.Be_Brick(frog.x, frog.y - 1, container) || frog.y - 1 <= 1)//在空中撞到砖块或者边界，马上下降
                {
                    frog.jump = false;
                }
                else
                {
                    if (frog.y >= frog.starty - 4)//如果在跳跃高度内,往上跳
                    {
                        double c = TimeGo(frog.jumpduring, frog.jumpstart);//与起跳时间的秒数差
                        frog.y -= (int)Math.Round(-0.444 * c * c + 2.666 * c + 1);
                        Clear(mappos);
                        mappos[frog.y, frog.x] = 'm';
                    }
                    else if (frog.y < frog.starty - 3)//超越跳跃最高点了
                    {
                        frog.jump = false;
                    }
                }
            }//跳跃
            if (frog.Be_Reporter(frog.x, frog.y, container) || frog.Be_Water(frog.x, frog.y + 1, container))
            {
                frog.hp = 0;
            }//青蛙碰怪或者碰到水
            if (!frog.Be_Brick(frog.x, frog.y + 1, container) && frog.jump == false)
            {
                frog.y++;
                Clear(mappos);
                mappos[frog.y, frog.x] = 'm';
            }//青蛙脚下没有砖块
            if (mappos[frog.y + 1, frog.x] == '@')
            {
                mappos[frog.y + 1, frog.x] = ' ';
                container.re.Remove((frog.y + 1) * 100 + frog.x);
            }//青蛙脚下是怪
            if (container.bu.Count > 0)
            {
                List<Bullet> disappear = new List<Bullet>();
                BulletClear(mappos);
                foreach (var bullet in container.bu)
                {
                    if (bullet.x - bullet.startx > 10 || bullet.Be_Brick(bullet.x + 1, bullet.y, container) || bullet.Be_Reporter(bullet.x, bullet.y, container))
                    {
                        disappear.Add(bullet);
                    }
                    else
                    {
                        bullet.x++;
                        mappos[bullet.y, bullet.x] = '>';
                    }
                }
                foreach (var bullet in disappear)
                {
                    container.bu.Remove(bullet);
                }
            }//子弹判定（如果子弹超过射程或者子弹碰墙或者子弹碰到怪便消失）
            return mappos;
        }//青蛙的更新
        static void ReporterUpdate(Container container, char[,] mappos)
        {
            List<int> dead_reporter = new List<int>();
            List<int> down_reporter = new List<int>();
            List<int> right_reporter = new List<int>();
            List<int> left_reporter = new List<int>();

            foreach (var pair in container.re)
            {
                pair.Value.y = pair.Key / 100;
                pair.Value.x = pair.Key % 100;
                int y = pair.Value.y;
                int x = pair.Value.x;
                if (!pair.Value.Be_Brick(pair.Value.x, pair.Value.y + 1, container))
                {
                    if (pair.Value.Be_Water(pair.Value.x, pair.Value.y + 1, container))//脚下是水
                    {
                        dead_reporter.Add(pair.Value.y * 100 + pair.Value.x);//将脚下碰水的记者加入死亡列表
                        mappos[pair.Value.y, pair.Value.x] = mappos[2, 0];
                    }
                    else//脚下不是水，记者往下落
                    {
                        down_reporter.Add(pair.Value.y * 100 + pair.Value.x);
                    }
                }//脚下没有砖块
                else if (mappos[pair.Value.y, pair.Value.x - 1] == '>')
                {
                    dead_reporter.Add(pair.Value.y * 100 + pair.Value.x);
                }
                else
                {
                    if (pair.Value.turn == 0)
                    {
                        if (pair.Value.Be_Brick(pair.Value.x - 1, pair.Value.y, container) || pair.Value.x - 1 <= 1 || pair.Value.Be_Reporter(pair.Value.x - 1, pair.Value.y, container))
                        {
                            pair.Value.turn = 1;
                        }
                        else
                        {
                            left_reporter.Add(pair.Value.y * 100 + pair.Value.x);
                        }
                    }
                    if (pair.Value.turn == 1)
                    {
                        if (pair.Value.Be_Brick(pair.Value.x + 1, pair.Value.y, container) || pair.Value.x + 1 >= 100 || pair.Value.Be_Reporter(pair.Value.x + 1, pair.Value.y, container))
                        {
                            pair.Value.turn = 0;
                        }
                        else
                        {
                            right_reporter.Add(pair.Value.y * 100 + pair.Value.x);
                        }
                    }

                }//脚下有砖块       

            }
            foreach (var pair in dead_reporter)//从记者字典里移除死亡的记者
            {
                container.re.Remove(pair);
            }
            foreach (var pair in down_reporter)
            {
                int x = container.re[pair].x;
                int y = container.re[pair].y;
                Reporter re = container.re[pair];
                container.re.Remove(y * 100 + x);
                container.re.Add((y + 1) * 100 + x, re);
                mappos[y, x] = ' ';
                mappos[y + 1, x] = '@';
            }
            foreach (var pair in right_reporter)
            {
                int x = container.re[pair].x;
                int y = container.re[pair].y;
                Reporter re = container.re[pair];
                container.re.Remove(y * 100 + x);
                container.re.Add(y * 100 + (x + 1), re);
                mappos[y, x] = ' ';
                mappos[y, x + 1] = '@';
            }
            foreach (var pair in left_reporter)
            {
                int x = container.re[pair].x;
                int y = container.re[pair].y;
                Reporter re = container.re[pair];
                container.re.Remove(y * 100 + x);
                container.re.Add(y * 100 + (x - 1), re);
                mappos[y, x] = ' ';
                mappos[y, x - 1] = '@';
            }

            //Console.ReadKey();

        }//记者的更新
        static double TimeGo(DateTime dt1, DateTime dt2)//计算时差
        {
            TimeSpan ts = dt2.Subtract(dt1);
            return Math.Abs(ts.TotalSeconds);
        }
        static void Clear(char[,] mappos)
        {
            for (int n = 0; n < 20; n++)
            {
                for (int m = 0; m < 100; m++)
                {
                    if (mappos[n, m] == 'm')
                    {
                        mappos[n, m] = ' ';
                    }
                }
            }
        }//清除足迹
        static void BulletClear(char[,] mappos)
        {
            for (int n = 0; n < 20; n++)
            {
                for (int m = 0; m < 100; m++)
                {
                    if (mappos[n, m] == '>')
                    {
                        mappos[n, m] = ' ';
                    }
                }
            }
        }//清除弹迹
    }
    public class MapCreate
    {
        const int gameheigth = 22;
        const int gamewidth = 100;
        public int Pos_num(int _y, int _x)//坐标转序号
        {
            int posnum = _y * gamewidth + _x;
            return posnum;
        }
        public void Num_pos(int posnum, out int y, out int x)//序号转坐标
        {
            y = posnum / gamewidth;
            x = posnum % gamewidth;
        }
        public string[] Mapchoose(int levelnumb)
        {
            string[] array = new string[22];
            switch (levelnumb)
            {
                case 1:
                    array = File.ReadAllLines(@"..\地图一.txt");
                    array = File.ReadAllLines("..\n地图一.txt");
                    array = File.ReadAllLines("../地图一.txt");
                    return array;
                case 2:
                    array = File.ReadAllLines(@"地图二.txt");
                    return array;
                case 3:
                    array = File.ReadAllLines(@"地图三.txt");
                    return array;
                case 4:
                    array = File.ReadAllLines(@"地图四.txt");
                    return array;
                case 5:
                    array = File.ReadAllLines(@"地图五.txt");
                    return array;
                case 6:
                    array = File.ReadAllLines(@"胜利.txt");
                    return array;
                case 7:
                    array = File.ReadAllLines(@"失败.txt");
                    return array;
            }
            return array;
        }//地图文件读取（关卡数-地图）
        public char[,] PosRead(string[] levelmap)
        {
            List<string> stringmap = new List<string>();
            List<char[]> charmap = new List<char[]>();
            char[,] mappos = new char[22, 100];
            string[] array = levelmap;
            int height = 0;
            foreach (var s in array)
            {
                char[] a = null;
                stringmap.Add(s);
                a = stringmap[height].ToArray();
                charmap.Add(a);//char数组里面增加了一串横的坐标里的内容
                int length = 0;//横坐标
                for (length = 0; length < 100 && a.Length > length; length++)
                {
                    mappos[height, length] = a[length];
                }
                height++;
            }
            return mappos;
        }//读取地图，返回char二维数组（地图-地图信息）
        public void PrintDraw(char[,] mappos)
        {
            for (int n = 0; n < 22; n++)
            {
                for (int m = 0; m < 100; m++) { Console.Write(mappos[n, m]); }
                Console.WriteLine();
            }
        }//打印地图
        public Container ReadChar(char[,] mappos)
        {
            Container container = new Container();
            for (int n = 0; n < 22; n++)
            {
                for (int m = 0; m < 100; m++)
                {
                    if (mappos[n, m] == '#')
                    {
                        Brick brick = new Brick();//砖块坐标导入
                        container.br.Add(Pos_num(n, m), brick);
                    }
                    if (mappos[n, m] == '~')//水池坐标导入
                    {
                        Water water = new Water();
                        container.wa.Add(Pos_num(n, m), water);
                    }
                    if (mappos[n, m] == '0')//金币坐标导入
                    {
                        TimeCoin coin = new TimeCoin();
                        container.ti.Add(Pos_num(n, m), coin);
                    }
                    if (mappos[n, m] == '@')//敌人坐标导入
                    {
                        Reporter reporter = new Reporter();
                        container.re.Add(Pos_num(n, m), reporter);
                    }
                    if (mappos[n, m] == '%')//眼镜坐标导入
                    {
                        Glasses glasses = new Glasses();
                        container.gl.Add(Pos_num(n, m), glasses);
                    }
                    if (mappos[n, m] == 'n')//城门坐标导入
                    {
                        Wall gate = new Wall();
                        container.ga.Add(Pos_num(n, m), gate);
                    }
                }
                Console.WriteLine();
            }
            return container;
        }//读取每一个点坐标的物品，加入相应字典
    }
}