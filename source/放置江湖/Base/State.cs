using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 放置江湖.Base
{
    /// <summary>
    /// 角色身上持续状态类
    /// </summary>
    class State
    {
        /// <summary>
        /// 技能类型
        /// </summary>
        public SkillType skillType;
        /// <summary>
        /// dot名
        /// </summary>
        public string name;
        /// <summary>
        /// 伤害
        /// </summary>
        public int damage;
        /// <summary>
        /// 持续时间
        /// </summary>
        public int time;

        public  State(SkillType _type,string _name,int _damage,int _time)
        {
            skillType = _type;
            name = _name;
            switch (skillType)
            {
                case SkillType.Buff:
                case SkillType.DeBuff:
                    damage = _damage;
                    time = _time;
                    break;
                case SkillType.Invincible:
                    time = _time;
                    break;
                default:
                    break;
            }
        }
    }
}
