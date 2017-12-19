using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower
{
    public class Program
    {
        const int width = 13;
        const int height = 13;
        public static List<Container> element = new List<Container>();//封装所有数据
        
        public static int a = 0;//层数
        public static string s = null;
        public static int Tmp_a = 0;

        public static int MapPos(int x,int y)
        {
            int next_Pos = y * width + x;
            return next_Pos;
        }
        public static void MapXy(int next_pos,out int x,out int y)
        {
            y = next_pos / width;
            x = next_pos % width;
            
        }

        public static void Describe(char[,] maps, ConsoleColor[,] color_buffer)//画各种物品的形状 
        {
            foreach (var pair in element[a].wall) //画墙壁
            {
                int x, y;
                x = y = 0;
                MapXy(pair.Key,out x,out y);
                maps[x, y] = '█';
            }
            foreach (var pair in element[a].gate)//画门
            {
                if (pair.Value.type == ConsoleKey2.Yellow)
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '〓';
                    color_buffer[x, y] = ConsoleColor.Yellow;
                }
                else if (pair.Value.type==ConsoleKey2.Blue)
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '〓';
                    color_buffer[x, y] = ConsoleColor.Blue;
                }
                else if (pair.Value.type == ConsoleKey2.red)
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '〓';
                    color_buffer[x, y] = ConsoleColor.Red;
                }

            }
            foreach (var pair in element[a].eq)//画道具
            {
                if (pair.Value.Name == "圣剑")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '剑';
                    color_buffer[x, y] = ConsoleColor.Magenta;
                }
                else if (pair.Value.Name == "圣盾")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '盾';
                    color_buffer[x, y] = ConsoleColor.DarkCyan;
                }
                else if (pair.Value.Name == "红血瓶")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '血';
                    color_buffer[x, y] = ConsoleColor.Red;
                }
                else if (pair.Value.Name == "蓝血瓶")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '血';
                    color_buffer[x, y] = ConsoleColor.Blue;
                }
                else if (pair.Value.Name == "蓝宝石")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '◆';
                    color_buffer[x, y] = ConsoleColor.Blue;
                }
                else if (pair.Value.Name == "红宝石")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '◆';
                    color_buffer[x, y] = ConsoleColor.Red;
                }
            }
            foreach (var pair in element[a].ms)//画怪物
            {
                if (pair.Value.Name == "小蝙蝠")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '蝠';
                }
                else if (pair.Value.Name == "史莱姆")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '姆';
                }
                else if (pair.Value.Name == "骷髅怪")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '骷';
                }
                else if (pair.Value.Name == "骷髅将军")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '军';
                }
                else if (pair.Value.Name == "暗黑法师")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '法';
                }
                else if (pair.Value.Name == "巨人")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '巨';
                }
                else if (pair.Value.Name == "魔王")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '魔';
                    color_buffer[x, y] = ConsoleColor.Red;
                }
            }
            foreach (var pair in element[a].key)//画钥匙
            {
                if (pair.Value.type == ConsoleKey2.Yellow)
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '♀';
                    color_buffer[x, y] = ConsoleColor.Yellow;
                }
                else if (pair.Value.type == ConsoleKey2.red)
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '♀';
                    color_buffer[x, y] = ConsoleColor.Red;
                }
                else if (pair.Value.type == ConsoleKey2.Blue)
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '♀';
                    color_buffer[x, y] = ConsoleColor.Blue;
                }
            }
            foreach (var pair in element[a].stair)
            {
                if (pair.Value.type == Dict.Up)
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '↑';
                }
                else if (pair.Value.type == Dict.Down)
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '↓';
                }
            }
            foreach (var pair in element[a].npc)
            {
                int x, y;
                x = y = 0;
                MapXy(pair.Key, out x, out y);
                maps[x, y] = 'N';
                color_buffer[x, y] = ConsoleColor.DarkBlue;
            }
            foreach (var pair in element[a].func)//画功能道具
            {
                if (pair.Value.Name == "楼层跳跃魔杖")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '☆';
                    color_buffer[x, y] = ConsoleColor.DarkMagenta;
                }
                else if (pair.Value.Name == "怪物字典")
                {
                    int x, y;
                    x = y = 0;
                    MapXy(pair.Key, out x, out y);
                    maps[x, y] = '≡';
                    color_buffer[x, y] = ConsoleColor.DarkCyan;
                }
            }
        }

        public static bool MovePlye(Play play)
        {
            
            if (Tmp_a<a)
            {
                Tmp_a = a;
            }
            ConsoleKeyInfo key = Console.ReadKey();
            int Tmp_x = play.x;
            int Tmp_y = play.y;
            if (key.Key == ConsoleKey.UpArrow)//获取输入，并改变玩家的坐标
            {
                Tmp_x -= 1;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                Tmp_x += 1;
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                Tmp_y -= 1;
            }
            else if (key.Key == ConsoleKey.RightArrow)
            {
                Tmp_y += 1;
            }
            else if (key.Key == ConsoleKey.B)//使用楼层跳跃功能
            {
                if (play.Jump)
                {
                    int Tmp_b = 0;
                    bool c = false;
                    Console.Clear();
                    Console.WriteLine("输入你想到达的楼层");
                    while (!c)
                    {
                        string Tmp_ss;
                        Tmp_ss = Console.ReadLine();
                        c = int.TryParse(Tmp_ss, out Tmp_b);
                        if (Tmp_b < 0 || Tmp_b > Tmp_a + 1)
                        {
                            c = false;
                        }
                    }
                    if (a < Tmp_b - 1)
                    {
                        a = Tmp_b - 1;
                        play.x = element[a].Up_x;
                        play.y = element[a].Up_y;
                        s = "玩家使用了道具来到" + (a + 1) + "层";
                    }
                    else if (a > Tmp_b - 1)
                    {
                        a = Tmp_b - 1;
                        play.x = element[a].Down_x;
                        play.y = element[a].Down_y;
                        s = "玩家使用了道具来到" + (a + 1) + "层";
                    }
                    else
                    {
                        return true;
                    }

                }
                return true;
            }
            else if (key.Key == ConsoleKey.Y)
            {
                if (play.See)
                {
                    Console.Clear();


                    foreach (var pair in element[a].ms)
                    {
                        int Tmp_HP, Tmp_Atk, Tmp_Dfs;
                        Tmp_HP = play.Hp;
                        Tmp_Atk = play.Atk;
                        Tmp_Dfs = play.Dfs;
                        int Tmp_a = pair.Value.Battle2(Tmp_Atk,Tmp_Dfs,Tmp_HP);
                        if (Tmp_a == 9999)
                        {
                            Console.WriteLine("怪物{0}，血量{1}，攻击力{2}，防御力{3},打不过！", pair.Value.Name, pair.Value.Hp, pair.Value.Atk, pair.Value.Dfs);
                        }
                        else
                        {
                            Console.WriteLine("怪物{0}，血量{1}，攻击力{2}，防御力{3},预计损伤{4}点", pair.Value.Name, pair.Value.Hp, pair.Value.Atk, pair.Value.Dfs,Tmp_a);
                        }
                    }
                    Console.ReadKey();
                    return true;
                }
            }
            else
            {
                return true;
            }
            if (Tmp_x < 1 || Tmp_x >11 || Tmp_y < 1||Tmp_y >11)
            {
                return true;
            }
            int Tmp_pos = MapPos(Tmp_x,Tmp_y);

            if (element[a].wall.ContainsKey(Tmp_pos))//如果下一步是墙
            {

            }
            else if (element[a].gate.ContainsKey(Tmp_pos))//如果下一步是门
            {
                if (element[a].gate[Tmp_pos].type == ConsoleKey2.Yellow && play.relKey > 0)
                {
                    play.relKey -= 1;
                    s = "打开了黄色门，黄色钥匙数量-1";
                    element[a].gate.Remove(Tmp_pos);
                    return true;
                }
                else if (element[a].gate[Tmp_pos].type==ConsoleKey2.Blue && play.BlueKey>0)
                {
                    play.BlueKey -= 1;
                    s = "打开了蓝色门，蓝色钥匙数量-1";
                    element[a].gate.Remove(Tmp_pos);
                    return true;
                }
                else if (element[a].gate[Tmp_pos].type == ConsoleKey2.red && play.RedKey > 0)
                {
                    play.RedKey -= 1;
                    s = "打开了红色门，红色钥匙数量-1";
                    element[a].gate.Remove(Tmp_pos);
                    return true;
                }
                s = "相应颜色钥匙数量不足，无法打开门";
            }
            else if (element[a].eq.ContainsKey(Tmp_pos))//如果下一步是道具
            {
                s = "捡到了" + element[a].eq[Tmp_pos].Name+"属性提升";
                play.Hp = play.Hp + element[a].eq[Tmp_pos].AddHp;
                play.Atk = play.Atk + element[a].eq[Tmp_pos].Addatk;
                play.Dfs = play.Dfs + element[a].eq[Tmp_pos].Adddfs;
                element[a].eq.Remove(Tmp_pos);
            }
            else if (element[a].key.ContainsKey(Tmp_pos))//如果下一步是钥匙
            {
                if (element[a].key[Tmp_pos].type == ConsoleKey2.Yellow)
                {
                    play.relKey += 1;
                    s = "捡到了黄色钥匙，黄钥匙数量加1";
                }
                else if (element[a].key[Tmp_pos].type == ConsoleKey2.Blue)
                {
                    play.BlueKey += 1;
                    s = "捡到了蓝色钥匙，蓝钥匙数量加1";
                }
                else if (element[a].key[Tmp_pos].type == ConsoleKey2.red)
                {
                    play.RedKey += 1;
                    s = "捡到了红色钥匙，红钥匙数量加1";
                }
                element[a].key.Remove(Tmp_pos);
            }
            else if (element[a].ms.ContainsKey(Tmp_pos))//如果下一步是怪物
            {
                int Tmp_Hp = play.Hp;
                bool c = element[a].ms[Tmp_pos].Battle(play);
                if (c == false)
                {
                    return false;
                }
                else if (c==true)
                {
                    s = "与怪物" + element[a].ms[Tmp_pos].Name + "战斗，血量减少" + (Tmp_Hp-play.Hp)+"点"+"经验增加"+ element[a].ms[Tmp_pos].Level * 10+"金币增加"+ element[a].ms[Tmp_pos].Level * 10;
                    play.Exp = play.Exp + element[a].ms[Tmp_pos].Level * 5;
                    play.Money = play.Money + element[a].ms[Tmp_pos].Level * 5;
                    element[a].ms.Remove(Tmp_pos);
                    
                }
            }
            else if (element[a].stair.ContainsKey(Tmp_pos))//如果下一步是楼梯
            {
                if (element[a].stair[Tmp_pos].type == Dict.Up)
                {
                    a = a + 1;
                    if (a>=10)
                    {
                        return false;
                    }
                    element[a].map1s.color_buffer[play.x, play.y] = ConsoleColor.Gray;
                    play.x = element[a].Up_x;
                    play.y = element[a].Up_y;
                }
                else if (element[a].stair[Tmp_pos].type == Dict.Down)
                {
                    a = a - 1;
                    element[a].map1s.color_buffer[play.x, play.y] = ConsoleColor.Gray;
                    play.x = element[a].Down_x;
                    play.y = element[a].Down_y;   
                }
            }
            else if (element[a].npc.ContainsKey(Tmp_pos))//如果下一步是Npc
            {
                if (element[a].npc[Tmp_pos].Name == "恶魔商店")
                {
                    int h = 0;
                    bool c = false;
                    Console.Clear();
                    Console.WriteLine("欢迎来到恶魔商店，给我一点小小的金币，就能给你带来巨大的提升。还有一次只能购买一件物品哦！");
                    Console.WriteLine("1:50金币获得攻击+10");
                    Console.WriteLine("2:50金币获得血量+100");
                    Console.WriteLine("3:50金币获得防御+10");
                    while (!c)
                    {
                        string m = Console.ReadLine();
                        c = int.TryParse(m, out h);
                        if (h < 1 || h > 4)
                        {
                            c = false;
                        }
                    }
                    if (h == 1)
                    {
                        if (play.Money<50)
                        {
                            s = "金币不够，购买失败！";
                            return true;
                        }
                        play.Money -= 50;
                        play.Atk += 10;
                    }
                    else if (h == 2)
                    {
                        if (play.Money < 50)
                        {
                            s = "金币不够，购买失败！";
                            return true;
                        }
                        play.Money -= 50;
                        play.Hp += 100;
                    }
                    else if (h == 3)
                    {
                        if (play.Money < 50)
                        {
                            s = "金币不够，购买失败！";
                            return true;
                        }
                        play.Money -= 50;
                        play.Dfs += 10;
                    }
                    s = "购买成功，感谢光临！";
                    return true;
                }
                else if (element[a].npc[Tmp_pos].Name == "经验商人")
                {
                    int h = 0;
                    bool c = false;
                    Console.Clear();
                    Console.WriteLine("欢迎来到经验商店，给我一点小小的经验，就能给你带来巨大的提升。还有一次只能购买一件物品哦！");
                    Console.WriteLine("1:50经验获得攻击+10");
                    Console.WriteLine("2:50经验获得血量+100");
                    Console.WriteLine("3:50经验获得防御+10");
                    Console.WriteLine("4:100经验获得等级+1");
                    while (!c)
                    {
                        string m = Console.ReadLine();
                        c = int.TryParse(m, out h);
                        if (h < 1 || h > 5)
                        {
                            c = false;
                        }
                    }
                    if (h == 1)
                    {
                        if (play.Exp<50)
                        {
                            s = "经验不足，购买失败";
                            return true;
                        }
                        play.Exp -= 50;
                        play.Atk += 10;
                    }
                    else if (h == 2)
                    {
                        if (play.Exp < 50)
                        {
                            s = "经验不足，购买失败";
                            return true;
                        }
                        play.Exp -= 50;
                        play.Hp += 100;
                    }
                    else if (h == 3)
                    {
                        if (play.Exp < 50)
                        {
                            s = "经验不足，购买失败";
                            return true;
                        }
                        play.Exp -= 50;
                        play.Dfs += 10;
                    }
                    else if (h == 4)
                    {
                        if (play.Exp < 100)
                        {
                            s = "经验不足，购买失败";
                            return true;
                        }
                        play.Exp -= 100;
                        play.Dfs += 5;
                        play.Atk += 5;
                        play.Level += 1;
                        play.Hp += 50;
                    }
                    s = "购买成功，感谢光临！";
                    return true;
                }
                else if (element[a].npc[Tmp_pos].Name == "钥匙商人")
                {
                    int h = 0;
                    bool c = false;
                    Console.Clear();
                    Console.WriteLine("欢迎来到钥匙商店，给我一点小小的金币，就能给你一把开启新世界的钥匙。还有一次只能购买一件物品哦！");
                    Console.WriteLine("1:100金币获得一把红钥匙");
                    Console.WriteLine("2:70金币获得一把蓝钥匙");
                    Console.WriteLine("3:30金币获得一把黄钥匙");
                    while (!c)
                    {
                        string m = Console.ReadLine();
                        c = int.TryParse(m, out h);
                        if (h < 1 || h > 4)
                        {
                            c = false;
                        }
                    }
                    if (h == 1)
                    {
                        if (play.Money < 100)
                        {
                            s = "金币不够，购买失败！";
                            return true;
                        }
                        play.Money -= 100;
                        play.RedKey += 1;
                    }
                    else if (h == 2)
                    {
                        if (play.Money < 70)
                        {
                            s = "金币不够，购买失败！";
                            return true;
                        }
                        play.Money -= 70;
                        play.BlueKey += 1;
                    }
                    else if (h == 3)
                    {
                        if (play.Money < 30)
                        {
                            s = "金币不够，购买失败！";
                            return true;
                        }
                        play.Money -= 30;
                        play.relKey += 1;
                    }
                    s = "购买成功，感谢光临！";
                    return true;
                }
                else if (element[a].npc[Tmp_pos].Name == "老人")
                {
                    s = element[a].npc[Tmp_pos].Dialogue;
                    play.relKey++;
                    play.BlueKey++;
                    play.RedKey++;
                    element[a].npc.Remove(Tmp_pos);
                }
                else if (element[a].npc[Tmp_pos].Name == "富商")
                {
                    s = element[a].npc[Tmp_pos].Dialogue;
                    play.Money += 200;
                    element[a].npc.Remove(Tmp_pos);
                }
                else if (element[a].npc[Tmp_pos].Name == "祭师")
                {
                    s = element[a].npc[Tmp_pos].Dialogue;
                    play.Level += 3;
                    play.Hp += 3 * 50;
                    play.Atk += 3 * 5;
                    play.Dfs += 3 * 5;
                    element[a].npc.Remove(Tmp_pos);
                }
            }
            else if (element[a].func.ContainsKey(Tmp_pos))//如果下一步是功能道具
            {
                if (element[a].func[Tmp_pos].Name == "楼层跳跃魔杖")
                {
                    play.Jump = true;
                    s = "玩家获得了楼层跳跃魔杖，开启了楼层跳跃功能,按B键开启楼层跳跃功能";
                    element[a].func.Remove(Tmp_pos);
                    return true;
                }
                else if (element[a].func[Tmp_pos].Name =="怪物字典")
                {
                    play.See = true;
                    s = "玩家获得了怪物字典，开启了查看怪物属性功能,按Y键开启查看功能";
                    element[a].func.Remove(Tmp_pos);
                    return true;
                }
            }
            else
            {
                element[a].map1s.color_buffer[play.x, play.y] = ConsoleColor.Gray;
                MapXy(Tmp_pos,out play.x,out play.y);
                
            }
            return true;
        }

        public static void Print(char[,] a,ConsoleColor[,] b)
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    if (j == 12)
                    {
                    
                        Console.ForegroundColor = b[i, j];
                        Console.WriteLine(a[i, j]);
                       
                        
                        break;
                    }
                    if (a[i, j] < 128)
                    {

                        Console.ForegroundColor = b[i, j];
                        Console.Write(a[i, j]);
                        
                        Console.Write(" ");
                        continue;
                    }
                    Console.ForegroundColor = b[i, j];
                    Console.Write(a[i, j]);
                    
                }
            }

        }

        static void Main(string[] args)
        {
            Play wj = new Play();
            Checkpoint gk = new Checkpoint();
            gk.check();
            bool m = true;
            s = "目标：打败魔王，冲上11层";
            while (m)
            {
                Console.WriteLine("当前关卡{0},血量{1},攻击力{2},防御力{3},黄钥匙{4},蓝钥匙{5},红钥匙{6},等级{7},经验{8},金币{9}",a+1,wj.Hp,wj.Atk,wj.Dfs,wj.relKey,wj.BlueKey,wj.RedKey,wj.Level,wj.Exp,wj.Money);
                element[a].map1s.Fill_Map();
                element[a].map1s.Fill_Map2();
                element[a].map1s.Boundary_Map();
                element[a].map1s.maps[wj.x, wj.y] = '勇';
                element[a].map1s.color_buffer[wj.x, wj.y] = ConsoleColor.Magenta;
                Describe(element[a].map1s.maps, element[a].map1s.color_buffer);
                Print(element[a].map1s.maps, element[a].map1s.color_buffer);
                Console.WriteLine(s);
                m = MovePlye(wj);
                Console.Clear();
            }
            if (wj.Hp < 0)
            {
                Console.WriteLine("玩家血量归零，游戏结束！");
            }
            else
            {
                Console.WriteLine("恭喜通关，游戏结束！");
            }
            
            Console.ReadKey();
        }
    }
}
