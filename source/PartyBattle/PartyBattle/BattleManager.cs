using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyBattle
{
    class BattleManager
    {
        public List<BaseCharacter> heros;
        public List<BaseCharacter> enemy;
        /// <summary>
        /// 选择需要的位置信息
        /// </summary>
        public List<int> textPos;
        /// <summary>
        /// 临时存储选中的技能
        /// </summary>
        public Skill tempSkill;
        /// <summary>
        /// 当前行动的角色的序号
        /// </summary>
        public int actionIndex;
        /// <summary>
        /// 选中的地方的序号
        /// </summary>
        public int targetEnemy;
        /// <summary>
        /// 选中的己方的序号
        /// </summary>
        public int targetParty = 0;
        /// <summary>
        /// 战斗进行的回合数
        /// </summary>
        private int counts;

        public BattleManager()
        {
            heros = new List<BaseCharacter>();
            enemy = new List<BaseCharacter>();
            textPos = new List<int>();
            counts = 1;
        }

        public void DrawBattleField()
        {
            for (int i = 0; i < heros.Count; i++)
            {
                MyDraw.DrawCharacterInfo(heros[i], i);
            }
            for (int j = 0; j < enemy.Count; j++)
            {
                MyDraw.DrawCharacterInfo(enemy[j], j);
            }
        }

        public void BattleStart()
        {
            while (true)
            {
                actionIndex = 0;
                targetEnemy = 0;
                MyDraw.DrawCounts(counts);
                ExeCountStartState();
                HerosAction();
                EnemyAction();
                ExeCountEndState();
                StateCountDec();
                if (IsOver())
                {
                    break;
                }
                counts++;
                ClearUpState();
                MyDraw.DrawIntroText("按任意键进入下一回合");
                Console.ReadKey(true);
            }
        }

        /// <summary>
        /// 对每个角色的状态进行检测，在回合开始的时候HOT状态生效
        /// </summary>
        private void ExeCountStartState()
        {
            for (int i = 0; i < heros.Count; i++)
            {
                heros[i].ExeCountStartState(i);
            }
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].ExeCountStartState(i);
            }
        }

        /// <summary>
        /// 对每个角色的状态进行检测，在回合结束的时候DOT状态生效
        /// </summary>
        private void ExeCountEndState()
        {
            for (int i = 0; i < heros.Count; i++)
            {
                heros[i].ExeCountEndState(i);
            }
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].ExeCountEndState(i);
            }
        }

        /// <summary>
        /// 对回合类的状态持续回合减1
        /// </summary>
        private void StateCountDec()
        {
            for (int i = 0; i < heros.Count; i++)
            {
                heros[i].StateCountDec();
            }
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].StateCountDec();
            }
        }

        private void ClearUpState()
        {
            foreach (BaseCharacter h in heros)
            {
                h.CleanUpState();
            }
            foreach (BaseCharacter e in enemy)
            {
                e.CleanUpState();
            }
        }

        private bool IsOver()
        {
            if (IsAllDead(enemy))
            {
                MyDraw.DrawBattleMessage("敌人全灭，玩家胜利！");
                return true;
            }
            if (IsAllDead(heros))
            {
                MyDraw.DrawBattleMessage("玩家全灭，战斗失败！");
                return true;
            }
            return false;
        }

        private bool IsAllDead(List<BaseCharacter> bcList)
        {
            bool isAllDead = true;
            foreach (BaseCharacter b in bcList)
            {
                if (b.Hp > 0)
                {
                    isAllDead = false;
                }
            }
            return isAllDead;
        }

        /// <summary>
        /// 玩家的行动部分
        /// </summary>
        private void HerosAction()
        {
            //玩家的行动
            for (int i = 0; i < heros.Count; i++)
            {
                if (heros[i].Hp == 0 || heros[i].IsDizzy())
                    continue;
                actionIndex = i;
                textPos.Clear();
                SetSkillPos();
                DrawSkill();
                MyDraw.DrawIntroText("轮到" + heros[actionIndex].name + "行动了，请选择指令：");
                tempSkill = heros[actionIndex].skill[GetSkillChoice(heros[actionIndex])];
                MyDraw.DrawBattleMessage(heros[actionIndex].name + "选择了技能：" + tempSkill.name);
                if (tempSkill.targetType == TargetType.EnemySingle)
                {
                    MyDraw.DrawIntroText("请选择目标：");
                    SetEnemyPos();
                    DrawEnemy();
                    targetEnemy = GetEnemyChoice();
                    if (targetEnemy == -1)
                    {
                        i--;
                        continue;
                    }
                    tempSkill.UseSkillSingle(heros[actionIndex], actionIndex, enemy[targetEnemy], targetEnemy);
                }
                if (tempSkill.targetType == TargetType.EnemyMulti)
                {
                    tempSkill.UseSkillMulti(heros[actionIndex], actionIndex, enemy);
                }
                if (tempSkill.targetType == TargetType.PartySingle)
                {
                    MyDraw.DrawIntroText("请选择目标：");
                    SetPartyPos();
                    DrawParty();
                    targetEnemy = GetPartyChoice();
                    if (targetEnemy == -1)
                    {
                        i--;
                        continue;
                    }
                    tempSkill.UseSkillSingle(heros[actionIndex], actionIndex, heros[targetEnemy], targetEnemy);
                }
                if (tempSkill.targetType == TargetType.PartyMulti)
                {
                    tempSkill.UseSkillMulti(heros[actionIndex], actionIndex, heros);
                }
                if (tempSkill.targetType == TargetType.Self)
                {
                    tempSkill.UseSkillSingle(heros[actionIndex], actionIndex, heros[actionIndex], actionIndex);
                }
            }
        }


        private void EnemyAction()
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                if (enemy[i].Hp == 0 || enemy[i].IsDizzy())
                    continue;
                Skill temp = enemy[i].GetRandomSkill(enemy[i], enemy, counts);
                if (temp.targetType == TargetType.Self)
                {
                    temp.UseSkillSingle(enemy[i], i, enemy[i], i);
                }
                if (temp.targetType == TargetType.EnemySingle)
                {
                    int target = GetRandomAttackerTarget(enemy[i], temp, heros);
                    //处理嘲讽状态
                    for (int j = 0; j < heros.Count; j++)
                    {
                        double tauntRatio = heros[j].IsTaunt();
                        if (heros[j].Hp > 0 && tauntRatio != -1.0)
                        {
                            if (tauntRatio > Program.random.NextDouble())
                            {
                                target = j;
                                MyDraw.DrawBattleMessageDelay(string.Format("{0}处于嘲讽状态下，敌人的攻击转了过去。", heros[j].name));
                            }
                            break;
                        }
                    }
                    temp.UseSkillSingle(enemy[i], i, heros[target], target);
                }
                if (temp.targetType == TargetType.EnemyMulti)
                {
                    temp.UseSkillMulti(enemy[i], i, heros);
                }
                if (temp.targetType == TargetType.PartySingle)
                {
                    int target = 0;
                    if (temp.type == SkillType.Heal)
                        target = GetHealTarget(temp, enemy);
                    else
                        target = GetRandomAttackerTarget(enemy[i], temp, enemy);
                    temp.UseSkillSingle(enemy[i], i, enemy[target], target);
                }
                if (temp.targetType == TargetType.PartyMulti)
                {
                    temp.UseSkillMulti(enemy[i], i, enemy);
                }
            }
        }

        /// <summary>
        /// 根据攻击模式选择需要攻击的角色
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="s"></param>
        /// <param name="bcList"></param>
        /// <returns></returns>
        private int GetRandomAttackerTarget(BaseCharacter attacker, Skill s, List<BaseCharacter> bcList)
        {
            int temp = 0;
            int tempHp = bcList[0].Hp;
            //选取血量最多的目标
            if (attacker.actionType == ActionMode.MaxHP)
            {
                for (int i = 0; i < bcList.Count; i++)
                {
                    if (!bcList[i].CanBeTarget(s))
                        continue;
                    if (bcList[i].Hp > tempHp)
                    {
                        temp = i;
                        tempHp = bcList[i].Hp;
                    }
                }
                return temp;
            }
            //选取血量最少的目标
            else if (attacker.actionType == ActionMode.MinHP)
            {
                for (int i = 0; i < bcList.Count; i++)
                {
                    if (!bcList[i].CanBeTarget(s))
                        continue;
                    if (bcList[i].Hp < tempHp)
                    {
                        temp = i;
                        tempHp = bcList[i].Hp;
                    }
                }
                return temp;
            }
            else
            {
                List<int> tempList = new List<int>();
                for (int i = 0; i < bcList.Count; i++)
                {
                    if (bcList[i].CanBeTarget(s))
                        tempList.Add(i);
                }
                return tempList[Program.random.Next(0, tempList.Count)];
            }
        }

        /// <summary>
        /// 选择需要恢复的角色
        /// </summary>
        /// <param name="s"></param>
        /// <param name="bcList"></param>
        /// <returns></returns>
        private int GetHealTarget(Skill s, List<BaseCharacter> bcList)
        {
            int temp = 0;
            int tempHp = bcList[0].Hp;
            for (int i = 0; i < bcList.Count; i++)
            {
                if (!bcList[i].CanBeTarget(s))
                    continue;
                if (bcList[i].Hp < tempHp)
                {
                    temp = i;
                    tempHp = bcList[i].Hp;
                }
            }
            return temp;
        }

        private void SetSkillPos()
        {
            textPos.Clear();
            int len = 0;
            for (int i = 0; i < heros[actionIndex].skill.Count; i++)
            {
                textPos.Add(len);
                len += MyDraw.GetLength((1 + i) + "." + heros[actionIndex].skill[i].name + "  ");
            }
        }

        private void SetEnemyPos()
        {
            textPos.Clear();
            int len = 0;
            for (int i = 0; i < enemy.Count; i++)
            {
                textPos.Add(len);
                len += MyDraw.GetLength((1 + i) + "." + enemy[i].name + "  ");
            }
        }

        private void SetPartyPos()
        {
            textPos.Clear();
            int len = 0;
            for (int i = 0; i < heros.Count; i++)
            {
                textPos.Add(len);
                len += MyDraw.GetLength((1 + i) + "." + heros[i].name + "  ");
            }
        }

        private void DrawSkill()
        {
            for (int i = 0; i < heros[actionIndex].skill.Count; i++)
            {
                MyDraw.DrawChoiceText(i, heros[actionIndex].skill[i].name, textPos[i]);
            }
        }

        private void DrawEnemy()
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                MyDraw.DrawChoiceText(i, enemy[i].name, textPos[i]);
            }
        }

        private void DrawParty()
        {
            for (int i = 0; i < heros.Count; i++)
            {
                MyDraw.DrawChoiceText(i, heros[i].name, textPos[i]);
            }
        }

        public int GetSkillChoice(BaseCharacter b)
        {
            int temp = 0;
            MyDraw.DrawChoiced(temp, b.skill[temp].name, textPos[temp]);
            MyDraw.DrawChoiceInfo(b.skill[temp]);
            bool enter = false;
            while (!enter)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (temp > 0)
                    {
                        MyDraw.ClearLine(21);
                        MyDraw.DrawChoiceText(temp, b.skill[temp].name, textPos[temp]);
                        temp = temp - 1;
                        MyDraw.DrawChoiced(temp, b.skill[temp].name, textPos[temp]);
                        MyDraw.DrawChoiceInfo(b.skill[temp]);
                    }
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (temp < textPos.Count - 1)
                    {
                        MyDraw.ClearLine(21);
                        MyDraw.DrawChoiceText(temp, b.skill[temp].name, textPos[temp]);
                        temp = temp + 1;
                        MyDraw.DrawChoiced(temp, b.skill[temp].name, textPos[temp]);
                        MyDraw.DrawChoiceInfo(b.skill[temp]);
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (b.skill[temp].CanUseSkill(b))
                    {
                        MyDraw.ClearChoiceText();
                        break;
                    }
                }
            }
            return temp;
        }

        public int GetEnemyChoice()
        {
            int temp = 0;
            MyDraw.DrawChoiced(temp, enemy[temp].name, textPos[temp]);
            bool enter = false;
            while (!enter)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (temp > 0)
                    {
                        MyDraw.ClearLine(21);
                        MyDraw.DrawChoiceText(temp, enemy[temp].name, textPos[temp]);
                        temp = temp - 1;
                        MyDraw.DrawChoiced(temp, enemy[temp].name, textPos[temp]);
                    }
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (temp < textPos.Count - 1)
                    {
                        MyDraw.ClearLine(21);
                        MyDraw.DrawChoiceText(temp, enemy[temp].name, textPos[temp]);
                        temp = temp + 1;
                        MyDraw.DrawChoiced(temp, enemy[temp].name, textPos[temp]);
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (enemy[temp].CanBeTarget(tempSkill))
                    {
                        MyDraw.ClearChoiceText();
                        break;
                    }
                    else
                    {
                        MyDraw.DrawBattleMessage("无法选择已经死亡的目标，请重新选择");
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    MyDraw.ClearLine(1);
                    return -1;
                }
            }
            return temp;
        }

        public int GetPartyChoice()
        {
            int temp = 0;
            MyDraw.DrawChoiced(temp, heros[temp].name, textPos[temp]);
            bool enter = false;
            while (!enter)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (temp > 0)
                    {
                        MyDraw.ClearLine(21);
                        MyDraw.DrawChoiceText(temp, heros[temp].name, textPos[temp]);
                        temp = temp - 1;
                        MyDraw.DrawChoiced(temp, heros[temp].name, textPos[temp]);
                    }
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (temp < textPos.Count - 1)
                    {
                        MyDraw.ClearLine(21);
                        MyDraw.DrawChoiceText(temp, heros[temp].name, textPos[temp]);
                        temp = temp + 1;
                        MyDraw.DrawChoiced(temp, heros[temp].name, textPos[temp]);
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (heros[temp].CanBeTarget(tempSkill))
                    {
                        MyDraw.ClearChoiceText();
                        break;
                    }
                    else
                    {
                        MyDraw.DrawBattleMessage("无法选择已经死亡的目标，请重新选择");
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    MyDraw.ClearLine(1);
                    return -1;
                }
            }
            return temp;
        }
    }
}
