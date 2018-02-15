using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 拳愿阿修罗
{
    class Character
    {
        /// <summary>
        /// 角色类型
        /// </summary>
        public CharaType type;
        /// <summary>
        /// 角色名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 角色的身份信息介绍
        /// </summary>
        public string Info = "略";
        /// <summary>
        /// 力量值,与伤害相关
        /// </summary>
        int _strength;
        /// <summary>
        /// 限制力量值在1-20之间
        /// </summary>
        public int Strength
        {
            get
            {
                int temp=0;
                foreach (State state in States)
                {
                    temp += state.StrengthChange;
                    if (_strength + temp> 20)
                    {
                        _strength = 20;
                    }
                    else if (_strength + temp < 0)
                    {
                        _strength = 1;
                    }                 
                }
                return _strength+temp;
            }
            set
            {             
                _strength = value;
            }
        }
        /// <summary>
        /// 体格值,与抗击打能力相关
        /// </summary>
        int _frame;
        /// <summary>
        ///  限制体格值在1-20之间;
        /// </summary>
        public int Frame
        {
            get { return _frame; }
            set
            {
                if (value > 20)
                {
                    _frame = 20;
                }
                else if (value < 0)
                {
                    _frame = 1;
                }
                _frame = value;
            }
        }
        /// <summary>
        /// 敏捷值,与暴击和命中相关
        /// </summary>
        int _agile;
        /// <summary>
        ///  限制敏捷值在1-20之间;
        /// </summary>
        public int Agile
        {
            get
            {
                int temp = 0;
                foreach (State state in States)
                {
                    temp += state.AgileChange;
                    if (temp + _agile > 20)
                    {
                        _agile = 20;
                    }
                    else if (temp + _agile < 0)
                    {
                        _agile = 1;
                    }
                }
                return _agile+temp;
            }
            set
            {           
                _agile = value;
            }
        }
        /// <summary>
        /// 当前血量
        /// </summary>
        public int HP;
        /// <summary>
        /// 当前体力值(或者耐力值),类似MP,技能需要消耗体力值才能释放,否则只能普攻
        /// </summary>
        public int STA;
        /// <summary>
        /// 角色预设技能列表(所有技能)
        /// </summary>
        public List<Skill> Skills = new List<Skill>();
        /// <summary>
        /// 当前加载技能
        /// </summary>
        public List<Skill> CurrentSkills = new List<Skill>();
        /// <summary>
        /// 角色状态列表
        /// </summary>
        public List<State> States = new List<State>();
        public Character(string name, int strength, int frame, int agile)
        {
            Name = name;
            Strength = strength;
            Frame = frame;
            Agile = agile;
            HP = MaxHP();
            STA = MaxSTA();

        }
        /// <summary>
        /// 离散值,给予伤害数值一个小幅波动
        /// </summary>
        public int DiscreteValue = 2;
        /// <summary>
        /// 伤害计算函数,伤害随力量呈对数曲线增长
        /// </summary>
        /// <param name="strength"></param>
        /// <returns></returns>
        public int Damage()
        {
            int damage = (int)(Math.Log((Strength / 10.0 + 1), 1.003) * (1 + Utils.random.Next(-DiscreteValue, DiscreteValue) * 0.01));
            return damage;
        }
        /// <summary>
        /// 暴击倍率计算函数,随力量呈对数曲线增长
        /// </summary>
        /// <param name="strength"></param>
        /// <returns></returns>
        public double CriticalRatio()
        {
            double criticalratio = Math.Log(((Strength + 10) / 1000.0 + 1), 1.008);
            return criticalratio;
        }
        /// <summary>
        /// 最大生命值计算函数,随体格呈对数曲线增长
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public int MaxHP()
        {
            int maxhp = (int)Math.Log((Frame / 10.0 + 1), 1.0002772);
            return maxhp;
        }
        /// <summary>
        /// 减免伤害计算函数,即抗击打能力,随体格呈对数曲线增长
        /// </summary>
        /// <param name="frame">体格</param>
        /// <returns></returns>
        public int DamageModifier()
        {
            int damagemodifier = (int)Math.Log((Frame / 10.0 + 1), 1.0055);
            return damagemodifier;
        }
        /// <summary>
        /// 最大体力(或者叫耐力)计算函数,随体格呈对数曲线增长,使用技能的主要消耗
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public int MaxSTA()
        {
            int stamina = (int)Math.Log((Frame / 10.0 + 1), 1.000693);
            return stamina;
        }
        /// <summary>
        /// 暴击率计算函数,随敏捷呈指数增涨
        /// </summary>
        /// <param name="agile"></param>
        /// <returns></returns>
        public int CritRate()
        {
            int critrate = (int)Math.Pow((Agile + 10) / 10.0 + 1, 3.163);
            return critrate;
        }
        /// <summary>
        /// 闪避率计算函数,随敏捷呈指数增涨
        /// </summary>
        /// <param name="agile"></param>
        /// <returns></returns>
        public int DodgeRate()
        {
            int dodgerate = (int)Math.Pow(Agile / 10.0 + 1.3, 3.86);
            return dodgerate;
        }
        /// <summary>
        /// 把角色的技能添加到角色技能列表里
        /// </summary>
        /// <param name="skill"></param>
        public void AddSkill(Skill skill)
        {
            Skills.Add(skill);
        }
        /// <summary>
        /// 判段角色是否具有某种状态
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        public bool HasState(StateType stateType)
        {
            foreach (State s in States)
            {
                if (s.StateType == stateType)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 随机在当前主动技能列表中选择一个技能作为使用技能
        /// </summary>
        /// <param name="list">当前技能列表</param>
        /// <returns></returns>
        public Skill RandomSkill(List<Skill> list)
        {
            Skill skill = null;
            while (skill == null)
            {
                skill = list[Utils.random.Next(list.Count)];
                if (skill.CanUseSkill(this))
                {
                    return skill;
                }
                else
                {
                    skill = null;
                }
                
            }
            return skill;
        }
        /// <summary>
        /// 判断人物是否被命中
        /// </summary>
        /// <param name="cha">被击中者</param>
        /// <returns></returns>
        public bool IsHit()
        {
            return Utils.random.Next(1, 101) > DodgeRate() ? false : true;
        }
        /// <summary>
        /// 判断攻击者的攻击是否触发致命一击
        /// </summary>
        /// <param name="cha">攻击者</param>
        /// <returns></returns>
        public bool IsCrit()
        {
            return Utils.random.Next(1, 101) > CritRate() ? false : true;
        }
        /// <summary>
        /// 将状态添加到人物的状态列表里,包括BUFF和DEBUFF
        /// </summary>
        /// <param name="state"></param>
        public void AddState(State state, int[] index)
        {
            string text;
            for (int i = 0; i < States.Count; i++)
            {
                if (States[i].Name == state.Name && States[i].SkillName == state.SkillName)
                {
                    if (States[i].stateEffectType == StateEffectType.HP || States[i].stateEffectType == StateEffectType.STA)
                    {
                        States[i].HPOverTime += state.HPOverTime;
                        States[i].StaminaOverTime += state.StaminaOverTime;
                    }
                    else
                    {
                        States[i].Times = state.Times;
                        States[i].Rounds = state.Rounds;
                    }
                    text = string.Format("触发了{0}效果", state.Name);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Draw.BattleInfo(text, index);
                    text = string.Format("{0}的{1}效果持续时间延长！", Name, state.Name);
                    Draw.BattleInfo(text, index);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (state.StateType == StateType.Buff)
                    {
                        Draw.DrawHealAnimation(this);
                    }
                    Draw.DrawDamageAnimation(this);
                    return;
                }
            }
            States.Add(state);
            text = string.Format("触发了{0}效果", state.Name);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Draw.BattleInfo(text, index);
            text = string.Format("{0}受到{1}效果的影响", Name, state.Name);
            Draw.BattleInfo(text, index);
            Console.ForegroundColor = ConsoleColor.Gray;
            Draw.Clean_StateInfo(this);
            Draw.DrawState(this);
            Draw.CharacterValueInfo(this);
            if (state.StateType == StateType.Buff)
            {
                Draw.DrawHealAnimation(this);
            }
            Draw.DrawDamageAnimation(this);

        }
        /// <summary>
        /// 清理角色的状态栏，将次数和回合数为一的状态删除
        /// </summary>
        public void CleanExpiredState(int[] index)
        {
            List<int> list = new List<int>();
            for (int i = States.Count - 1; i >= 0; i--)
            {
                if (States[i].Rounds == 0 && States[i].Times == 0 && States[i].StateType != StateType.PermanentState)
                {
                    list.Add(i);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                string text = string.Format("{0}失去了{1}效果", Name, States[list[i]].Name);
                Draw.BattleInfo(text, index);
                States.RemoveAt(list[i]);
                Draw.Clean_StateInfo(this);
                Draw.DrawState(this);
                Draw.CharacterValueInfo(this);
            }
        }
        /// <summary>
        /// 对与持续回合类的状态，持续回合减1
        /// </summary>
        public void StateRoundDec()
        {
            foreach (State s in States)
            {
                if (s.Rounds > 0)
                    s.Rounds--;
            }
        }
        /// <summary>
        /// 清空角色的所有状态栏
        /// </summary>
        public void ResetState()
        {
            States.Clear();
        }
        /// <summary>
        /// 获取伤害加成,对于攻击者是BUFF,对于被攻击者是DEBUFF
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="stateType">BUFF和DEBUFF</param>
        /// <returns></returns>
        public int GetIncreaseDamage(int damage, StateType stateType)
        {
            int tempDamage = damage;
            foreach (State state in States)
            {
                if (state.StateType == stateType)
                {
                    if (state.Rounds > 0 || state.Times > 0)
                    {
                        tempDamage = Convert.ToInt32(damage * (1 + state.HitRate));
                    }
                }
            }
            return tempDamage;
        }
        /// <summary>
        /// 获取伤害削弱,对于攻击者来说是DEBUFF类型附带,对于被攻击者来说是BUFF类型
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="stateType">BUFF和DEBUFF</param>
        /// <returns></returns>
        public int GetWeakenedDamage(int damage, StateType stateType)
        {
            int tempDamage = damage;
            foreach (State state in States)
            {
                if (state.StateType == stateType)
                {
                    if (state.Rounds > 0 || state.Times > 0)
                    {
                        tempDamage = Convert.ToInt32(damage * (1 - state.BeHitRate));
                    }
                }
            }
            return tempDamage;
        }
        /// <summary>
        /// 状态生效函数
        /// </summary>
        /// <param name="index"></param>
        public void StateEffect(int[] index)
        {
            foreach (State state in States)
            {
                if (state.Rounds == 0 && state.Times == 0)
                {
                    continue;
                }
                string text = string.Format("{2}受到{0}的效果的影响({1})", state.Name, state.Rounds,Name);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Draw.BattleInfo(text, index);
                Console.ForegroundColor = ConsoleColor.Gray;
                if (state.Times > 0)
                {
                    state.Times--;
                }
                if (state.stateEffectType == StateEffectType.HP)
                {
                    if (state.StateType == StateType.Buff)
                    {
                        GetHeal(state.HPOverTime, index);
                        continue;
                    }
                    if (state.StateType == StateType.Debuff)
                    {
                        GetDamage(state.HPOverTime, index);
                        if(!IsAlive())
                        {
                             return;
                        }
                        continue;
                    }
                }
                if (state.stateEffectType == StateEffectType.STA)
                {
                    if (state.StateType == StateType.Buff)
                    {
                        GetSTAUP(state.StaminaOverTime, index);
                        continue;
                    }
                    if (state.StateType == StateType.Debuff)
                    {
                        GetSTALoss(state.StaminaOverTime, index);
                        continue;
                    }
                }
                if (state.stateEffectType == StateEffectType.StrengthChange)
                {
                    if (state.StateType == StateType.Buff)
                    {
                        text = "力量增强了";
                    }
                    else
                    {
                        text = "力量减弱了";
                    }

                }
                if (state.stateEffectType == StateEffectType.AgileChange)
                {
                    if (state.StateType == StateType.Buff)
                    {
                        text = "敏捷增强了";
                    }
                    else
                    {
                        text = "敏捷减弱了";
                    }
                }
                if(state.stateEffectType == StateEffectType.HitDamage)
                {
                    if(state.StateType == StateType.Buff)
                    {
                        text = "伤害增强了";
                    }
                    else
                    {
                        text = "伤害减弱了";
                    }
                }
                if(state.stateEffectType == StateEffectType.BeHitDamage)
                {
                    if(state.StateType == StateType.Buff)
                    {
                        text = "受到伤害增强了";
                    }
                    else
                    {
                        text = "受到伤害减弱了";
                    }
                }
                Draw.BattleInfo(text, index);
                Draw.DrawState(this);
                Draw.CharacterValueInfo(this);
            }
        }
        /// <summary>
        /// 角色受到伤害
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool GetDamage(int damage, int[] index)
        {

            int Damage = 0;
            if (HP >= damage)
            {
                Damage = damage;
                HP -= damage;
            }
            else
            {
                Damage = HP;
                HP = 0;
            }
            string text = string.Format("{0}受到了{1}点伤害", Name, Damage);
            Draw.BattleInfo(text, index);
            if (!IsAlive())
            {
                Draw.BattleInfo((Name + "重伤倒地"), index);

            }
            Draw.DrawDamageAnimation(this);
            Draw.CleanHP_STA(this);
            Draw.DrawHP_and_STA(this);
            return IsAlive();
        }
        /// <summary>
        /// 角色受到治疗
        /// </summary>
        /// <param name="recover"></param>
        /// <param name="index"></param>
        public void GetHeal(int heal, int[] index)
        {
            int Heal = 0;
            if (MaxHP() <= heal + HP)
            {
                Heal = MaxHP() - HP;
                HP = MaxHP();
            }
            else
            {
                Heal = heal;
                HP += Heal;
            }
            string text = string.Format("{0}恢复了{1}点生命值", Name, Heal);
            Console.ForegroundColor = ConsoleColor.Green;
            Draw.BattleInfo(text, index);
            Draw.DrawHealAnimation(this);
            Draw.CleanHP_STA(this);
            Draw.DrawHP_and_STA(this);
        }
        /// <summary>
        /// 角色回复体力
        /// </summary>
        /// <param name="recover"></param>
        /// <param name="index"></param>
        public void GetSTAUP(int stamina, int[] index)
        {
            int Sta = 0;
            if (MaxSTA() <= stamina + STA)
            {
                Sta = MaxSTA() - STA;
                STA = MaxSTA();
            }
            else
            {
                Sta = stamina;
                STA += Sta;
            }
            string text = string.Format("{0}恢复了{1}点体力值", Name, Sta);
            Console.ForegroundColor = ConsoleColor.Blue;
            Draw.BattleInfo(text, index);
            Draw.DrawHealAnimation(this);
            Draw.CleanHP_STA(this);
            Draw.DrawHP_and_STA(this);
        }
        /// <summary>
        /// 角色失去体力
        /// </summary>
        /// <param name="recover"></param>
        /// <param name="index"></param>
        public void GetSTALoss(int stamina, int[] index)
        {
            int sta = 0;
            if (STA >= stamina)
            {
                sta = stamina;
                STA -= sta;
            }
            else
            {
                sta = STA;
                STA = 0;
            }
            string text = string.Format("{0}损耗了{1}点体力值", Name, sta);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Draw.BattleInfo(text, index);
            Draw.DrawDamageAnimation(this);
            Draw.CleanHP_STA(this);
            Draw.DrawHP_and_STA(this);
        }

        public bool IsAlive()
        {
            if (HP == 0)
                return false;
            return true;
        }
    }
}
