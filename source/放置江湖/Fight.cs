/*
 * 
 * 战斗流程  调用FightEachOther方法传入战斗角色和怪物，循环进行战斗
 *                角色的玩家，是固定的  怪物是当前地图随机生成的怪物
 *                战斗采用回合制     暂时为 角色先攻，怪物后攻
 *                当玩家生命值变为0后，结束战斗循环，返回主界面
 *                当怪物生命值变为0后，，战斗场数+1，重置玩家属性，重新生成怪物，重新开始战斗循环
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using 放置江湖.Base;
using 放置江湖.Global;

namespace 放置江湖
{
    /// <summary>
    /// 战斗交互
    /// </summary>
    class Fight
    {
        Global.Global global = new Global.Global();

        static Random rd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        
        /// <summary>
        /// 战斗总场数
        /// </summary>
        static int index = 0;

        /// <summary>
        /// 战斗回合数
        /// </summary>
        static int round = 0;

        /// <summary>
        /// 是否战斗结束
        /// </summary>
        static bool isEnd = false;


         Character static_other_player = new Character();
         Character cur_player = new Character();
         Character other_player = new Character();


        public void INI(Character player,Character monster)
        {
            cur_player = player;
            other_player = monster;
            static_other_player = monster;
            index = 1;
            round = 1;
        }


        public void FightTimer(object sender, ElapsedEventArgs e)
        {
            StageBattle();
        }

        /// <summary>
        /// 战斗循环
        /// </summary>
        /// <param name="cur_player"></param>
        /// <param name="other_player"></param>
         void StageBattle()
        {
           

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("--------已经持续挂机{0}场--------------", index);
            if (cur_player.curHp > 0 && other_player.curHp > 0)
            {
                Console.WriteLine("--------第{0}回合开始--------------", round);
                //显示双方信息
                ShowInfo(cur_player);
                ShowInfo(other_player);

                FightEachOther(cur_player, other_player);

                if (other_player.curHp > 0)
                {
                    FightEachOther(other_player, cur_player);
                    Console.WriteLine();
                   
                }
                StateTakeEffect(cur_player);
                StateTakeEffect(other_player);
                round += 1;
            }
            else if (cur_player.curHp <= 0)
            {
                //怪物获得胜利的话，返回主界面  
                Console.WriteLine("--------怪物{0}获得胜利--------------", other_player.name);
                global.timer.Elapsed -= FightTimer;

                Console.WriteLine("--------计时器暂停画主界面--------------", cur_player.name);
            }
            else if (other_player.curHp <= 0)
            {
                //玩家获得胜利的话，
                Console.WriteLine("--------玩家{0}获得胜利--------------", cur_player.name);
                isEnd = true;
            }

          

            if (isEnd)
            {
                cur_player.CostExp(other_player.proExp);
                //重置玩家属性
                cur_player.ResetAttribute();
                other_player.ResetAttribute();
                index++;
                round = 1;
                isEnd = false;
                
            }
            
        }

        /// <summary>
        /// 显示战斗人员信息
        /// </summary>
        /// <param name="cha"></param>
        static void ShowInfo(Character cha)
        {

            if (cha.playertype == PlayerType.player)
            {

                Console.WriteLine("姓名: {0}：", cha.name);
                Console.Write("    Level:{0}", cha.curLevel);
                Console.Write("    HP:{0} /{1} ", cha.curHp, cha.maxHp);
                Console.Write("    MP:{0}/{1}", cha.curMp, cha.maxMp);
                Console.Write("    Exp:{0}/{1}", cha.curExp, cha.maxExp);
                Console.Write("    Atk:{0}", cha.atk);
                Console.Write("    Def:{0}", cha.def);
                Console.Write("\n");
            }
            else
            {
                Console.WriteLine("姓名: {0}：", cha.name);
                Console.Write("    HP:{0}  ", cha.curHp);
                Console.Write("    Atk:{0}  ", cha.atk);
                Console.Write("    Def:{0}  ", cha.def);
                Console.Write("\n");
            }

            Console.WriteLine("持续状态:");
            for (int i = 0; i < cha.states.Count; ++i)
            {
                Console.Write("{0}（剩余回合:{1}）    ", cha.states[i].name, cha.states[i].time);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 战斗循环
        /// </summary>
        /// <param name="cur_player">攻击者</param>
        /// <param name="other_player">被击者</param>
        void FightEachOther(Character cur_player, Character other_player)
        {
          

            if (cur_player.playertype == PlayerType.player)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            //随机选取技能并使用
            int skillIndex = rd.Next(cur_player.skills.Count);
            Skill temp = cur_player.skills[skillIndex];
            UseSkill(temp, cur_player, other_player);
         
        }


        /// <summary>
        /// 使用技能
        /// </summary>
        /// <param name="skill">要使用的技能</param>
        /// <param name="cur_cha">施展者</param>
        /// <param name="target_char">目标</param>
        void UseSkill(Skill skill, Character cur_cha, Character target_cha)
        {
            Console.WriteLine("--------------------------------------------------------");
            foreach (var state in target_cha.states)
            {
                if (state.skillType==SkillType.Invincible)
                {
                    Console.WriteLine("{0}身上拥有无敌状态，停止此次攻击", target_cha.name );
                    Console.WriteLine("--------------------------------------------------------");
                    return;
                }
            }
            SkillType st = skill.skillType;
            if (skill.expendMp > cur_cha.curMp)
            {
                Console.WriteLine("{0}的当前魔法值不足以释放{1}，默认施放一次普通攻击", cur_cha.name, skill.name);
                Console.WriteLine("--------------------------------------------------------");
                skill.skillType = SkillType.NormalAttack;
            }
            cur_cha.CostMp(skill.expendMp);
            int damage = 0;
            switch (skill.skillType)
            {

                case SkillType.NormalAttack:
                    damage = cur_cha.atk - target_cha.def;
                    if (damage <= 0)
                    {
                        damage = 0;
                        Console.WriteLine("{0}对{1}使用了{2}，但是被{1}防御了，{1}受到了0点伤害！！", cur_cha.name, target_cha.name, skill.name);
                    }
                    else
                    {
                        target_cha.CostHp(damage);
                        Console.WriteLine("{0}对{1}使用了{2}，{1}受到了{3}点伤害！！", cur_cha.name, target_cha.name, skill.name, damage);
                    }
                    break;
                case SkillType.Damage:
                    target_cha.CostHp(skill.damage);
                    Console.WriteLine("{0}对{1}使用了{2}，{1}受到了{3}点伤害！！", cur_cha.name, target_cha.name, skill.name, skill.damage);
                    break;
                case SkillType.Heal:
                    cur_cha.HealHp(skill.damage);
                    Console.WriteLine("{0}对自己使用了{1}，{0}获得了{2}点的生命回复！！", cur_cha.name, skill.name, skill.damage);
                    break;
                case SkillType.Buff:
                    State temp = new State(skill.skillType,skill.name,skill.damage,skill.time);
                    cur_player.AddState(temp);
                    Console.WriteLine("{0}对{0}使用了增益持续技能{1}，{0}获得{2}回合的增益效果！！", cur_cha.name, skill.name, skill.time);
                    break;
                case SkillType.DeBuff:
                    State temp1 = new State(skill.skillType, skill.name, skill.damage, skill.time);
                    other_player.AddState(temp1);
                    Console.WriteLine("{0}对{1}使用了减益持续技能{2}，{1}获得{3}回合的减益效果！！", cur_cha.name, other_player.name, skill.name,skill.time);
                    break;
                case SkillType.Invincible:
                    State temp2 = new State(skill.skillType, skill.name, 0, skill.time);
                    cur_player.AddState(temp2);
                    Console.WriteLine("{0}使用了无敌技能{1},或者{2}回合无敌效果！！", cur_cha.name, skill.name, skill.time);
                    break;
                default:
                    break;  
            }
            skill.skillType = st;
        }


        /// <summary>
        /// 状态生效和移除
        /// </summary>
        /// <returns></returns>
        public void StateTakeEffect(Character cur_player)
        {
            if (cur_player.states.Count != 0)
            {
                //将回合为0的状态移除数组
                for (int i =0; i < cur_player.states.Count; i++)
                {
                    if (cur_player.states[i].time == 0)
                    {
                        cur_player.states.RemoveAt(i);
                        i--;
                    }
                }

                foreach (State state in cur_player.states)
                {
                    switch (state.skillType)
                    {
                        case SkillType.Buff:
                            cur_player.HealHp(state.damage);
                            state.time -= 1;
                            break;
                        case SkillType.DeBuff:
                            cur_player.CostHp(state.damage);
                            state.time -= 1;
                            break;
                        case SkillType.Invincible:
                            state.time -= 1;
                            break;
                    }
                }
            }
         
            
        }

    }
}

