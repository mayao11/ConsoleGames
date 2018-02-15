using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsLib
{
    public static class RandomGenerator
    {
        static Random random = new Random();
        /// <summary>
        /// 封装的全局随机数获得器
        /// </summary>
        /// <param name="min">随机的下限</param>
        /// <param name="max">随机的上限，但是不包括自身</param>
        /// <returns>返回的结果</returns>
        public static int GetNumber(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
