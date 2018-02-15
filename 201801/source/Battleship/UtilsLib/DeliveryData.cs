using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsLib
{
    public enum MapGridStatus
    {
        Blank, Ship, BlankChosen, ShipChosen
    }
    [Serializable]
    public class DeliveryData
    {
        public int player_idx;//设定玩家索引，0为host，1为guest
        public bool isPlayerReady = false;
        public bool isReady = false;
        public bool isGameOver = false;
        public int winner_idx = -1;
        public MapGridStatus[,] mapData = new MapGridStatus[2, 100];
        public string cur_timestamp = null;
        /// <summary>
        /// 从数组中得到封装的敌方地图数据，对关键信息进行清洗
        /// </summary>
        /// <returns>过滤之后的数据</returns>
        public void CleanMapData(int map_idx)
        {
            for (int i = 0; i < 100; i++)
            {
                if ((mapData[map_idx, i] != MapGridStatus.BlankChosen) && (mapData[map_idx, i] != MapGridStatus.ShipChosen))
                {
                    mapData[map_idx, i] = MapGridStatus.Blank;
                }
            }
        }

        public void InitData()
        {
            isPlayerReady = false;
            isReady = false;
            isGameOver = false;
            winner_idx = -1;
            cur_timestamp = null;
        }
    }
}
