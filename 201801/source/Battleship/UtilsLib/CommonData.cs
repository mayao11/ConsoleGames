using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilsLib
{
    public class CommonData
    {
        //public int ready_idx = -1;
        public MapGridStatus[,] mapData = new MapGridStatus[2, 100];
        public List<string> list_timestamp = new List<string>();
        public bool[] list_ready = new bool[2] { true, false };
        public int[] list_leftShip = new int[2] { 20, 20 };
        //船的词典，用来保存每条船的每个格子的目标，船名可以被解析，例如"3_2"代表3格的船第二号
        public Dictionary<string, int[]>[] dict_ship = new Dictionary<string, int[]>[2];
        //船的反向索引，用来保存对应格子上的船名
        public Dictionary<int, string>[] dict_ship_reverse = new Dictionary<int, string>[2];
        public void InitData()
        {
            list_timestamp.Clear();
            dict_ship[0] = new Dictionary<string, int[]>();
            dict_ship[1] = new Dictionary<string, int[]>();
            dict_ship_reverse[0] = new Dictionary<int, string>();
            dict_ship_reverse[1] = new Dictionary<int, string>();
            list_ready[0] = true;
            list_ready[1] = false;
            list_leftShip[0] = 20;
            list_leftShip[1] = 20;
        }
    }
}
