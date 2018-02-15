using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace 拳愿阿修罗
{
    /// <summary>
    /// 负责游戏界面的显示与效果渲染
    /// </summary>
    class Draw
    {
        /// <summary>
        /// 画出指定角色的血条与体力条框架,战斗信息框架
        /// </summary>
        public static void DrawFrame(Character cha)
        {
            if (cha.type == CharaType.Player)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("┏");
                for (int i = 0; i < 20; i++)
                {
                    Console.Write("━");
                }
                Console.Write("┓");
                Console.SetCursorPosition(0, 1);
                Console.Write("┃");
                Console.SetCursorPosition(21, 1);
                Console.Write("┃");
                Console.SetCursorPosition(0, 2);
                Console.Write("┣");
                for (int i = 0; i < 20; i++)
                {
                    Console.Write("━");
                }
                Console.Write("┫");
                Console.SetCursorPosition(0, 3);
                Console.Write("┃");
                Console.SetCursorPosition(21, 3);
                Console.Write("┃");
                Console.SetCursorPosition(0, 4);
                Console.Write("┗");
                for (int i = 0; i < 20; i++)
                {
                    Console.Write("━");
                }
                Console.Write("┛");
                Console.SetCursorPosition(0, 0);
            }
            else if (cha.type == CharaType.Computer)
            {
                Console.SetCursorPosition(114 - 22, 0);
                Console.Write("┏");
                for (int i = 0; i < 20; i++)
                {
                    Console.Write("━");
                }
                Console.Write("┓");
                Console.SetCursorPosition(114 - 22, 1);
                Console.Write("┃");
                Console.SetCursorPosition(114 - 1, 1);
                Console.Write("┃");
                Console.SetCursorPosition(114 - 22, 2);
                Console.Write("┣");
                for (int i = 0; i < 20; i++)
                {
                    Console.Write("━");
                }
                Console.Write("┫");
                Console.SetCursorPosition(114 - 22, 3);
                Console.Write("┃");
                Console.SetCursorPosition(114 - 1, 3);
                Console.Write("┃");
                Console.SetCursorPosition(114 - 22, 4);
                Console.Write("┗");
                for (int i = 0; i < 20; i++)
                {
                    Console.Write("━");
                }
                Console.Write("┛");

            }
            Console.SetCursorPosition(31, 9);
            Console.Write("┏");
            for (int i = 0; i < 50; i++)
            {
                Console.Write("━");
            }
            Console.Write("┓");
            for (int i = 0; i < 18; i++)
            {
                Console.SetCursorPosition(31, 10 + i);
                Console.Write("┃");
                Console.SetCursorPosition(82, 10 + i);
                Console.Write("┃");
            }
            Console.SetCursorPosition(31, 9 + 18);
            Console.Write("┗");
            for (int i = 0; i < 50; i++)
            {
                Console.Write("━");
            }
            Console.Write("┛");
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        /// 战斗信息
        /// </summary>
        public static void BattleInfo(string text, int[] n)
        {
            if (n[0] > 16)
            {
                n[0] = 0;
                Clean_BattleInfo();
            }
            Console.SetCursorPosition(33, 10 + n[0]);
            Console.Write(text);
            n[0]++;
        }
        public static void Clean_BattleInfo()
        {
            for (int j = 0; j < 17; j++)
            {
                Console.SetCursorPosition(33, 10 + j);
                for (int i = 0; i < 49; i++)
                {
                    Console.Write(" ");
                }
            }

        }
        public static void Clean_StateInfo(Character cha)
        {
            if(cha.type==CharaType.Player)
            {
                Console.SetCursorPosition(0, 5);            
            }
            if(cha.type==CharaType.Computer)
            {
                Console.SetCursorPosition(57, 5);
            }
            for (int i = 0; i < 57; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        /// 计算血量或者体力条的长度
        /// </summary>
        /// <param name="hp_sta"></param>
        /// <param name="maxhp_sta"></param>
        /// <returns></returns>
        public static int HP_STALength(int hp_sta, int maxhp_sta)
        {
            double percent = (double)hp_sta / maxhp_sta;
            int length = (int)Math.Round(percent * 20);
            if (length < 1)
            {
                return 0;
            }
            else if (length > 20)
            {
                return 20;
            }
            return length;

        }

        /// <summary>
        ///  擦去角色的血量条与体力条 用于修改
        /// </summary>
        /// <param name="cha"></param>
        public static void CleanHP_STA(Character cha)
        {
            int HP_temp = GetLength(Convert.ToString(cha.HP) + "/" + Convert.ToString(cha.MaxHP()));
            int STA_temp = GetLength(Convert.ToString(cha.STA) + "/" + Convert.ToString(cha.MaxSTA()));
            if (cha.type == CharaType.Player)
            {
                Console.SetCursorPosition(1, 1);
                for (int i = 0; i < 20; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(1, 3);
                for (int i = 0; i < 20; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(22, 1);
                for (int i = 0; i < 57 - 22; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(22, 3);
                for (int i = 0; i < 57 - 22; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(0, 0);
            }
            if (cha.type == CharaType.Computer)
            {
                Console.SetCursorPosition(114 - 21, 1);
                for (int i = 0; i < 20; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(114 - 21, 3);
                for (int i = 0; i < 20; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(57, 1);
                for (int i = 0; i < 57 - 22; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(57, 1);
                for (int i = 0; i < 57 - 22; i++)
                {
                    Console.Write(" ");
                }
                Console.SetCursorPosition(0, 0);
            }
        }
        /// <summary>
        /// 显示角色的实时数值信息(包括名字)
        /// </summary>
        public static void CharacterValueInfo(Character cha)
        {
            if (cha.type == CharaType.Player)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 8);
                Console.Write(cha.Name);
                Console.SetCursorPosition(0, 10);
                Console.Write("STR:{0} FRA:{1} AGI:{2}", cha.Strength, cha.Frame, cha.Agile);
                Console.SetCursorPosition(0, 12);
                Console.Write("ATK:{0} DEF:{1}", cha.Damage(), cha.DamageModifier());
                Console.SetCursorPosition(0, 14);
                Console.Write("CRT:{0}% DOGE:{1}%", cha.CritRate(), cha.DodgeRate());

            }
            if (cha.type == CharaType.Computer)
            {
                Console.ForegroundColor = ConsoleColor.White;
                int temp = GetLength(cha.Name);
                Console.SetCursorPosition(114 - temp, 8);
                Console.Write(cha.Name);
                temp = GetLength("STR: FRA: AGI:" + cha.Strength + cha.Frame + cha.Agile);
                Console.SetCursorPosition(114 - temp, 10);
                Console.Write("STR:{0} FRA:{1} AGI:{2}", cha.Strength, cha.Frame, cha.Agile);
                temp = GetLength("ATK: DEF:" + cha.Damage() + cha.DamageModifier());
                Console.SetCursorPosition(114 - temp, 12);
                Console.Write("ATK:{0} DEF:{1}", cha.Damage(), cha.DamageModifier());
                temp = GetLength("CRT:% DOGE:%" + cha.CritRate() + cha.DodgeRate());
                Console.SetCursorPosition(114 - temp, 14);
                Console.Write("CRT:{0}% DOGE:{1}%", cha.CritRate(), cha.DodgeRate());
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        ///  显示体力条与血条
        /// </summary>
        public static void DrawHP_and_STA(Character cha)
        {
            int HP_length = HP_STALength(cha.HP, cha.MaxHP());
            int STA_lenrth = HP_STALength(cha.STA, cha.MaxSTA());
            int HP_temp = GetLength(Convert.ToString(cha.HP) + "/" + Convert.ToString(cha.MaxHP()));
            int STA_temp = GetLength(Convert.ToString(cha.STA) + "/" + Convert.ToString(cha.MaxSTA()));
            if (HP_length > 14)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (HP_length > 6)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (cha.type == CharaType.Player)
            {
                CleanHP_STA(cha);
                Console.SetCursorPosition(1, 1);
                for (int i = 0; i < HP_length; i++)
                {
                    Console.Write("┃");
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(1, 3);
                for (int i = 0; i < STA_lenrth; i++)
                {
                    Console.Write("┃");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(22, 1);
                Console.Write(" {0}/{1}", cha.HP, cha.MaxHP());
                Console.SetCursorPosition(22, 3);
                Console.Write(" {0}/{1}", cha.STA, cha.MaxSTA());
                Console.SetCursorPosition(0, 0);
            }
            if (cha.type == CharaType.Computer)
            {
                CleanHP_STA(cha);
                Console.SetCursorPosition((114 - 21) - (HP_length - 20), 1);
                for (int i = 0; i < HP_length; i++)
                {
                    Console.Write("┃");
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition((114 - 21) - (STA_lenrth - 20), 3);
                for (int i = 0; i < STA_lenrth; i++)
                {
                    Console.Write("┃");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(114 - 23 - HP_temp, 1);
                Console.Write(" {0}/{1}", cha.HP, cha.MaxHP());
                Console.SetCursorPosition(114 - 23 - STA_temp, 3);
                Console.Write(" {0}/{1}", cha.STA, cha.MaxSTA());
                Console.SetCursorPosition(0, 0);
            }
        }
        /// <summary>
        /// 播放受到伤害和负面状态时候的动画
        /// </summary>
        /// <param name="b"></param>
        /// <param name="i"></param>
        public static void DrawDamageAnimation(Character cha)
        {
            DrawNameColor(cha,12);
            Thread.Sleep(100);
            DrawNameColor(cha, 15);
            Thread.Sleep(100);
            DrawNameColor(cha, 12);
            Thread.Sleep(100);
            DrawNameColor(cha, 15);
            Thread.Sleep(100);
            DrawNameBass(cha);
            Thread.Sleep(200);
        }
        /// <summary>
        /// 播放角色受到治疗和增益状态时候的动画
        /// </summary>
        /// <param name="cha"></param>
        public static void DrawHealAnimation(Character cha)
        {
            DrawNameColor(cha, 2);
            Thread.Sleep(100);
            DrawNameColor(cha, 15);
            Thread.Sleep(100);
            DrawNameColor(cha, 2);
            Thread.Sleep(100);
            DrawNameColor(cha, 15);
            Thread.Sleep(100);
            DrawNameBass(cha);
            Thread.Sleep(200);
        }
        /// <summary>
        /// 播放躲闪动画
        /// </summary>
        public static void DrawDogeAnimation(Character cha)
        {
            DrawNameBass(cha);
            CleanName(cha, 8);
            Thread.Sleep(100);
            DrawNamedown(cha,9);
            Thread.Sleep(100);
            CleanName(cha, 9);
            Thread.Sleep(100);
            DrawNamedown(cha,9);
            Thread.Sleep(100);
            DrawNameBass(cha);
            CleanName(cha, 9);
            Thread.Sleep(200);
        }
        /// <summary>
        /// 播放攻击动画
        /// </summary>
        /// <param name="cha"></param>
        public static void DrawAttackAnimation(Character cha)
        {
            CleanfountName(cha, 0);
            DrawfountName(cha, 4);
            Thread.Sleep(100);
            CleanfountName(cha, 4);
            DrawfountName(cha, 8);
            Thread.Sleep(100);
            CleanfountName(cha, 8);
            DrawfountName(cha, 8);
            Thread.Sleep(100);
            CleanfountName(cha, 8);
            DrawfountName(cha, 4);
            Thread.Sleep(100);
            CleanfountName(cha, 4);
            DrawfountName(cha, 0);
            Thread.Sleep(200);

        }
        /// <summary>
        /// 把角色名字用自定义颜色打印,用于受到攻击的动画
        /// </summary>
        /// <param name="cha"></param>
        static void DrawNameColor(Character cha,int color)
        {
            if (cha.type == CharaType.Player)
            {
                Console.ForegroundColor = (ConsoleColor)(color);
                Console.SetCursorPosition(0, 8);
                Console.Write(cha.Name);
            }
            if (cha.type == CharaType.Computer)
            {
                Console.ForegroundColor = (ConsoleColor)(color);
                int temp = GetLength(cha.Name);
                Console.SetCursorPosition(114 - temp, 8);
                Console.Write(cha.Name);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        ///  回复角色名的为白色基本色
        /// </summary>
        /// <param name="cha"></param>
        static void DrawNameBass(Character cha)
        {
            if (cha.type == CharaType.Player)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 8);
                Console.Write(cha.Name);
            }
            if (cha.type == CharaType.Computer)
            {
                Console.ForegroundColor = ConsoleColor.White;
                int temp = GetLength(cha.Name);
                Console.SetCursorPosition(114 - temp, 8);
                Console.Write(cha.Name);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        ///  将角色名前移打印出来,用于制作攻击动画
        /// </summary>
        /// <param name="cha"></param>
        static void DrawfountName(Character cha,int length)
        {
            if (cha.type == CharaType.Player)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(length, 8);
                Console.Write(cha.Name);
            }
            if (cha.type == CharaType.Computer)
            {
                Console.ForegroundColor = ConsoleColor.White;
                int temp = GetLength(cha.Name);
                Console.SetCursorPosition(114 - temp-length, 8);
                Console.Write(cha.Name);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        ///  将前移的角色民清除,用于制作攻击动画
        /// </summary>
        /// <param name="cha"></param>
        static void CleanfountName(Character cha, int length)
        {
            if (cha.type == CharaType.Player)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(length, 8);
                Console.Write(cha.Name);
            }
            if (cha.type == CharaType.Computer)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                int temp = GetLength(cha.Name);
                Console.SetCursorPosition(114 - temp - length, 8);
                Console.Write(cha.Name);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        ///  将角色名字上下移动打印 用于制作躲闪动画
        /// </summary>
        /// <param name="cha"></param>
        static void DrawNamedown(Character cha,int line)
        {
            if (cha.type == CharaType.Player)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0,line);
                Console.Write(cha.Name);
            }
            if (cha.type == CharaType.Computer)
            {
                Console.ForegroundColor = ConsoleColor.White;
                int temp = GetLength(cha.Name);
                Console.SetCursorPosition(114 - temp, line);
                Console.Write(cha.Name);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        ///  清除上下移动名字,用于制作躲闪动画
        /// </summary>
        /// <param name="cha"></param>
        static void CleanName(Character cha,int line)
        {
            if (cha.type == CharaType.Player)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, line);
                Console.Write(cha.Name);
            }
            if (cha.type == CharaType.Computer)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                int temp = GetLength(cha.Name);
                Console.SetCursorPosition(114 - temp, line);
                Console.Write(cha.Name);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        /// 显示位于上方的回合数
        /// </summary>
        /// <param name="counts"></param>
        public static void DrawRound(int round)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(53, 0);
            Console.Write("第" + round + "回合");
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        /// 打印玩家的状态栏(简略)
        /// </summary>
        /// <param name="cha"></param>
        public static void DrawState(Character cha)
        {
            string text = "";
            Console.ForegroundColor = ConsoleColor.White;
            foreach(State state in cha.States)
            {
                switch (state.stateEffectType)
                {
                    case StateEffectType.StrengthChange:
                        text += "STR";
                        break;
                    case StateEffectType.AgileChange:
                        text += "AGI";
                        break;
                    case StateEffectType.HitDamage:
                        text += "ATK";
                        break;
                    case StateEffectType.BeHitDamage:
                        text += "DEF";
                        break;
                    case StateEffectType.HP:
                        text += "HP";
                        break;
                    case StateEffectType.STA:
                        text += "MP";
                        break;
                    default:
                        break;
                }
                if (state.StateType==StateType.Buff)
                {
                    text += "↑";
                    
                }
                if(state.StateType==StateType.Debuff)
                {
                    text += "↓";
                    
                }
                Clean_StateInfo(cha);
                if (cha.type==CharaType.Player)
                {
                    Console.SetCursorPosition(0, 5);
                }
                if(cha.type==CharaType.Computer)
                {
                    int length = GetLength(text);
                    Console.SetCursorPosition(114 - length , 5);
                }
                if (text != "")
                {
                    Console.Write(text);
                }          
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(0, 0);
        }
        /// <summary>
        /// 获取指定文本占据的控制台宽度，英文1位，中文2位
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetLength(string text)
        {
            byte[] bytes;
            int len = 0;
            for (int i = 0; i < text.Length; i++)
            {
                bytes = Encoding.Default.GetBytes(text.Substring(i, 1));
                len += bytes.Length > 1 ? 2 : 1;
            }
            return len;
        }
    }
}
