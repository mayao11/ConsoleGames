using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;


namespace 放置江湖.Global
{
    public class Global
    {
        public Timer timer = new Timer();

        public Global()
        {

            timer.Enabled = false;
            timer.Interval = 1000;
        }


        /// <summary>
        /// 最大等级
        /// </summary>
        public readonly int maxLevel = 5;

        /// <summary>
        /// 每级升级对应经验
        /// </summary>
        public readonly int[] maxExp = new int[5] { 100,1000,10000,100000,100000};

        /// <summary>
        /// 怪物数据  名字 血量 攻击 防御 提供经验值
        /// </summary>
        public readonly string[] monsterAttribute = 
            { "史莱姆,100,50,10,20",
              "蛤蟆镜,200,100,20,40"
        };
    }
}
