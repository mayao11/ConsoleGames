using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tower
{
    public class Checkpoint//关卡
    {

        public void checkpoint1()//第一层
        {
            Container Em = new Container();
#region//添加地图
            Map maps = new Map();
            maps.Fill_Map();
            maps.Fill_Map2();
            maps.Boundary_Map();
            Em.map1s = maps;
            
#endregion
#region //添加怪物
            Monster ms1 = new Monster();
            Monster ms2 = new Monster();
            Monster ms3 = new Monster();
            Monster ms4 = new Monster();
            Monster ms5 = new Monster();
            Monster ms6 = new Monster();
            Monster ms7 = new Monster();
            Monster ms8 = new Monster();
            Monster ms9 = new Monster();
            int next_pos1 = ms1.Slime(1, 3);
            int next_pos2 = ms2.Slime(1, 4);
            int next_pos3 = ms3.Slime(1, 5);
            int next_pos4 = ms4.Skeleton(4, 2);
            int next_pos5 = ms5.Bat(6, 7);
            int next_pos6 = ms6.Master(6, 8);
            int next_pos7 = ms7.Bat(6, 9);
            int next_pos8 = ms8.Skeleton2(7, 2);
            int next_pos9 = ms9.Bat(10, 10);
            Em.ms.Add(next_pos1, ms1);
            Em.ms.Add(next_pos2, ms2);
            Em.ms.Add(next_pos3, ms3);
            Em.ms.Add(next_pos4, ms4);
            Em.ms.Add(next_pos5, ms5);
            Em.ms.Add(next_pos6, ms6);
            Em.ms.Add(next_pos7, ms7);
            Em.ms.Add(next_pos8, ms8);
            Em.ms.Add(next_pos9, ms9);
#endregion
#region//添加道具
            Prop pop1 = new Prop();
            Prop pop2 = new Prop();
            Prop pop3 = new Prop();
            Prop pop4 = new Prop();
            Prop pop5 = new Prop();
            Prop pop6 = new Prop();
            Prop pop7 = new Prop();
            int next_Poppos1 = pop1.PotionRed(3,1);
            int next_Poppos2 = pop2.GemstoneRed(3, 7);
            int next_Poppos3 = pop3.GemstoneBlue(4, 7);
            int next_Poppos4 = pop4.PotionRed(4, 8);
            int next_Poppos5 = pop5.PotionRed(10, 1);
            int next_Poppos6 = pop6.PotionRed(11, 1);
            int next_Poppos7 = pop7.PotionBlue(11, 10);
            Em.eq.Add(next_Poppos1, pop1);
            Em.eq.Add(next_Poppos2, pop2);
            Em.eq.Add(next_Poppos3, pop3);
            Em.eq.Add(next_Poppos4, pop4);
            Em.eq.Add(next_Poppos5, pop5);
            Em.eq.Add(next_Poppos6, pop6);
            Em.eq.Add(next_Poppos7, pop7);
            #endregion
#region//添加特殊道具
            Func_Prop jump = new Func_Prop();
            int next_Funcpos = jump.Jump_Prop(11,2);
            Em.func.Add(next_Funcpos,jump);
#endregion
#region//添加钥匙
            Key key1 = new Key();
            Key key2 = new Key();
            Key key3 = new Key();
            Key key4 = new Key();
            Key key5 = new Key();
            int nexy_Keypos1 = key1.TypeKey(ConsoleKey2.Yellow, 3, 8);
            int nexy_Keypos2 = key2.TypeKey(ConsoleKey2.Yellow, 6, 1);
            int nexy_Keypos3 = key3.TypeKey(ConsoleKey2.Yellow, 10, 3);
            int nexy_Keypos4 = key4.TypeKey(ConsoleKey2.Yellow, 11, 3);
            int nexy_Keypos5 = key5.TypeKey(ConsoleKey2.Yellow, 10, 5);
            Em.key.Add(nexy_Keypos1, key1);
            Em.key.Add(nexy_Keypos2, key2);
            Em.key.Add(nexy_Keypos3, key3);
            Em.key.Add(nexy_Keypos4, key4);
            Em.key.Add(nexy_Keypos5, key5);
            #endregion
#region//添加门
            Gate gate1 = new Gate();
            Gate gate2 = new Gate();
            Gate gate3 = new Gate();
            Gate gate4 = new Gate();
            Gate gate5 = new Gate();
            Gate gate6 = new Gate();
            Gate gate7 = new Gate();
            int next_Gatepos1 = gate1.TypeGate(ConsoleKey2.Yellow, 3, 4);
            int next_Gatepos2 = gate2.TypeGate(ConsoleKey2.Yellow, 5, 2);
            int next_Gatepos3 = gate3.TypeGate(ConsoleKey2.Yellow, 5, 9);
            int next_Gatepos4 = gate4.TypeGate(ConsoleKey2.Yellow, 6, 6);
            int next_Gatepos5 = gate5.TypeGate(ConsoleKey2.Yellow, 8, 2);
            int next_Gatepos6 = gate6.TypeGate(ConsoleKey2.Yellow, 9, 6);
            int next_Gatepos7 = gate7.TypeGate(ConsoleKey2.Yellow, 9, 10);
            Em.gate.Add(next_Gatepos1, gate1);
            Em.gate.Add(next_Gatepos2, gate2);
            Em.gate.Add(next_Gatepos3, gate3);
            Em.gate.Add(next_Gatepos4, gate4);
            Em.gate.Add(next_Gatepos5, gate5);
            Em.gate.Add(next_Gatepos6, gate6);
            Em.gate.Add(next_Gatepos7, gate7);
            #endregion
#region//添加楼梯
            Stairs stair1 = new Stairs();
            int next_Stirpos1 = stair1.TypeStairs(Dict.Up,1,1);
            Em.stair.Add(next_Stirpos1,stair1);
            #endregion
#region//填加墙壁
            for (int i=1;i<11;i++)
            {
                Wall Tmp_wall = new Wall();
                int next_Wallpos = Tmp_wall.Wallpos(2,i);
                Em.wall.Add(next_Wallpos,Tmp_wall);
            }
            for (int j=4;j<12;j++)
            {
                Wall Tmp_wall = new Wall();
                int next_Wallpos = Tmp_wall.Wallpos(j, 4);
                Em.wall.Add(next_Wallpos, Tmp_wall);
            }
            for (int j = 3; j < 8; j++)
            {
                Wall Tmp_wall = new Wall();
                int next_Wallpos = Tmp_wall.Wallpos(j, 10);
                Em.wall.Add(next_Wallpos, Tmp_wall);
            }
            for (int i = 6; i < 10; i++)
            {
                Wall Tmp_wall = new Wall();
                int next_Wallpos = Tmp_wall.Wallpos(7, i);
                Em.wall.Add(next_Wallpos, Tmp_wall);
            }
            for (int j=9;j<12;j++)
            {
                Wall Tmp_wall = new Wall();
                int next_Wallpos = Tmp_wall.Wallpos(j, 8);
                Em.wall.Add(next_Wallpos, Tmp_wall);
            }
            for (int i = 6; i < 9; i++)
            {
                Wall Tmp_wall = new Wall();
                int next_Wallpos = Tmp_wall.Wallpos(5, i);
                Em.wall.Add(next_Wallpos, Tmp_wall);
            }
            Wall wall1 = new Wall();
            Wall wall2 = new Wall();
            Wall wall3 = new Wall();
            Wall wall4 = new Wall();
            Wall wall5 = new Wall();
            Wall wall6 = new Wall();
            Wall wall7 = new Wall();
            Wall wall8 = new Wall();
            Wall wall9 = new Wall();
            Wall wall10 = new Wall();
            int next_Wallpos1 = wall1.Wallpos(3, 6);
            int next_Wallpos2 = wall2.Wallpos(4, 6);
            int next_Wallpos3 = wall3.Wallpos(5, 1);
            int next_Wallpos4 = wall4.Wallpos(5, 3);
            int next_Wallpos5 = wall5.Wallpos(8, 1);
            int next_Wallpos6 = wall6.Wallpos(8, 3);
            int next_Wallpos7 = wall7.Wallpos(9, 5);
            int next_Wallpos8 = wall8.Wallpos(9, 7);
            int next_Wallpos9 = wall9.Wallpos(9, 9);
            int next_Wallpos10 = wall10.Wallpos(9, 11);
            Em.wall.Add(next_Wallpos1,wall1);
            Em.wall.Add(next_Wallpos2, wall2);
            Em.wall.Add(next_Wallpos3, wall3);
            Em.wall.Add(next_Wallpos4, wall4);
            Em.wall.Add(next_Wallpos5, wall5);
            Em.wall.Add(next_Wallpos6, wall6);
            Em.wall.Add(next_Wallpos7, wall7);
            Em.wall.Add(next_Wallpos8, wall8);
            Em.wall.Add(next_Wallpos9, wall9);
            Em.wall.Add(next_Wallpos10, wall10);
            #endregion
#region//添加楼层初始坐标
            Em.Down_x = 1;
            Em.Down_y = 2;
#endregion
            Program.element.Add(Em);


        }

        public void checkpoint2()//第二层
        {
            Container Em = new Container();
#region//添加地图
            Map maps = new Map();
            maps.Fill_Map();
            maps.Fill_Map2();
            maps.Boundary_Map();
            Em.map1s = maps;

#endregion
#region//添加墙壁,道具,门。
            FileStream fs = new FileStream("map2.txt", FileMode.Open,FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline=null;
            File_Map(strReadline,read,Em);

#endregion
#region//添加楼梯
            Stairs stair1 = new Stairs();
            Stairs stair2 = new Stairs();
            int next_Sairpos1 = stair1.TypeStairs(Dict.Down, 1, 1);
            int next_Sairpos2 = stair2.TypeStairs(Dict.Up, 11, 1);
            Em.stair.Add(next_Sairpos1, stair1);
            Em.stair.Add(next_Sairpos2, stair2);
            #endregion
#region//添加特殊怪物
            Monster ms1 = new Monster();
            Monster ms2 = new Monster();
            int next_Mspos1 = ms1.Giant(2, 6);
            int next_Mspos2 = ms2.Giant(2, 8);
            Em.ms.Add(next_Mspos1, ms1);
            Em.ms.Add(next_Mspos2, ms2);
            #endregion
#region//添加Npc
            Npc npc1 = new Npc();
            Npc npc2 = new Npc();
            int next_Npcpos1 = npc1.Oldman3(4, 11);
            int next_Npcpos2 = npc2.Oldman2(7, 11);
            Em.npc.Add(next_Npcpos1,npc1);
            Em.npc.Add(next_Npcpos2, npc2);
#endregion

            #region//添加楼层初始位置
            Em.Up_x = 2;
            Em.Up_y = 1;
            Em.Down_x = 10;
            Em.Down_y = 1;
#endregion
            Program.element.Add(Em);
        }

        public void checkpoint3()//第三层
        {
            Container Em = new Container();

#region//添加地图
            Map maps = new Map();
            maps.Fill_Map();
            maps.Fill_Map2();
            maps.Boundary_Map();
            Em.map1s = maps;

            #endregion
#region//添加道具,怪物,墙壁,门
            FileStream fs = new FileStream("map3.txt", FileMode.Open,FileAccess.Read);
            StreamReader read = new StreamReader(fs,Encoding.Default);
            string strReadline =null;
            File_Map(strReadline,read,Em);
            #endregion

            Npc npc1 = new Npc();
            int nexy_Npcpos = npc1.Oldman1(4,11);
            Em.npc.Add(nexy_Npcpos,npc1);
#region//添加楼梯
            Stairs stair1 = new Stairs();
            Stairs stair2 = new Stairs();
            int next_Sairpos1 = stair1.TypeStairs(Dict.Down, 11, 1);
            int next_Sairpos2 = stair2.TypeStairs(Dict.Up, 11, 11);
            Em.stair.Add(next_Sairpos1, stair1);
            Em.stair.Add(next_Sairpos2, stair2);
            #endregion

#region//添加楼层初始坐标
            Em.Up_x = 11;
            Em.Up_y = 2;
            Em.Down_x = 11;
            Em.Down_y = 10;
            #endregion

            Program.element.Add(Em);
        }

        public void checkpoint4()//第四层
        {
            Container Em = new Container();

#region//添加地图
            Map maps = new Map();
            maps.Fill_Map();
            maps.Fill_Map2();
            maps.Boundary_Map();
            Em.map1s = maps;

            #endregion

#region//添加道具,怪物,墙壁,门
            FileStream fs = new FileStream("map4.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline = null;
            File_Map(strReadline, read, Em);
            #endregion

            Npc npc1 = new Npc();
            Npc npc2 = new Npc();
            int nexy_Npcpos1 = npc1.Devil_Shop(1, 6);
            int nexy_Npcpos2 = npc2.Oldman2(1, 10);
            Em.npc.Add(nexy_Npcpos1, npc1);
            Em.npc.Add(nexy_Npcpos2, npc2);

            #region//添加楼梯
            Stairs stair1 = new Stairs();
            Stairs stair2 = new Stairs();
            int next_Sairpos1 = stair1.TypeStairs(Dict.Down, 11, 11);
            int next_Sairpos2 = stair2.TypeStairs(Dict.Up, 11, 1);
            Em.stair.Add(next_Sairpos1, stair1);
            Em.stair.Add(next_Sairpos2, stair2);
#endregion

#region//添加楼层初始坐标
            Em.Up_x = 10;
            Em.Up_y = 11;
            Em.Down_x = 10;
            Em.Down_y = 1;
            #endregion


            Program.element.Add(Em);
        }

        public void checkpoint5()//第五层
        {
            Container Em = new Container();

            #region//添加地图
            Map maps = new Map();
            maps.Fill_Map();
            maps.Fill_Map2();
            maps.Boundary_Map();
            Em.map1s = maps;

            #endregion

            #region//添加道具,怪物,墙壁,门
            FileStream fs = new FileStream("map5.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline = null;
            File_Map(strReadline, read, Em);
            #endregion

            Func_Prop jump = new Func_Prop();
            int next_Funcpos = jump.Memory_Prop(9,4);
            Em.func.Add(next_Funcpos,jump);
            Prop sj = new Prop();
            int next_PropPos = sj.Sword(11,11);
            Em.eq.Add(next_PropPos,sj);


            #region//添加楼梯
            Stairs stair1 = new Stairs();
            Stairs stair2 = new Stairs();
            int next_Sairpos1 = stair1.TypeStairs(Dict.Down, 11, 1);
            int next_Sairpos2 = stair2.TypeStairs(Dict.Up, 1, 1);
            Em.stair.Add(next_Sairpos1, stair1);
            Em.stair.Add(next_Sairpos2, stair2);
            #endregion

            #region//添加楼层初始坐标
            Em.Up_x = 11;
            Em.Up_y = 2;
            Em.Down_x = 2;
            Em.Down_y = 1;
            #endregion


            Program.element.Add(Em);
        }

        public void checkpoint6()//第六层
        {
            Container Em = new Container();

            #region//添加地图
            Map maps = new Map();
            maps.Fill_Map();
            maps.Fill_Map2();
            maps.Boundary_Map();
            Em.map1s = maps;

            #endregion

            #region//添加道具,怪物,墙壁,门
            FileStream fs = new FileStream("map6.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline = null;
            File_Map(strReadline, read, Em);
            #endregion


            Npc npc1 = new Npc();
            Npc npc2 = new Npc();
            int nexy_Npcpos1 = npc1.Oldman3(8, 4);
            int nexy_Npcpos2 = npc2.Exp_Shop(4, 8);
            Em.npc.Add(nexy_Npcpos1, npc1);
            Em.npc.Add(nexy_Npcpos2, npc2);

            #region//添加楼梯
            Stairs stair1 = new Stairs();
            Stairs stair2 = new Stairs();
            int next_Sairpos1 = stair1.TypeStairs(Dict.Down, 1, 1);
            int next_Sairpos2 = stair2.TypeStairs(Dict.Up, 11, 11);
            Em.stair.Add(next_Sairpos1, stair1);
            Em.stair.Add(next_Sairpos2, stair2);
            #endregion

            #region//添加楼层初始坐标
            Em.Up_x = 2;
            Em.Up_y = 1;
            Em.Down_x = 10;
            Em.Down_y = 11;
            #endregion


            Program.element.Add(Em);
        }

        public void checkpoint7()//第七层
        {
            Container Em = new Container();

            #region//添加地图
            Map maps = new Map();
            maps.Fill_Map();
            maps.Fill_Map2();
            maps.Boundary_Map();
            Em.map1s = maps;

            #endregion

            #region//添加道具,怪物,墙壁,门
            FileStream fs = new FileStream("map7.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline = null;
            File_Map(strReadline, read, Em);
            #endregion

            Npc npc1 = new Npc();
            int nexy_Npcpos1 = npc1.Money_Shop(1, 6);
            Em.npc.Add(nexy_Npcpos1, npc1);

            #region//添加楼梯
            Stairs stair1 = new Stairs();
            Stairs stair2 = new Stairs();
            int next_Sairpos1 = stair1.TypeStairs(Dict.Down, 11, 11);
            int next_Sairpos2 = stair2.TypeStairs(Dict.Up, 1, 1);
            Em.stair.Add(next_Sairpos1, stair1);
            Em.stair.Add(next_Sairpos2, stair2);
            #endregion

            #region//添加楼层初始坐标
            Em.Up_x = 10;
            Em.Up_y = 11;
            Em.Down_x = 2;
            Em.Down_y = 1;
            #endregion


            Program.element.Add(Em);
        }

        public void checkpoint8()//第八层
        {
            Container Em = new Container();

            #region//添加地图
            Map maps = new Map();
            maps.Fill_Map();
            maps.Fill_Map2();
            maps.Boundary_Map();
            Em.map1s = maps;

            #endregion

            #region//添加道具,怪物,墙壁,门
            FileStream fs = new FileStream("map8.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline = null;
            File_Map(strReadline, read, Em);
            #endregion

            Monster ms1 = new Monster();
            Monster ms2 = new Monster();
            int next_Mspos1 = ms1.Giant(5, 9);
            int next_Mspos2 = ms2.Giant(5, 11);
            Em.ms.Add(next_Mspos1, ms1);
            Em.ms.Add(next_Mspos2, ms2);

            #region//添加楼梯
            Stairs stair1 = new Stairs();
            Stairs stair2 = new Stairs();
            int next_Sairpos1 = stair1.TypeStairs(Dict.Down, 1, 1);
            int next_Sairpos2 = stair2.TypeStairs(Dict.Up, 1, 6);
            Em.stair.Add(next_Sairpos1, stair1);
            Em.stair.Add(next_Sairpos2, stair2);
            #endregion

            #region//添加楼层初始坐标
            Em.Up_x = 2;
            Em.Up_y = 1;
            Em.Down_x = 2;
            Em.Down_y = 6;
            #endregion


            Program.element.Add(Em);
        }

        public void checkpoint9()//第九层
        {
            Container Em = new Container();

            #region//添加地图
            Map maps = new Map();
            maps.Fill_Map();
            maps.Fill_Map2();
            maps.Boundary_Map();
            Em.map1s = maps;

            #endregion

            #region//添加道具,怪物,墙壁,门
            FileStream fs = new FileStream("map9.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline = null;
            File_Map(strReadline, read, Em);
            #endregion

            Prop sd = new Prop();
            int next_PropPos = sd.Shield(7,9);
            Em.eq.Add(next_PropPos,sd);

            #region//添加楼梯
            Stairs stair1 = new Stairs();
            Stairs stair2 = new Stairs();
            int next_Sairpos1 = stair1.TypeStairs(Dict.Down, 1, 6);
            int next_Sairpos2 = stair2.TypeStairs(Dict.Up, 11, 1);
            Em.stair.Add(next_Sairpos1, stair1);
            Em.stair.Add(next_Sairpos2, stair2);
            #endregion

            #region//添加楼层初始坐标
            Em.Up_x = 2;
            Em.Up_y = 6;
            Em.Down_x = 10;
            Em.Down_y = 1;
            #endregion


            Program.element.Add(Em);
        }

        public void checkpoint10()//第十层
        {
            Container Em = new Container();

            #region//添加地图
            Map maps = new Map();
            maps.Fill_Map();
            maps.Fill_Map2();
            maps.Boundary_Map();
            Em.map1s = maps;

            #endregion

            #region//添加道具,怪物,墙壁,门
            FileStream fs = new FileStream("map10.txt", FileMode.Open, FileAccess.Read);
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline = null;
            File_Map(strReadline, read, Em);
            #endregion

            #region//添加楼梯
            Stairs stair1 = new Stairs();
            Stairs stair2 = new Stairs();
            int next_Sairpos1 = stair1.TypeStairs(Dict.Down, 11, 1);
            int next_Sairpos2 = stair2.TypeStairs(Dict.Up, 1, 6);
            Em.stair.Add(next_Sairpos1, stair1);
            Em.stair.Add(next_Sairpos2, stair2);
            #endregion

            Monster mw = new Monster();
            int next_Mspos = mw.Devil(3,6);
            Em.ms.Add(next_Mspos,mw);

            #region//添加楼层初始坐标
            Em.Up_x = 10;
            Em.Up_y = 1;
            Em.Down_x = 2;
            Em.Down_y = 6;
            #endregion


            Program.element.Add(Em);
        }


        public void check()//加载关卡
        {
            checkpoint1();
            checkpoint2();
            checkpoint3();
            checkpoint4();
            checkpoint5();
            checkpoint6();
            checkpoint7();
            checkpoint8();
            checkpoint9();
            checkpoint10();
        }

        public void File_Map(String str,StreamReader read,Container Em)//读取文件中的位置，变换成各种物品！
        {
            int y = 0;
            while ((str = read.ReadLine()) != null)
            {

                for (int x=0;x<str.Length;++x)
                {
                    if (str[x] == '#')//墙壁
                    {
                        Wall wall2 = new Wall();
                        int next_Wallpos = wall2.Wallpos(y, x);
                        Em.wall.Add(next_Wallpos, wall2);
                    }
                    else if (str[x] == 'l')//蓝血瓶
                    {
                        Prop prop1 = new Prop();
                        int next_PopPos = prop1.PotionBlue(y, x);
                        Em.eq.Add(next_PopPos, prop1);
                    }
                    else if (str[x] == 'h')//红血瓶
                    {
                        Prop prop2 = new Prop();
                        int next_PopPos = prop2.PotionRed(y, x);
                        Em.eq.Add(next_PopPos, prop2);
                    }
                    else if (str[x] == 'r')//红色门
                    {
                        Gate gate1 = new Gate();
                        int next_Gatepos = gate1.TypeGate(ConsoleKey2.red, y, x);
                        Em.gate.Add(next_Gatepos, gate1);
                    }
                    else if (str[x] == 'b')//蓝色门
                    {
                        Gate gate2 = new Gate();
                        int next_Gatepos = gate2.TypeGate(ConsoleKey2.Blue, y, x);
                        Em.gate.Add(next_Gatepos, gate2);
                    }
                    else if (str[x] == 'y')//黄色门
                    {
                        Gate gate3 = new Gate();
                        int next_Gatepos = gate3.TypeGate(ConsoleKey2.Yellow, y, x);
                        Em.gate.Add(next_Gatepos, gate3);
                    }
                    else if (str[x] == 'k')//黄钥匙
                    {
                        Key key1 = new Key();
                        int next_Keypos = key1.TypeKey(ConsoleKey2.Yellow, y, x);
                        Em.key.Add(next_Keypos, key1);
                    }
                    else if (str[x] == 'e')//蓝钥匙
                    {
                        Key key2 = new Key();
                        int next_Keypos = key2.TypeKey(ConsoleKey2.Blue, y, x);
                        Em.key.Add(next_Keypos, key2);
                    }
                    else if (str[x] == 'd')//红钥匙
                    {
                        Key key3 = new Key();
                        int next_Keypos = key3.TypeKey(ConsoleKey2.red, y, x);
                        Em.key.Add(next_Keypos, key3);
                    }
                    else if (str[x] == 'o')//蓝宝石
                    {
                        Prop prop3 = new Prop();
                        int next_PropPos = prop3.GemstoneBlue(y, x);
                        Em.eq.Add(next_PropPos, prop3);
                    }
                    else if (str[x] == 'p')//红宝石
                    {
                        Prop prop4 = new Prop();
                        int next_PropPos = prop4.GemstoneRed(y, x);
                        Em.eq.Add(next_PropPos, prop4);
                    }
                    else if (str[x] == 'm')//史莱姆
                    {
                        Monster ms1 = new Monster();
                        int next_Mspos = ms1.Slime(y, x);
                        Em.ms.Add(next_Mspos, ms1);
                    }
                    else if (str[x] == 'f')//小蝙蝠
                    {
                        Monster ms2 = new Monster();
                        int next_Mspos = ms2.Bat(y, x);
                        Em.ms.Add(next_Mspos, ms2);
                    }
                    else if (str[x] == 'g')//骷髅怪
                    {
                        Monster ms3 = new Monster();
                        int next_Mspos = ms3.Skeleton(y, x);
                        Em.ms.Add(next_Mspos, ms3);
                    }
                    else if (str[x] == 'j')//骷髅将军
                    {
                        Monster ms4 = new Monster();
                        int next_Mspos = ms4.Skeleton2(y, x);
                        Em.ms.Add(next_Mspos, ms4);
                    }
                    else if (str[x] == 's')//暗黑法师
                    {
                        Monster ms5 = new Monster();
                        int next_Mspos = ms5.Master(y, x);
                        Em.ms.Add(next_Mspos, ms5);
                    }

                }
                y += 1;
            }
        }
    }
}
