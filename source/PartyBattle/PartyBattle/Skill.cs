using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyBattle
{
    class Skill
    {
        internal delegate int SkillDamage(BaseCharacter a, BaseCharacter b);
        internal delegate void SkillEffect(BaseCharacter a, BaseCharacter b);
        public string name;
        /// <summary>
        /// 技能类型，包含攻击技能，恢复技能，DOT技能，HOT技能，状态技能
        /// </summary>
        public SkillType type;
        /// <summary>
        /// 技能的目标类型
        /// </summary>
        public TargetType targetType;
        /// <summary>
        /// 技能的伤害类型
        /// </summary>
        public DamageType damageType;
        /// <summary>
        /// 使用技能需要的HP
        /// </summary>
        public int hpCost = 0;
        /// <summary>
        /// 使用技能需要的MP
        /// </summary>
        public int mpCost = 0;
        /// <summary>
        /// 释放一次技能的攻击次数或恢复次数
        /// </summary>
        public int hitTimes = 1;
        /// <summary>
        /// 技能伤害的离散度
        /// </summary>
        public int dispersion = 0;
        /// <summary>
        /// 技能是否能对自己释放
        /// </summary>
        public bool self = false;
        /// <summary>
        /// 是否可以对死亡角色使用
        /// </summary>
        public bool canDeath = false;
        /// <summary>
        /// 技能是否包含即死效果
        /// </summary>
        public bool isDeathNow = false;
        /// <summary>
        /// 即死概率
        /// </summary>
        public int deathRatio = 0;
        /// <summary>
        /// 技能是否必定命中
        /// </summary>
        public bool noMiss = false;
        /// <summary>
        /// 技能是否必定暴击
        /// </summary>
        public bool mustCrit = false;
        /// <summary>
        /// 技能描述，包括消耗和效果
        /// </summary>
        public string description = "";
        /// <summary>
        /// 技能的效果计算公式，返回值将作为伤害量或者恢复量，该属性会被damage和heal的值覆盖，添加状态时也需要为这个属性赋值
        /// </summary>
        public SkillDamage skillDamage = null;
        /// <summary>
        /// 技能附加的状态效果
        /// </summary>
        public SkillEffect skillEffect = null;

        /// <summary>
        /// 判断角色是否能够释放技能
        /// </summary>
        /// <param name="attacker"></param>
        /// <returns></returns>
        public bool CanUseSkill(BaseCharacter attacker)
        {
            //判断角色是否有足够的资源释放技能
            if (attacker.Hp <= hpCost || attacker.Mp < mpCost)
            {
                MyDraw.DrawBattleMessage("消耗不足，无法使用技能！");
                return false;
            }

            //判断角色是否被禁用了技能
            if (attacker.IsSilence() && type != SkillType.NormalAttack)
            {
                MyDraw.DrawBattleMessage(attacker.name + "被沉默了，无法使用技能！");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 群体技能的释放逻辑，因为有单次技能伤害加成状态的原因，需要在技能结束后多做一次判断
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="att_index"></param>
        /// <param name="targets"></param>
        public void UseSkillMulti(BaseCharacter attacker, int att_index, List<BaseCharacter> targets)
        {
            //消耗位置放在这里放置全体技能多次扣除技能消耗
            attacker.Hp -= hpCost;
            attacker.Mp -= mpCost;
            for (int i = 0; i < targets.Count; i++)
            {
                if (attacker.Hp == 0)
                    return;
                if (targets[i].Hp == 0 && canDeath == false)
                    continue;
                UseSkill(attacker, att_index, targets[i], i);
            }
            if (type == SkillType.Damage)
            {
                if (skillDamage != null)
                {
                    if (damageType == DamageType.Magic)
                    {
                        attacker.IncreaseStateDec(StateType.MagicDamageIncrease);
                        foreach (var t in targets)
                        {
                            t.IncreaseStateDec(StateType.MagicBeDamageIncrease);
                        }
                    }
                    else if (damageType == DamageType.Physical)
                    {
                        attacker.IncreaseStateDec(StateType.PhysicalDamageIncrease);
                        foreach (var t in targets)
                        {
                            t.IncreaseStateDec(StateType.PhysicalBeDamageIncrease);
                        }
                    }
                }
            }
            MyDraw.DrawState(attacker, att_index);
            for (int i = 0; i < targets.Count; i++)
            {
                MyDraw.DrawState(targets[i], i);
            }
        }

        /// <summary>
        /// 单体技能的释放逻辑，因为有单次技能伤害加成状态的原因，需要在技能结束后多做一次判断
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="att_index"></param>
        /// <param name="target"></param>
        /// <param name="tar_index"></param>
        public void UseSkillSingle(BaseCharacter attacker, int att_index, BaseCharacter target, int tar_index)
        {
            attacker.Hp -= hpCost;
            attacker.Mp -= mpCost;
            UseSkill(attacker, att_index, target, tar_index);
            if (type == SkillType.Damage)
            {
                if (skillDamage != null)
                {
                    if (damageType == DamageType.Magic)
                    {
                        attacker.IncreaseStateDec(StateType.MagicDamageIncrease);
                        target.IncreaseStateDec(StateType.MagicBeDamageIncrease);
                    }
                    else if (damageType == DamageType.Physical)
                    {
                        attacker.IncreaseStateDec(StateType.PhysicalDamageIncrease);
                        target.IncreaseStateDec(StateType.PhysicalBeDamageIncrease);
                    }
                }
            }
            MyDraw.DrawState(attacker, att_index);
            MyDraw.DrawState(target, tar_index);
        }

        /// <summary>
        /// 释放一个技能
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="att_index"></param>
        /// <param name="target"></param>
        /// <param name="tar_index"></param>
        private void UseSkill(BaseCharacter attacker, int att_index, BaseCharacter target, int tar_index)
        {
            //过程中生存游戏提示信息的文本
            string text = "";
            int tempdamage = 0;
            MyDraw.DrawCharacterInfo(attacker, att_index);
            text = string.Format("{0}对{1}施放了{2}", attacker.name, target.name, name);
            MyDraw.DrawBattleMessageDelay(text);
            if (targetType == TargetType.EnemyMulti || targetType == TargetType.EnemySingle)
            {
                if(attacker.type == CharacterType.Hero)
                    MyDraw.DrawAttackAnimation(attacker, att_index, target, tar_index);
                else
                    MyDraw.DrawAttackAnimationEnemy(attacker, att_index, target, tar_index);
            }
            //对即时伤害的技能的处理
            if (type == SkillType.Damage || type == SkillType.NormalAttack)
            {
                //获取技能的基础伤害值
                if (skillDamage != null)
                {
                    tempdamage = skillDamage.Invoke(attacker, target);
                    //判断目标是否无敌
                    if (target.IsInvincible())
                    {
                        return;
                    }
                    //判断目标是否具有技能伤害类型对应的反射状态
                    if (damageType == DamageType.Physical)
                    {
                        if (target.IsPhysicalReflect())
                        {
                            if (target.type == CharacterType.Hero)
                                MyDraw.DrawAttackAnimation(target, tar_index, attacker, att_index);
                            else
                                MyDraw.DrawAttackAnimationEnemy(target, tar_index, attacker, att_index);
                            if (attacker.IsInvincible())
                            {
                                return;
                            }
                            else
                            {
                                //获取攻击者和目标身上的物理伤害加深BUFF并且计算他们的效果
                                tempdamage = attacker.GetIncreaseDamage(tempdamage, StateType.PhysicalDamageIncrease);
                                tempdamage = attacker.GetIncreaseDamage(tempdamage, StateType.PhysicalBeDamageIncrease);
                                attacker.GetDamage(tempdamage, att_index);
                                return;
                            }
                        }
                        if (!IsHit(attacker) && noMiss == false)
                        {
                            text = string.Format("{0}未能击中目标！", attacker.name);
                            MyDraw.DrawBattleMessageDelay(text);
                            return;
                        }
                        if (IsCrit(attacker) || mustCrit == true)
                        {
                            text = string.Format("{0}触发了会心一击！", attacker.name);
                            MyDraw.DrawBattleMessageDelay(text);
                            tempdamage = tempdamage * 15 / 10;
                        }
                        //获取攻击者和目标身上的物理伤害加深BUFF并且计算他们的效果
                        tempdamage = attacker.GetIncreaseDamage(tempdamage, StateType.PhysicalDamageIncrease);
                        tempdamage = target.GetIncreaseDamage(tempdamage, StateType.PhysicalBeDamageIncrease);
                    }
                    else if (damageType == DamageType.Magic)
                    {
                        if (target.IsMagicReflect())
                        {
                            if (target.type == CharacterType.Hero)
                                MyDraw.DrawAttackAnimation(target, tar_index, attacker, att_index);
                            else
                                MyDraw.DrawAttackAnimationEnemy(target, tar_index, attacker, att_index);
                            if (attacker.IsInvincible())
                            {
                                return;
                            }
                            else
                            {
                                //获取攻击者和目标身上的魔法伤害加深BUFF并且计算他们的效果
                                tempdamage = attacker.GetIncreaseDamage(tempdamage, StateType.MagicDamageIncrease);
                                tempdamage = attacker.GetIncreaseDamage(tempdamage, StateType.MagicBeDamageIncrease);
                                attacker.GetDamage(tempdamage, att_index);
                                return;
                            }
                        }
                        else
                        {
                            //获取攻击者和目标身上的魔法伤害加深BUFF并且计算他们的效果
                            tempdamage = attacker.GetIncreaseDamage(tempdamage, StateType.MagicDamageIncrease);
                            tempdamage = target.GetIncreaseDamage(tempdamage, StateType.MagicBeDamageIncrease);
                        }
                    }

                    //进行即时伤害并且生成对应的文字信息
                    for (int t = 0; t < hitTimes; t++)
                    {
                        int realdamage = Convert.ToInt32(tempdamage + tempdamage * Program.random.Next(-dispersion, dispersion) * 0.01);
                        //处理即死效果
                        if (isDeathNow)
                        {
                            if (Program.random.Next(0, 100) < deathRatio)
                            {
                                MyDraw.DrawBattleMessageDelay("触发了即死效果。");
                                realdamage = target.maxHp;
                            }
                        }
                        if (!target.GetDamage(realdamage, tar_index))
                        {
                            break;
                        }
                    }
                }
                //如果技能有特殊效果，执行特殊效果
                if (skillEffect != null)
                {
                    MyDraw.DrawEffectAnimation(target, tar_index);
                    skillEffect.Invoke(attacker, target);
                    MyDraw.DrawState(target, tar_index);
                }
                return;
            }

            //对即时回复类技能的处理
            if (type == SkillType.Heal)
            {
                if (skillDamage != null)
                {
                    tempdamage = skillDamage.Invoke(attacker, target);
                    if (!target.IsForbidHeal())
                    { 
                        target.GetRecover(tempdamage, tar_index);
                    }
                }
                if (skillEffect != null)
                {
                    MyDraw.DrawEffectAnimation(target, tar_index);
                    skillEffect.Invoke(attacker, target);
                    MyDraw.DrawState(target, tar_index);
                }
                return;
            }

            //对状态类技能的处理，主要是Buff,Debuff等
            if (type == SkillType.State)
            {
                if (skillEffect != null)
                {
                    MyDraw.DrawEffectAnimation(target, tar_index);
                    skillEffect.Invoke(attacker, target);
                    MyDraw.DrawState(target, tar_index);
                }
            }
            return;
        }

        /// <summary>
        /// 判断是否命中
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsHit(BaseCharacter b)
        {
            int temp = Program.random.Next(1, 101);
            if (temp > b.hit)
                return false;
            return true;
        }

        /// <summary>
        /// 判断是否暴击
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool IsCrit(BaseCharacter b)
        {
            int temp = Program.random.Next(1, 101);
            if (temp < b.crt)
                return true;
            return false;
        }
    }
}
