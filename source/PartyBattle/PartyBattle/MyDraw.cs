using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PartyBattle
{
    /// <summary>
    /// 用于向控制台输出显示信息的类
    /// </summary>
    static class MyDraw
    {
        /// <summary>
        /// 清空指定一行的显示信息
        /// </summary>
        /// <param name="line"></param>
        public static void ClearLine(int line)
        {
            Console.SetCursorPosition(0, line);
            for (int i = 0; i < Program.width; i++)
            {
                Console.Write(' ');
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 在指定行的指定开始位置清空指定宽度的内容
        /// </summary>
        /// <param name="line"></param>
        /// <param name="start"></param>
        /// <param name="width"></param>
        public static void ClearLine(int line, int start, int width)
        {
            Console.SetCursorPosition(start, line);
            for (int i = 0; i < width; i++)
            {
                Console.Write(' ');
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 输出50个空白字符，刚好是屏幕的一半，用于擦除角色或者敌人信息使用
        /// </summary>
        private static void WriteEmpty()
        {
            WriteEmpty(50);
        }

        private static void WriteEmpty(int count)
        {
            for (int i = 0; i < count; i++)
                Console.Write(' ');
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 清空对应序号的玩家或者敌人信息
        /// </summary>
        /// <param name="b"></param>
        /// <param name="i"></param>
        private static void ClearCharacterInfo(BaseCharacter b, int i)
        {
            if (b.type == CharacterType.Hero)
            {
                Console.SetCursorPosition(0, 4 * i + 2);
                WriteEmpty();
                Console.SetCursorPosition(0, 4 * i + 3);
                WriteEmpty();
                Console.SetCursorPosition(0, 4 * i + 4);
                WriteEmpty();
            }
            if (b.type == CharacterType.Enemy)
            {
                Console.SetCursorPosition(50, 4 * i + 2);
                WriteEmpty();
                Console.SetCursorPosition(50, 4 * i + 3);
                WriteEmpty();
                Console.SetCursorPosition(50, 4 * i + 4);
                WriteEmpty();
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 显示指定角色的状态栏
        /// </summary>
        /// <param name="bc"></param>
        /// <param name="index"></param>
        public static void DrawState(BaseCharacter bc, int index)
        {
            string temp = "";
            foreach (State s in bc.state)
            {
                switch (s.type)
                {
                    case StateType.DamageOverTime:
                        temp += "DOT ";
                        break;
                    case StateType.Silence:
                        temp += "SI ";
                        break;
                    case StateType.Buff:
                        if (s.atkUp > 0)
                            temp += "AU ";
                        if (s.defUp > 0)
                            temp += "DU ";
                        if (s.atkUp < 0)
                            temp += "AD ";
                        if (s.defUp < 0)
                            temp += "DD ";
                        break;
                    case StateType.MagicReflect:
                        temp += "MR ";
                        break;
                    case StateType.PhysicalReflect:
                        temp += "PR ";
                        break;
                    case StateType.PhysicalDamageIncrease:
                        temp += "PI ";
                        break;
                    case StateType.PhysicalBeDamageIncrease:
                        temp += "PBD ";
                        break;
                    case StateType.Taunt:
                        temp += "TA ";
                        break;
                    case StateType.Revive:
                        temp += "RE ";
                        break;
                    case StateType.Invincible:
                        temp += "IV ";
                        break;
                    case StateType.HealOverTime:
                        temp += "HOT ";
                        break;
                    case StateType.Dizzy:
                        temp += "DI ";
                        break;
                    case StateType.ForbidHeal:
                        temp += "FH ";
                        break;
                    default:
                        break;
                }
            }
            if (bc.type == CharacterType.Hero)
            {
                Console.SetCursorPosition(0, index * 4 + 5);
                WriteEmpty();
                Console.SetCursorPosition(0, index * 4 + 5);
            }
            else
            {
                int len = GetLength(temp);
                Console.SetCursorPosition(Program.width - len - 1, index * 4 + 5);
                WriteEmpty(len);
                Console.SetCursorPosition(Program.width - len - 1, index * 4 + 5);
            }
            if (temp != "")
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(temp);
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 播放玩家攻击敌人时的动画效果
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="att_index"></param>
        /// <param name="target"></param>
        /// <param name="tar_index"></param>
        public static void DrawAttackAnimation(BaseCharacter attacker, int att_index, BaseCharacter target, int tar_index)
        {
            int xStart = GetLength("HP:" + attacker.Hp) + 2;
            int xEnd = Program.width - GetLength("HP:" + target.Hp) - 2;
            int xMiddle = (xEnd - xStart) / 2;
            int yStart = att_index * 4 + 3;
            int yEnd = tar_index * 4 + 3;
            for (int i = xStart; i < xEnd; i = i + 2)
            {
                if (i == xMiddle || i == xMiddle + 1)
                {
                    Console.SetCursorPosition(i - 2, yStart);
                    Console.Write("  ");
                    if (yStart > yEnd)
                    {
                        for (int j = yStart; j >= yEnd; j--)
                        {
                            Console.SetCursorPosition(i, j + 1);
                            Console.Write(" ");
                            Console.SetCursorPosition(i, j);
                            Console.Write("↑");
                            Thread.Sleep(20);
                        }
                    }
                    else if (yStart < yEnd)
                    {
                        for (int j = yStart; j <= yEnd; j++)
                        {
                            Console.SetCursorPosition(i, j - 1);
                            Console.Write(" ");
                            Console.SetCursorPosition(i, j);
                            Console.Write("↓");
                            Thread.Sleep(20);
                        }
                    }
                }
                else if (i < xMiddle)
                {
                    Console.SetCursorPosition(i - 2, yStart);
                    Console.Write("  ");
                    Console.SetCursorPosition(i, yStart);
                    Console.Write("→");
                }
                else
                {
                    Console.SetCursorPosition(i - 2, yEnd);
                    Console.Write("  ");
                    Console.SetCursorPosition(i, yEnd);
                    Console.Write("→");
                }
                Thread.Sleep(20);
            }
            Console.SetCursorPosition(xEnd - 2, yEnd);
            Console.Write("  ");
        }

        /// <summary>
        /// 播放敌人攻击玩家时的动画效果
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="att_index"></param>
        /// <param name="target"></param>
        /// <param name="tar_index"></param>
        public static void DrawAttackAnimationEnemy(BaseCharacter attacker, int att_index, BaseCharacter target, int tar_index)
        {
            int xStart = Program.width - GetLength("HP:" + attacker.Hp) - 2;
            int xEnd = GetLength("HP:" + target.Hp) + 2;
            int xMiddle = (xStart - xEnd) / 2;
            int yStart = att_index * 4 + 3;
            int yEnd = tar_index * 4 + 3;
            for (int i = xStart; i > xEnd; i = i - 2)
            {
                if (i == xMiddle || i == xMiddle - 1)
                {
                    Console.SetCursorPosition(i + 2, yStart);
                    Console.Write("  ");
                    if (yStart > yEnd)
                    {
                        for (int j = yStart; j >= yEnd; j--)
                        {
                            Console.SetCursorPosition(i, j + 1);
                            Console.Write(" ");
                            Console.SetCursorPosition(i, j);
                            Console.Write("↑");
                            Thread.Sleep(20);
                        }
                    }
                    else if (yStart < yEnd)
                    {
                        for (int j = yStart; j <= yEnd; j++)
                        {
                            Console.SetCursorPosition(i, j - 1);
                            Console.Write(" ");
                            Console.SetCursorPosition(i, j);
                            Console.Write("↓");
                            Thread.Sleep(20);
                        }
                    }
                }
                else if (i < xMiddle)
                {
                    if (i != xStart)
                    {
                        Console.SetCursorPosition(i + 2, yEnd);
                        Console.Write("  ");
                    }
                    Console.SetCursorPosition(i, yEnd);
                    Console.Write("←");
                }
                else
                {
                    if (i != xStart)
                    {
                        Console.SetCursorPosition(i + 2, yStart);
                        Console.Write("  ");
                    }
                    Console.SetCursorPosition(i, yStart);
                    Console.Write("←");
                }
                Thread.Sleep(20);
            }
            Console.SetCursorPosition(xEnd + 2, yEnd);
            Console.Write("  ");
        }

        /// <summary>
        /// 播放受到伤害时候的动画
        /// </summary>
        /// <param name="b"></param>
        /// <param name="i"></param>
        public static void DrawDamageAnimation(BaseCharacter b, int i)
        {
            DrawCharacterInfoOpsColor(b, i, 5, 3);
            Thread.Sleep(100);
            DrawCharacterInfo(b, i);
            Thread.Sleep(100);
            DrawCharacterInfoOpsColor(b, i, 5, 3);
            Thread.Sleep(100);
            DrawCharacterInfo(b, i);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 播放添加状态时候的闪烁的效果
        /// </summary>
        /// <param name="b"></param>
        /// <param name="i"></param>
        public static void DrawEffectAnimation(BaseCharacter b, int i)
        {
            DrawCharacterInfoOpsColor(b, i, 15, 9);
            Thread.Sleep(100);
            DrawCharacterInfo(b, i);
            Thread.Sleep(100);
            DrawCharacterInfoOpsColor(b, i, 15, 9);
            Thread.Sleep(100);
            DrawCharacterInfo(b, i);
            Thread.Sleep(100);
        }

        /// <summary>
        /// 按照指定颜色显示角色信息，第一个int是玩家信息的颜色，第二个是敌人的颜色，用于闪烁
        /// </summary>
        /// <param name="b"></param>
        /// <param name="i"></param>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        private static void DrawCharacterInfoOpsColor(BaseCharacter b, int i, int color1, int color2)
        {
            ClearCharacterInfo(b, i);
            if (b.type == CharacterType.Hero)
            {
                Console.ForegroundColor = (ConsoleColor)(color1);
                Console.SetCursorPosition(0, 4 * i + 2);
                Console.Write(b.name);
                Console.SetCursorPosition(0, 4 * i + 3);
                Console.Write("HP:" + b.Hp);
                Console.SetCursorPosition(0, 4 * i + 4);
                Console.Write("MP:" + b.Mp);
            }
            if (b.type == CharacterType.Enemy)
            {
                Console.ForegroundColor = (ConsoleColor)(color2);
                int temp = GetLength(b.name);
                Console.SetCursorPosition(Program.width - temp, 4 * i + 2);
                Console.Write(b.name);
                temp = GetLength("HP:" + b.Hp);
                Console.SetCursorPosition(Program.width - temp, 4 * i + 3);
                Console.Write("HP:" + b.Hp);
                temp = GetLength("MP:" + b.Mp);
                Console.SetCursorPosition(Program.width - temp, 4 * i + 4);
                Console.Write("MP:" + b.Mp);
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 显示指定序号的玩家或者敌人信息
        /// </summary>
        /// <param name="b"></param>
        /// <param name="i"></param>
        public static void DrawCharacterInfo(BaseCharacter b, int i)
        {
            ClearCharacterInfo(b, i);
            if (b.type == CharacterType.Hero)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, 4 * i + 2);
                Console.Write(b.name);
                Console.SetCursorPosition(0, 4 * i + 3);
                Console.Write("HP:" + b.Hp);
                Console.SetCursorPosition(0, 4 * i + 4);
                Console.Write("MP:" + b.Mp);
                DrawState(b, i);
            }
            if (b.type == CharacterType.Enemy)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                int temp = GetLength(b.name);
                Console.SetCursorPosition(Program.width - temp, 4 * i + 2);
                Console.Write(b.name);
                temp = GetLength("HP:" + b.Hp);
                Console.SetCursorPosition(Program.width - temp, 4 * i + 3);
                Console.Write("HP:" + b.Hp);
                temp = GetLength("MP:" + b.Mp);
                Console.SetCursorPosition(Program.width - temp, 4 * i + 4);
                Console.Write("MP:" + b.Mp);
                DrawState(b, i);
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 显示位于上方的战斗信息，显示完毕后有1S延时
        /// </summary>
        /// <param name="text"></param>
        public static void DrawBattleMessageDelay(string text)
        {
            DrawBattleMessage(text);
            Console.SetCursorPosition(0, 0);
            Thread.Sleep(1000);
        }

        /// <summary>
        /// 显示位于上方的战斗信息，无延时
        /// </summary>
        /// <param name="text"></param>
        public static void DrawBattleMessage(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            ClearLine(1);
            Console.SetCursorPosition(0, 1);
            Console.Write(text);
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 显示位于上方的回合数
        /// </summary>
        /// <param name="counts"></param>
        public static void DrawCounts(int counts)
        {
            ClearChoiceText();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(42, 0);
            Console.Write("第" + counts + "回合，开始！");
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 清空下方行动提示，选择项和对应的说明文字
        /// </summary>
        public static void ClearChoiceText()
        {
            ClearLine(19);
            ClearLine(20);
            ClearLine(21);
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 显示位于下方的行动提示文字
        /// </summary>
        /// <param name="text"></param>
        public static void DrawIntroText(string text)
        {
            Console.SetCursorPosition(0, 19);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(text);
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 显示下方的单个选择项
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        public static void DrawChoiceText(int index, string name, int pos)
        {
            Console.SetCursorPosition(pos, 20);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write((index + 1) + "." + name + "  ");
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 显示下方的选择项的说明文字
        /// </summary>
        /// <param name="s"></param>
        public static void DrawChoiceInfo(Skill s)
        {
            Console.SetCursorPosition(0, 21);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(s.description);
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// 显示当前选中的文字
        /// </summary>
        /// <param name="index"></param>
        /// <param name="name"></param>
        /// <param name="pos"></param>
        public static void DrawChoiced(int index, string name, int pos)
        {
            //反色处理
            Console.BackgroundColor = (ConsoleColor)(15 - (int)ConsoleColor.Black);
            Console.ForegroundColor = (ConsoleColor)(15 - (int)ConsoleColor.Cyan);
            Console.SetCursorPosition(pos, 20);
            Console.Write((index + 1) + "." + name);
            Console.BackgroundColor = ConsoleColor.Black;
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
