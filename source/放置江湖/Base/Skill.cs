using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 放置江湖.Base;

public enum SkillType
{
    NormalAttack,
    Damage,
    Heal,
    Buff,
    DeBuff,
    Invincible,
}

public enum SkillTarget
{
    enemy,
    oneself
}

//public enum SkillState
//{
//    dot,
//    Invincible
//}

namespace 放置江湖
{

    class Skill
    {
        /// <summary>
        /// 伤害值
        /// </summary>
        public int damage;
        /// <summary>
        /// 技能名
        /// </summary>
        public string name;
        /// <summary>
        /// 技能类型
        /// </summary>
        public SkillType skillType;
        /// <summary>
        /// 技能目标
        /// </summary>
        public SkillTarget skillTarget;
        /// <summary>
        /// 持续回合数
        /// </summary>
        public int time;
        /// <summary>
        /// 消耗Mp
        /// </summary>
        public int expendMp;

        public static Skill SkillCreateNormalAttack(string name,int damage)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.damage = damage;
            skill.expendMp = 0;
            skill.skillType = SkillType.NormalAttack;
            skill.skillTarget = SkillTarget.enemy;
            return skill;
        }

        public static Skill CreateDamageSkill(string name,int damage,int expendMp)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.damage = damage;
            skill.expendMp = expendMp;
            skill.skillType = SkillType.Damage;
            skill.skillTarget = SkillTarget.enemy;
            return skill;
        }

        public static Skill CreateHealSkill(string name,int damage,int expendMp)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.damage = damage;
            skill.expendMp = expendMp;
            skill.skillType = SkillType.Heal;
            skill.skillTarget = SkillTarget.oneself;
            return skill;
        }

        public static Skill CreateBuffSkill(string name,int damage,int expendMp,int time)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.damage = damage;
            skill.expendMp = expendMp;
            skill.skillType = SkillType.Buff;
            skill.skillTarget = SkillTarget.oneself;
            skill.time = time;
            return skill;
        }

        public static  Skill CreateDeBuffSkill(string name,int damage,int expendMp,int time)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.damage = damage;
            skill.expendMp = expendMp;
            skill.skillType = SkillType.DeBuff;
            skill.skillTarget = SkillTarget.enemy;
            skill.time = time;
            return skill;
        }

        public static Skill CreateInvincibleSkill(string name,int expendMp,int time)
        {
            Skill skill = new Skill();
            skill.name = name;
            skill.expendMp = expendMp;
            skill.skillType = SkillType.Invincible;
            skill.skillTarget = SkillTarget.oneself;
            skill.time = time;
            return skill;
        }

    }
}
