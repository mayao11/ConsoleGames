using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyBattle
{
    class BaseCharacter
    {
        internal delegate Skill EnemyAction(BaseCharacter bc, List<BaseCharacter> bcList, int counts);
        public string name;
        public int maxHp;
        public int maxMp;
        public int Hp;
        public int Mp;
        public int atk;
        public int def;
        public int mat;
        public int men;
        /// <summary>
        /// 命中率
        /// </summary>
        public int hit;
        /// <summary>
        /// 暴击率
        /// </summary>
        public int crt;
        public int level;
        public CharacterType type;
        public ActionMode actionType = ActionMode.Normal;

        /// <summary>
        /// 敌方怪物的行动模式，获得一个可以使用的技能
        /// </summary>
        public EnemyAction GetRandomSkill = null;

        public List<Skill> skill = new List<Skill>();
        public List<State> state = new List<State>();

        /// <summary>
        /// 判段角色是否具有某种状态
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        public bool HasState(StateType stateType)
        {
            foreach (State s in state)
            {
                if (s.type == stateType)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断角色是否具有无敌状态
        /// </summary>
        /// <returns></returns>
        public bool IsInvincible()
        {
            bool temp = false;
            foreach (State s in state)
            {
                if (s.type == StateType.Invincible)
                {
                    MyDraw.DrawBattleMessageDelay(name + "处于无敌状态中，伤害无效！");
                    if (s.times > 0)
                    {
                        s.times -= 1;
                    }
                    temp = true;
                }
            }
            if (temp)
                CleanUpState();
            return temp;
        }

        /// <summary>
        /// 判断角色是否被沉默
        /// </summary>
        /// <returns></returns>
        public bool IsSilence()
        {
            bool temp = false;
            foreach (State s in state)
            {
                if (s.type == StateType.Silence)
                {
                    MyDraw.DrawBattleMessageDelay(name + "处于沉默状态中，无法释放技能！");
                    if (s.times > 0)
                    {
                        s.times -= 1;
                    }
                    temp = true;
                }
            }
            if (temp)
                CleanUpState();
            return temp;
        }

        /// <summary>
        /// 判断角色是否具有物理反射
        /// </summary>
        /// <returns></returns>
        public bool IsPhysicalReflect()
        {
            bool temp = false;
            foreach (State s in state)
            {
                if (s.type == StateType.PhysicalReflect)
                {
                    MyDraw.DrawBattleMessageDelay(name + "处于物理反射状态中，攻击被弹了回来！");
                    if (s.times > 0)
                    {
                        s.times -= 1;
                    }
                    temp =  true;
                }
            }
            if (temp)
                CleanUpState();
            return temp;
        }

        /// <summary>
        /// 判断角色是否具有魔法反射
        /// </summary>
        /// <returns></returns>
        public bool IsMagicReflect()
        {
            bool temp = false;
            foreach (State s in state)
            {
                if (s.type == StateType.MagicReflect)
                {
                    MyDraw.DrawBattleMessageDelay(name + "处于魔法反射状态中，魔法被弹了回来");
                    if (s.times > 0)
                    {
                        s.times -= 1;
                    }
                    temp =  true;
                }
            }
            if (temp)
                CleanUpState();
            return temp;
        }

        /// <summary>
        /// 判断角色是否具有复活状态
        /// </summary>
        /// <returns></returns>
        public bool IsRevive()
        {
            bool temp = false;
            foreach (State s in state)
            {
                if (s.type == StateType.Revive)
                {
                    MyDraw.DrawBattleMessageDelay(name + "处于复活状态中，重新站了起来！");
                    Hp = s.hprecover;
                    if (s.times > 0)
                    {
                        s.times -= 1;
                    }
                    temp = true;
                }
            }
            if (temp)
                CleanUpState();
            return temp;
        }

        /// <summary>
        /// 判断角色是否具有眩晕状态
        /// </summary>
        /// <returns></returns>
        public bool IsDizzy()
        {
            bool temp = false;
            foreach (State s in state)
            {
                if (s.type == StateType.Dizzy)
                {
                    MyDraw.DrawBattleMessageDelay(name + "处于" + s.name + "状态中，无法行动！");
                    if (s.times > 0)
                    {
                        s.times -= 1;
                    }
                    temp = true;
                }
            }
            if (temp)
                CleanUpState();
            return temp;
        }

        /// <summary>
        /// 判断角色是否禁止生命恢复
        /// </summary>
        /// <returns></returns>
        public bool IsForbidHeal()
        {
            bool temp = false;
            foreach (State s in state)
            {
                if (s.type == StateType.ForbidHeal)
                {
                    MyDraw.DrawBattleMessageDelay(name + "处于" + s.name + "状态中，无法恢复生命值！");
                    if (s.times > 0)
                    {
                        s.times -= 1;
                    }
                    temp = true;
                }
            }
            if (temp)
                CleanUpState();
            return temp;
        }

        public double IsTaunt()
        {
            foreach (State s in state)
            {
                if (s.type == StateType.Taunt)
                {
                    if (s.times > 0)
                    {
                        s.times -= 1;
                    }
                    return s.ratio;
                }
            }
            return -1.0;
        }

        /// <summary>
        /// 判断角色是否能被当前技能选为目标
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool CanBeTarget(Skill s)
        {
            if (Hp > 0)
                return true;
            else
            {
                if (s.canDeath)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 添加一个状态
        /// </summary>
        /// <param name="s"></param>
        public void AddState(State s)
        {
            int i = 0;
            for (; i < state.Count; i++)
            {
                if (state[i].name == s.name && state[i].skillName == s.skillName)
                {
                    state[i].times = s.times;
                    state[i].counts = s.counts;
                    MyDraw.DrawBattleMessageDelay(name + "的" + s.name + "状态持续时间延长！");
                    return;
                }
            }
            s.AddState?.Invoke(this);
            state.Add(s);
            MyDraw.DrawBattleMessageDelay(name + "获得了" + s.name + "状态");
        }

        /// <summary>
        /// 移除一个状态
        /// </summary>
        /// <param name="s"></param>
        public void RemoveState(State s)
        {
            int i = 0;
            for (; i < state.Count; i++)
            {
                if (state[i].name == s.name && state[i].skillName == s.skillName)
                {
                    state[i]?.RemoveState(this);
                    state.RemoveAt(i);
                    MyDraw.DrawBattleMessageDelay(name + "失去了" + s.name + "状态");
                    break;
                }
            }
        }

        /// <summary>
        /// 执行HOT效果
        /// </summary>
        /// <param name="index"></param>
        public void ExeCountStartState(int index)
        {
            foreach (State s in state)
            {
                if (s.type != StateType.DamageOverTime && s.type != StateType.HealOverTime)
                    continue;
                string text = string.Format("因为{0}的效果！", s.name);
                if (s.type == StateType.HealOverTime)
                {
                    if (s.times == 0 && s.counts == 0)
                        continue;
                    if (s.times > 0)
                    {
                        s.times--;
                    }
                    MyDraw.DrawBattleMessage(text);
                    if(!IsForbidHeal())
                        GetRecover(s.hprecover, index);
                }
            }
        }

        /// <summary>
        /// 执行DOT效果
        /// </summary>
        /// <param name="index"></param>
        public void ExeCountEndState(int index)
        {
            foreach (State s in state)
            {
                if (s.type != StateType.DamageOverTime && s.type != StateType.HealOverTime)
                    continue;
                string text = string.Format("因为{0}的效果！", s.name);
                if (s.type == StateType.DamageOverTime)
                {
                    if (s.times == 0 && s.counts == 0)
                        continue;
                    if (s.times > 0)
                    {
                        s.times--;
                    }
                    MyDraw.DrawBattleMessage(text);
                    if(!IsInvincible())
                        GetDamage(s.damage, index);
                }
            }
        }

        /// <summary>
        /// 对与持续回合类的状态，持续回合减1
        /// </summary>
        public void StateCountDec()
        {
            foreach (State s in state)
            {
                if (s.counts > 0)
                    s.counts--;
            }
        }
        /// <summary>
        /// 清理角色的状态栏，将次数和回合数为一的状态删除
        /// </summary>
        public void CleanUpState()
        {
            List<int> list = new List<int>();
            for (int i = state.Count - 1; i >= 0; i--)
            {
                if (state[i].counts == 0 && state[i].times == 0)
                {
                    list.Add(i);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                string text = string.Format("{0}失去了{1}状态", name, state[list[i]].name);
                MyDraw.DrawBattleMessageDelay(text);
                state.RemoveAt(list[i]);
            }
        }

        /// <summary>
        /// 清空角色的状态栏
        /// </summary>
        public void ResetState()
        {
            state.Clear();
        }

        /// <summary>
        /// 角色受到伤害
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool GetDamage(int damage, int index)
        {
            MyDraw.DrawDamageAnimation(this, index);
            int tempDamage = 0;
            if (Hp >= damage)
            {
                tempDamage = damage;
                Hp -= damage;
            }
            else
            {
                tempDamage = Hp;
                Hp = 0;
            }
            string  text = string.Format("{0}受到了{1}点伤害", name, tempDamage);
            MyDraw.DrawCharacterInfo(this, index);
            MyDraw.DrawBattleMessageDelay(text);
            if (!IsAlive())
            {
                if (!IsRevive())
                {
                    MyDraw.DrawBattleMessageDelay(name + "死亡了");
                    ResetState();
                }
                MyDraw.DrawCharacterInfo(this, index);
            }
            return IsAlive();
        }

        /// <summary>
        /// 角色受到的恢复量
        /// </summary>
        /// <param name="recover"></param>
        /// <param name="index"></param>
        public void GetRecover(int recover, int index)
        {
            MyDraw.DrawEffectAnimation(this, index);
            int tempRecover = 0;
            if (maxHp <= recover + Hp)
            {
                tempRecover = maxHp - Hp;
                Hp = maxHp;
            }
            else
            {
                tempRecover = recover;
                Hp += tempRecover;
            }
            string text = string.Format("{0}恢复了{1}点生命值", name, tempRecover);
            MyDraw.DrawCharacterInfo(this, index);
            MyDraw.DrawBattleMessageDelay(text);
        }

        /// <summary>
        /// 获取指定状态的伤害加成
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="stateType"></param>
        /// <returns></returns>
        public int GetIncreaseDamage(int damage, StateType stateType)
        {
            int tempDamage = damage;
            foreach (State s in state)
            {
                if (s.type == stateType)
                {
                    if(s.counts > 0 || s.times > 0)
                        tempDamage = Convert.ToInt32(tempDamage * (1 + s.ratio));
                }
            }
            return tempDamage;
        }

        public void IncreaseStateDec(StateType stateType)
        {
            foreach (State s in state)
            {
                if (s.type == stateType)
                {
                    if (s.times > 0)
                    {
                        s.times--;
                    }
                }
            }
        }

        public bool IsAlive()
        {
            if (Hp == 0)
                return false;
            return true;
        }
    }
}
