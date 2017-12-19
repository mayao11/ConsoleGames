using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyBattle
{
    public enum SkillType
    {
        /// <summary>
        /// 伤害技能
        /// </summary>
        Damage,
        /// <summary>
        /// 恢复技能
        /// </summary>
        Heal,
        /// <summary>
        /// 各种状态类技能，buff,debuff等
        /// </summary>
        State,
        /// <summary>
        /// 普通攻击
        /// </summary>
        NormalAttack
    }

    public enum TargetType
    {
        /// <summary>
        /// 己方单体
        /// </summary>
        PartySingle,
        /// <summary>
        /// 己方全体
        /// </summary>
        PartyMulti,
        /// <summary>
        /// 敌方单体
        /// </summary>
        EnemySingle,
        /// <summary>
        /// 敌方全体
        /// </summary>
        EnemyMulti,
        /// <summary>
        /// 自身
        /// </summary>
        Self,
    }

    public enum StateType
    {
        /// <summary>
        /// 持续伤害状态
        /// </summary>
        DamageOverTime,

        /// <summary>
        /// 持续恢复状态
        /// </summary>
        HealOverTime,
         
        /// <summary>
        /// 物理反射状态
        /// </summary>
        PhysicalReflect,

        /// <summary>
        /// 魔法反射状态
        /// </summary>
        MagicReflect,

        /// <summary>
        /// 复活状态
        /// </summary>
        Revive,

        /// <summary>
        /// 各种buff
        /// </summary>
        Buff,

        /// <summary>
        /// 各种debuff
        /// </summary>
        Debuff,

        /// <summary>
        /// 无敌状态
        /// </summary>
        Invincible,

        /// <summary>
        /// 沉默状态
        /// </summary>
        Silence,

        /// <summary>
        /// 物理伤害加深状态
        /// </summary>
        PhysicalDamageIncrease,

        /// <summary>
        /// 受到的物理伤害加深的状态
        /// </summary>
        PhysicalBeDamageIncrease,

        /// <summary>
        /// 魔法伤害增加
        /// </summary>
        MagicDamageIncrease,

        /// <summary>
        /// 受到的魔法伤害增加
        /// </summary>
        MagicBeDamageIncrease,

        /// <summary>
        /// 眩晕状态
        /// </summary>
        Dizzy,

        /// <summary>
        /// 禁止恢复的状态
        /// </summary>
        ForbidHeal,

        /// <summary>
        /// 嘲讽状态
        /// </summary>
        Taunt,
    }

    public enum CharacterType
    {
        /// <summary>
        /// 玩家
        /// </summary>
        Hero,
        /// <summary>
        /// 怪物
        /// </summary>
        Enemy,
    }

    /// <summary>
    /// 伤害类型
    /// </summary>
    public enum DamageType
    {
        /// <summary>
        /// 物理伤害，会丢失，但是能够暴击
        /// </summary>
        Physical,
        /// <summary>
        /// 魔法伤害，必定命中，没有暴击
        /// </summary>
        Magic
    }

    /// <summary>
    /// 怪物选择目标的准则
    /// </summary>
    public enum ActionMode
    {
        /// <summary>
        /// 攻击血量最高的
        /// </summary>
        MaxHP,
        /// <summary>
        /// 攻击血量最低的
        /// </summary>
        MinHP,
        /// <summary>
        /// 随机攻击
        /// </summary>
        Normal,
    }
}
