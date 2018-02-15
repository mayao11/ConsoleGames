using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 拳愿阿修罗
{
    class Skill
    {

        ///// <summary>
        ///// 技能附加状态的委托
        ///// </summary>
        ///// <param name="attacter">攻击者</param>
        ///// <param name="BeHiter">被攻击者</param>
        //internal delegate State SkillState();
        /// <summary>
        /// 技能名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 技能的信息,包含简略介绍,消耗HP/STA,攻击次数等等
        /// </summary>
        public string Info="略";
        /// <summary>
        /// 技能类型
        /// </summary>
        public SkillType Type;
        /// <summary>
        /// 技能消耗的生命值,卖血技能
        /// </summary>
        public int CostHP=0;
        /// <summary>
        /// 技能消耗的体力值;
        /// </summary>
        public int CostSTA=0;
        /// <summary>
        ///  技能的攻击次数;
        /// </summary>
        public int HitTimes=1;
        /// <summary>
        /// 技能的伤害系数
        /// </summary>
        public double DamageRate=1.0;
        /// <summary>
        /// 技能是否包含即死效果
        /// </summary>
        public bool DeathState=false;
        /// <summary>
        /// 即死概率
        /// </summary>
        public int DeathRate = 0;
        /// <summary>
        /// 必中效果
        /// </summary>
        public bool MustHit = false;
        /// <summary>
        /// 必定暴击效果
        /// </summary>
        public bool MustCrit = false;

        public State SkillState=null;
      
        public bool SkillStateEffect()
        {
            if(SkillState.IsTrigger())
            {

                return true;
            }
            return false;
        }
        ///// <summary>
        ///// 技能附加状态
        ///// </summary>
        //public SkillState skillstate = null;        
        /// <summary>
        /// 技能的伤害值计算,返回一个计算完毕后的伤害值
        /// </summary>
        public  int SkillDamage(Character attacter,Character behiter)
        {
            int damage = (int)(attacter.Damage() * DamageRate - behiter.DamageModifier());
                
            return damage > 0 ? damage : 1;
        }
        /// <summary>
        ///  判断角色是否能使用技能
        /// </summary>
        /// <param name="cha">使用者</param>
        /// <returns></returns>
        public bool CanUseSkill(Character cha)
        {
            if(cha.HP<CostHP||cha.STA<CostSTA)
            {
                return false;
            }
            return true;
        }
   

        public void UseSkill(Character attacker,Character behiter,int[] index)
        {
            if (Type==SkillType.Active)
            {
                string text = string.Format("{0}使用了{1}",attacker.Name,Name);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Draw.BattleInfo(text, index);
                Console.ForegroundColor = ConsoleColor.Gray;
                attacker.HP -= CostHP;
                attacker.STA -= CostSTA; 
                for(int i=0;i<HitTimes;i++)
                {
                    Draw.DrawAttackAnimation(attacker);
                    int tempdamage;
                    if(behiter.IsHit()&&MustHit==false)
                    {
                         text = string.Format("{0}躲闪开了",behiter.Name);
                        Draw.BattleInfo(text, index);
                        Draw.DrawDogeAnimation(behiter);
                        continue;
                    }
                    tempdamage = SkillDamage(attacker, behiter);
                    if(SkillState!=null)
                    {
                        if (SkillStateEffect())
                        {
                            if (SkillState.StateType == StateType.Buff)
                            {                           
                                attacker.AddState(SkillState, index);
                            }
                            else
                            {
                                behiter.AddState(SkillState, index);
                            }

                        }
                    }   
                    tempdamage = attacker.GetIncreaseDamage(tempdamage, StateType.Buff);
                    tempdamage = behiter.GetIncreaseDamage(tempdamage, StateType.Debuff);
                    tempdamage = attacker.GetWeakenedDamage(tempdamage, StateType.Debuff);
                    tempdamage = behiter.GetWeakenedDamage(tempdamage, StateType.Buff);
                    if(attacker.IsCrit()||MustCrit==true)
                    {
                         text = string.Format("{0}击中了要害",attacker.Name);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Draw.BattleInfo(text, index);
                        tempdamage = (int)(tempdamage * attacker.CriticalRatio());
                        
                    }
                    behiter.GetDamage(tempdamage, index);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            if(Type==SkillType.state)
            {
                string text = string.Format("{0}使用了{1}", attacker.Name, Name);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Draw.BattleInfo(text, index);
                Console.ForegroundColor = ConsoleColor.Gray;
                attacker.HP -= CostHP;
                attacker.STA -= CostSTA;

            }
        }
                               
        

      

    }
}
