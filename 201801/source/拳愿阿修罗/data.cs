using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 拳愿阿修罗
{
    class Data
    {
        public List<Character> cha_list = new List<Character>();
        /// <summary>
        /// 基础攻击技能,肉搏
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Skill CreateSkill_melee(string name)
        {
            Skill skill = new Skill()
            {
                Name = name,
                CostHP = 0,
                CostSTA = 0,
                HitTimes = 1,
                DamageRate=0.7,
                Type=SkillType.Active,
            };
            return skill;      
        }
        /// <summary>
        /// 伤害技能,可多次攻击,可附加状态
        /// </summary>
        /// <returns></returns>
        public static Skill CreateDamageSkill(string name,int costhp,int coststa,int hittimes,double damagerate)
        {
            Skill skill = new Skill()
            {
                Name=name,
                CostHP=costhp,
                CostSTA=coststa,
                HitTimes=hittimes,
                DamageRate=damagerate,
                Type=SkillType.Active,
            };
            return skill;
        }
        /// <summary>
        /// 状态技能
        /// </summary>
        /// <returns></returns>
        public static Skill CreateStateSkill(string name, int costhp, int coststa)
        {
            Skill skill = new Skill()
            {
                Name = name,
                CostHP = costhp,
                CostSTA = coststa,
                Type = SkillType.Active,
            };
            return skill;
        }
        /// <summary>
        /// HP相关的状态,回血或DOT
        /// </summary>
        /// <returns></returns>
        public static State CreatehpState(string name,StateType type,int triggerrate,int times,int rounds,int hp)
        {
            State state = new State()
            {
                Name = name,
                StateType = type,
                stateEffectType = StateEffectType.HP,
                TriggerRate=triggerrate,
                Times=times,
                Rounds=rounds,
                HPOverTime=hp,
            };
            return state;
        }
        /// <summary>
        /// 体力相关的状态,回复或损失体力
        /// </summary>
        /// <returns></returns>
        public static State CreateSTAState(string name, StateType type,  int triggerrate, int times, int rounds,int sta)
        {

            State state = new State()
            {
                Name = name,
                StateType = type,
                stateEffectType = StateEffectType.STA,
                TriggerRate = triggerrate,
                Times = times,
                Rounds = rounds,
                StaminaOverTime = sta,
            };
            return state;
        }
        /// <summary>
        /// 力量状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="effectType"></param>
        /// <param name="triggerrate"></param>
        /// <param name="rounds"></param>
        /// <param name="strength"></param>
        /// <returns></returns>
        public static State CreateStrengthState(string name, StateType type, int triggerrate, int rounds, int strength)
        {
            State state = new State()
            {
                Name = name,
                StateType = type,
                stateEffectType = StateEffectType.StrengthChange,
                TriggerRate = triggerrate,
                Rounds = rounds,
                StrengthChange = strength,
            };
            return state;
        }
        /// <summary>
        /// 敏捷状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="effectType"></param>
        /// <param name="triggerrate"></param>
        /// <param name="rounds"></param>
        /// <param name="agile"></param>
        /// <returns></returns>
        public static State CreateAgileState(string name, StateType type,  int rounds, int agile)
        {
            State state = new State()
            {
                Name = name,
                StateType = type,
                stateEffectType = StateEffectType.AgileChange,
                Rounds = rounds,
                AgileChange = agile,
            };
            return state;
        }
        /// <summary>
        /// 攻击增强/削弱状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="effectType"></param>
        /// <param name="triggerrate"></param>
        /// <param name="rounds"></param>
        /// <param name="hitrate"></param>
        /// <returns></returns>
        public static State CreateHitState(string name, StateType type,  int triggerrate, int rounds, int hitrate)
        {
            State state = new State()
            {
                Name = name,
                StateType = type,
                stateEffectType =StateEffectType.HitDamage,
                TriggerRate = triggerrate,
                Rounds = rounds,
                HitRate = hitrate,
            };
            return state;

        }
        /// <summary>
        /// 被攻击的削弱/增强状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="effectType"></param>
        /// <param name="triggerrate"></param>
        /// <param name="rounds"></param>
        /// <param name="behitrate"></param>
        /// <returns></returns>
        public static State CreateBeHitState(string name, StateType type, int triggerrate, int rounds, int behitrate)
        {
            State state = new State()
            {
                Name = name,
                StateType = type,
                stateEffectType = StateEffectType.BeHitDamage,
                TriggerRate = triggerrate,
                Rounds = rounds,
                BeHitRate = behitrate,
            };
            return state;
        }
        public static Character CreateCharacter1()
        {
            Character character = new Character("十鬼蛇王马", 8, 9, 10);
            character.Info = "'阿修罗'十鬼蛇王马,十鬼蛇二虎的徒弟,二虎流的继承者,因不明原因失忆";
            Skill attack = CreateSkill_melee("二虎流.金刚型.瞬铁爆");
            character.Skills.Add(attack);

            Skill buff1 = CreateStateSkill("二虎流.火天型.火走",0,50);
            buff1.Info = "犹如火焰般飘忽不定,用于迷惑敌人的步法.5回合内提升3点敏捷(50)";
            buff1.SkillState= CreateAgileState("火走", StateType.Buff, 5, 3);
            character.Skills.Add(buff1);

            Skill damage1 = CreateDamageSkill("二虎流.水天型.水燕", 0, 75, 3, 0.6);
            damage1.Info = "对全身肌肉精准控制打出的连打.连续造成3次伤害,必定命中";
            damage1.MustHit = true;
            character.Skills.Add(damage1);

            return character;
        }
        public static Character CreateCharacter2()
        {
            Character character = new Character("桐生刹那",7,7,10);
            character.Info = "十鬼蛇王马宿命中的对手.以前参加“死亡格斗”,后为了王马转入拳愿比赛";
            Skill attack = CreateSkill_melee("狐影流.罗刹掌");
            character.Skills.Add(attack);

            Skill buff1 = CreateStateSkill("狐影流.瞬", 0, 70);
            buff1.Info = "利用眨眼的瞬间移动到对手死角的步法.2回合内提升10点敏捷";
            buff1.SkillState= CreateAgileState("瞬", StateType.Buff, 2, 10);
            character.Skills.Add(buff1);

            Skill damage1 = CreateDamageSkill("真.罗刹掌", 0, 80, 1, 1.4);
            damage1.Info = "在罗刹掌原有的基础上,将威力集中于指尖,大幅提升威力.50%几率造成流血效果";
            damage1.SkillState= CreatehpState("出血", StateType.Debuff, 50, 1, 3, 100);
            character.Skills.Add(damage1);

            return character;
        }
    }
}
