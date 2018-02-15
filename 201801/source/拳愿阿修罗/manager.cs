using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 拳愿阿修罗
{
    class Manager
    {
        /// <summary>
        /// 所有角色的列表
        /// </summary>
        public List<Character> cha_list ;
        /// <summary>
        ///  选项的坐标位置
        /// </summary>
        public List<int> text_pos;
        /// <summary>
        /// 战斗信息的位置计数器
        /// </summary>
        public int []BattleInfo_Indext = {0};
        /// <summary>
        /// 玩家角色
        /// </summary>
        public Character Player;
        /// <summary>
        /// 电脑角色
        /// </summary>
        public Character Computer;
        /// <summary>
        /// 战斗回合数
        /// </summary>
        public int round=1 ;
        public Manager()
        {
            cha_list = new List<Character>();
            text_pos = new List<int>();
            round = 1;
        }
        /// <summary>
        ///  战斗
        /// </summary>
        public void RoundBattle()
        {
            while(true)
            {
                Draw.DrawRound(round);
                OneRoundStateEffect();
                Action();
                if(RoundOver())
                {
                    return;
                }
                StateCountDec();
                CleanExpiredState();
                round++;
            }
        }
        public void DrawBattle()
        {
            Draw.DrawFrame(Player);
            Draw.DrawFrame(Computer);
            Draw.CharacterValueInfo(Player);
            Draw.CharacterValueInfo(Computer);
            Draw.DrawHP_and_STA(Player);
            Draw.DrawHP_and_STA(Computer);
        }
        /// <summary>
        /// 每回合开始刷新生效的状态
        /// </summary>
        /// <param name="player"></param>
        /// <param name="computer"></param>
        private void OneRoundStateEffect()
        {
            Player.StateEffect(BattleInfo_Indext);
            Computer.StateEffect(BattleInfo_Indext);
        }
        /// <summary>
        /// 状态持续回合减一
        /// </summary>
        private void StateCountDec()
        {
            Player.StateRoundDec();
            Computer.StateRoundDec();
        }
        /// <summary>
        ///  清除所有回合或者次数为0的状态
        /// </summary>
        private void CleanExpiredState()
        {
            Player.CleanExpiredState(BattleInfo_Indext);
            Computer.CleanExpiredState(BattleInfo_Indext);
        }
        /// <summary>
        /// 分出胜负
        /// </summary>
        /// <returns></returns>
        private bool RoundOver()
        {
            if(!Player.IsAlive())
            {
                string text = string.Format("胜负已分,{0}晋级下一轮", Computer.Name);
                Draw.BattleInfo(text, BattleInfo_Indext);
                return true;
            }
            if(!Computer.IsAlive())
            {
                

                string text = string.Format("胜负已分,{0}晋级下一轮", Player.Name);
                Draw.BattleInfo(text, BattleInfo_Indext);
                return true;
            }
            return false;
        }
        /// <summary>
        ///  行动部分,敏捷高的先手
        /// </summary>
        private void Action()
        {
            if (Player.Agile >= Computer.Agile)
            {
                Player.RandomSkill(Player.Skills).UseSkill(Player, Computer, BattleInfo_Indext);
                if(!Computer.IsAlive())
                {
                    return;
                }
                Computer.RandomSkill(Computer.Skills).UseSkill(Computer, Player, BattleInfo_Indext);
            }
            else
            {
                Computer.RandomSkill(Computer.Skills).UseSkill(Computer, Player, BattleInfo_Indext);
                if(!Player.IsAlive())
                {
                    return;
                }
                Player.RandomSkill(Player.Skills).UseSkill(Player, Computer, BattleInfo_Indext);
            }
        }
    }
}
