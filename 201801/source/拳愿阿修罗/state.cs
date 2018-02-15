using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 拳愿阿修罗
{
    class State
    {
        /// <summary>
        /// 技能附加类特殊状态的触发条件与几率的委托
        /// </summary>
        /// <param name="Attacter"></param>
        /// <param name="BeHiter"></param>
        /// <returns></returns>
        public delegate bool StateEffectTrigger(Character Attacter, Character BeHiter);
        /// <summary>
        ///  生效状态的委托
        /// </summary>
        /// <param name="cha"></param>
        public delegate void StateEffect(Character cha);
        /// <summary>
        /// 状态名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 触发状态的技能名字
        /// </summary>
        public string SkillName;
        /// <summary>
        /// 状态类型 BUFF/DEBUFF
        /// </summary>
        public StateType StateType;
        /// <summary>
        /// 状态效果类型
        /// </summary>
        public StateEffectType stateEffectType;
        /// <summary>
        /// 状态的生效次数
        /// </summary>
        public int Times = 0;
        /// <summary>
        /// 状态的回合数
        /// </summary>
        public int Rounds = 0;

       
        /// <summary>
        /// 受到状态时每回合的伤害/治疗值
        /// </summary>
        public int HPOverTime=0;
        /// <summary>
        /// 受到状态时每回合恢复/损失的体力值
        /// </summary>
        public int StaminaOverTime = 0;
        /// <summary>
        /// 受到状态影响时提升/降低的力量值
        /// </summary>
        public int StrengthChange = 0;
        /// <summary>
        /// 受到状态影响时提升/降低的敏捷值
        /// </summary>
        public int AgileChange = 0;
        /// <summary>
        /// 受到伤害加深或削弱的比率
        /// </summary>
        public double BeHitRate = 0;
        /// <summary>
        /// 攻击时伤害加深或者削弱的比率
        /// </summary>
        public double HitRate = 0;
        /// <summary>
        ///  状态的触发几率
        /// </summary>
        public int TriggerRate = 100;
   
        /// <summary>
        /// 默认的状态是否触发的函数
        /// </summary>
        /// <returns></returns>
        public bool IsTrigger()
        {
            if(Utils.random.Next(1,101)<=TriggerRate)
            {
                return true;
            }
            return false;
        }
       
        /// <summary>
        /// 添加第一次生效的状态时调用的方法
        /// </summary>
        public StateEffect AddState = null;
        /// <summary>
        /// 移除状态时调用的方法
        /// </summary>
        public StateEffect RemoveState = null;
        /// <summary>
        /// 技能附加特殊状态的触发条件与几率
        /// </summary>
        public StateEffectTrigger EffectTrigger = null;
    }
}
