using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyBattle
{
    class State
    {
        public delegate void StateEffect(BaseCharacter a);
        public string name;
        /// <summary>
        /// 生成这个状态的技能的名字
        /// </summary>
        public string skillName;
        /// <summary>
        /// 状态生效的次数
        /// </summary>
        public int times = 0;
        /// <summary>
        /// 状态生效的回合数
        /// </summary>
        public int counts = 0;
        /// <summary>
        /// DOT时候的伤害
        /// </summary>
        public int damage = 0;
        /// <summary>
        /// HOT时候的伤害
        /// </summary>
        public int hprecover = 0;
        /// <summary>
        /// 属性或伤害加深减少时候的比率
        /// </summary>
        public double ratio = 0;
        /// <summary>
        /// 当前状态提升的物理攻击力
        /// </summary>
        public int atkUp = 0;
        /// <summary>
        /// 当前状态提升的物理防御力
        /// </summary>
        public int defUp = 0;
        /// <summary>
        /// 当前状态提升的魔法攻击力
        /// </summary>
        public int matUp = 0;
        /// <summary>
        /// 当前状态提升的魔法防御力
        /// </summary>
        public int menUp = 0;
        /// <summary>
        /// 状态的类型
        /// </summary>
        public StateType type;
        /// <summary>
        /// 第一次施加这个状态时调用的方法
        /// </summary>
        public StateEffect AddState = null;
        /// <summary>
        /// 移除这个状态的时候调用的方法
        /// </summary>
        public StateEffect RemoveState = null;
    }
}
