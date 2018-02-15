using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using UtilsLib;
namespace Battleship
{
    class Program
    {
        static HttpListener httpListener = null;
        //共用数据的类
        static CommonData commonData = new CommonData();
        static double last_response = 0.0f;
        //用于存储客户端链接上的时间戳，以此来区分客户端
        //static List<string> list_timestamp = new List<string>();
        static void Main(string[] args)
        {
            //创建服务端监听
            httpListener = new HttpListener();
            //指定身份验证方式
            httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            //指定监听ip和接口
            httpListener.Prefixes.Add("http://+:8080/");
            //开始监听
            httpListener.Start();
            commonData.InitData();
            commonData.mapData = GenerateMaps();
            Console.WriteLine("maps are ready");
            //Showmaps();
            Thread thread = new Thread(new ThreadStart(Handler));
            thread.Start();
        }
        //函数中的传送的数据有问题，目前仅为测试
        static void Handler()
        {
            DeliveryData deliveryData = new DeliveryData();
            while (true)
            {
                //等待获得客户的请求
                HttpListenerContext context = httpListener.GetContext();
                if (IsOverTime(5000))
                {
                    commonData.InitData();
                    commonData.mapData = GenerateMaps();
                    GenerateReverseIndex();
                    deliveryData.InitData();
                    Console.WriteLine("服务器已经重置");
                }

                #region 状态机，本例中，通过URL传过来的数据只需要时间戳和打击的坐标轴即可
                //防止恶意URL
                string str_timestamp;
                if (context.Request.QueryString["timestamp"] != null)
                {
                    str_timestamp = context.Request.QueryString["timestamp"];
                }
                else
                {
                    continue;
                }
                if (!commonData.list_timestamp.Contains(str_timestamp))
                {
                    TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    str_timestamp = ts.TotalMilliseconds.ToString();
                    commonData.list_timestamp.Add(str_timestamp);
                }
                //确定请求发出方
                deliveryData.cur_timestamp = str_timestamp;
                deliveryData.player_idx = commonData.list_timestamp.IndexOf(str_timestamp);
                if (commonData.list_timestamp.Count >= 2)
                {
                    deliveryData.isPlayerReady = true;
                }
                if (deliveryData.player_idx < 2)//如果此时是玩家的请求
                {
                    if (context.Request.QueryString["mapidx"] != null)
                    {
                        int mapidx = 0;
                        int.TryParse(context.Request.QueryString["mapidx"], out mapidx);
                        if (commonData.mapData[1 - deliveryData.player_idx, mapidx] == MapGridStatus.Blank)
                        {
                            commonData.mapData[1 - deliveryData.player_idx, mapidx] = MapGridStatus.BlankChosen;
                            commonData.list_ready[deliveryData.player_idx] = false;
                            commonData.list_ready[1 - deliveryData.player_idx] = true;
                        }
                        else if (commonData.mapData[1 - deliveryData.player_idx, mapidx] == MapGridStatus.Ship)
                        {
                            commonData.mapData[1 - deliveryData.player_idx, mapidx] = MapGridStatus.ShipChosen;
                            SetBlankChosen(1 - deliveryData.player_idx, mapidx);
                            commonData.list_leftShip[1 - deliveryData.player_idx]--;
                            if (commonData.list_leftShip[1 - deliveryData.player_idx] == 0)
                            {
                                deliveryData.isGameOver = true;
                                deliveryData.winner_idx = deliveryData.player_idx;
                            }
                        }
                    }
                    else
                    {
                        deliveryData.isReady = commonData.list_ready[deliveryData.player_idx];
                    }
                }
                else
                {
                    //观众处理
                }

                #endregion

                //刷新数据
                deliveryData.mapData = (MapGridStatus[,])commonData.mapData.Clone();
                if (deliveryData.player_idx < 2)
                {
                    deliveryData.CleanMapData(1 - deliveryData.player_idx);
                }

                //传回的响应
                HttpListenerResponse response = context.Response;
                last_response = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
                //服务端上显示连接记录
                Console.WriteLine("player: {0} linked, id: {1}", context.Request.Url, commonData.list_timestamp[deliveryData.player_idx]);
                //传输数据部分
                byte[] buffer = SerializeHelper.SerializeToBinary(deliveryData);
                using (Stream output = response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                }
            }

        }
        /// <summary>
        /// 随机生成地图，用于测试
        /// </summary>
        /// <returns>地图数据</returns>
        static MapGridStatus[,] GenerateTestMap()
        {
            MapGridStatus[,] data = new MapGridStatus[2, 100];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    data[i, j] = (MapGridStatus)RandomGenerator.GetNumber(0, 4);
                }
            }
            return data;
        }
        /// <summary>
        /// 临时生成地图图像，用于测试
        /// </summary>
        static void Showmaps()
        {
            for (int i = 0; i < 2; i++)
            {
                int count = 0;
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        Console.Write((int)commonData.mapData[i, count++]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        static MapGridStatus[,] GenerateMaps()
        {
            MapGridStatus[,] data = new MapGridStatus[2, 100];
            for (int i = 0; i < 2; i++)
            {
                MapGridStatus[] oneMap = new MapGridStatus[100];
                bool[] list_used = new bool[100];
                for (int j = 0; j < 100; j++)
                {
                    list_used[j] = false;
                }
                for (int j = 4; j > 0; j--)
                {
                    for (int k = 0; k < 5 - j; k++)
                    {
                        int[] ship = GetShip(list_used, j);
                        while (!IsShipValid(oneMap, ship))
                        {
                            ship = GetShip(list_used, j);
                        }
                        //将找好的船加入字典当中
                        commonData.dict_ship[i][j.ToString() + k.ToString()] = ship;
                        SetShipOnMap(oneMap, ship);
                        for (int l = 0; l < 100; l++)
                        {
                            if (oneMap[l] == MapGridStatus.BlankChosen || oneMap[l] == MapGridStatus.Ship)
                            {
                                list_used[l] = true;
                            }
                        }
                    }
                }
                for (int j = 0; j < 100; j++)
                {
                    if (oneMap[j] != MapGridStatus.Ship)
                    {
                        oneMap[j] = MapGridStatus.Blank;
                    }
                }

                for (int j = 0; j < 100; j++)
                {
                    data[i, j] = oneMap[j];
                }
            }
            return data;
        }
        static int[] GetShip(bool[] list_used, int len)
        {
            int[] res = new int[len];
            int direct = RandomGenerator.GetNumber(0, 2);//0为横，1为竖，假设只向右下延伸
            while (true)
            {
                int idx = RandomGenerator.GetNumber(0, 100);
                if (list_used[idx])
                {
                    continue;
                }
                res[0] = idx;
                if (len == 1)
                {
                    break;
                }
                if ((direct == 0 && res[0] % 10 <= 10 - len) || (direct == 1 && res[0] / 10 <= 10 - len))
                {
                    for (int i = 1; i < len; i++)
                    {
                        if (direct == 0)
                        {
                            res[i] = res[0] + i;
                        }
                        else
                        {
                            res[i] = res[0] + 10 * i;
                        }
                    }
                    break;
                }
                else
                {
                    continue;
                }
            }
            return res;
        }
        static bool IsShipValid(MapGridStatus[] map, int[] ship)
        {
            for (int i = 0; i < ship.Length; i++)
            {
                if (map[ship[i]] == MapGridStatus.BlankChosen || map[ship[i]] == MapGridStatus.Ship)
                {
                    return false;
                }
            }
            return true;
        }
        static void SetShipOnMap(MapGridStatus[] map, int[] ship)
        {
            int left = ship[0] % 10 - 1 > 0 ? ship[0] % 10 - 1 : 0;
            int top = ship[0] / 10 - 1 > 0 ? ship[0] / 10 - 1 : 0;
            int right = ship[ship.Length - 1] % 10 + 1 < 9 ? ship[ship.Length - 1] % 10 + 1 : 9;
            int bottom = ship[ship.Length - 1] / 10 + 1 < 9 ? ship[ship.Length - 1] / 10 + 1 : 9;
            for (int i = left; i < right + 1; i++)
            {
                for (int j = top; j < bottom + 1; j++)
                {
                    map[j * 10 + i] = MapGridStatus.BlankChosen;
                }
            }
            for (int i = 0; i < ship.Length; i++)
            {
                map[ship[i]] = MapGridStatus.Ship;
            }
        }

        static bool IsOverTime(double milliseconds)
        {
            double d = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds;
            
            return d - last_response > milliseconds;
        }
        /// <summary>
        /// 每次击中一个船之后，需要将合理的空格自动选中
        /// </summary>
        /// <param name="player_idx">玩家索引</param>
        /// <param name="idx">击中点的索引</param>
        static void SetBlankChosen(int player_idx, int idx)
        {
            //防止将非船的四角改变状态
            if (commonData.mapData[player_idx, idx] != MapGridStatus.Ship && commonData.mapData[player_idx, idx] != MapGridStatus.ShipChosen)
            {
                return;
            }
            int left = idx % 10;
            int top = idx / 10;
            //将四角涂成选中空格状态
            if (left > 0 && top > 0)
            {
                commonData.mapData[player_idx, idx - 11] = MapGridStatus.BlankChosen;
            }
            if (left < 9 && top > 0)
            {
                commonData.mapData[player_idx, idx - 9] = MapGridStatus.BlankChosen;
            }
            if (left > 0 && top < 9)
            {
                commonData.mapData[player_idx, idx + 9] = MapGridStatus.BlankChosen;
            }
            if (left < 9 && top < 9)
            {
                commonData.mapData[player_idx, idx + 11] = MapGridStatus.BlankChosen;
            }
            //如果船沉了，就将一圈全部改成选中空格状态
            if (IsSunk(player_idx, idx))
            {
                int[] sunkShip = commonData.dict_ship[player_idx][commonData.dict_ship_reverse[player_idx][idx]];
                //船以左上为head，右下为tail
                int head_left = sunkShip[0] % 10;
                int head_top = sunkShip[0] / 10;
                int tail_left = sunkShip[sunkShip.Length - 1] % 10;
                int tail_top = sunkShip[sunkShip.Length - 1] / 10;
                if (left > 0)
                {
                    commonData.mapData[player_idx, sunkShip[0] - 1] = MapGridStatus.BlankChosen;
                }
                if (top > 0)
                {
                    commonData.mapData[player_idx, sunkShip[0] - 10] = MapGridStatus.BlankChosen;
                }
                if (left < 9)
                {
                    commonData.mapData[player_idx, sunkShip[sunkShip.Length - 1] + 1] = MapGridStatus.BlankChosen;
                }
                if (top < 9)
                {
                    commonData.mapData[player_idx, sunkShip[sunkShip.Length - 1] + 10] = MapGridStatus.BlankChosen;
                }
            }
        }
        /// <summary>
        /// 判定在击中该点之后该船是否已经沉没
        /// </summary>
        /// <param name="player_idx">玩家索引</param>
        /// <param name="idx">集中点索引</param>
        /// <returns>是否击沉</returns>
        static bool IsSunk(int player_idx, int idx)
        {
            string shipname = commonData.dict_ship_reverse[player_idx][idx];
            foreach (var item in commonData.dict_ship[player_idx][shipname])
            {
                if (commonData.mapData[player_idx, item] != MapGridStatus.ShipChosen)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 生成反向索引
        /// </summary>
        static void GenerateReverseIndex()
        {
            for (int i = 0; i < 2; i++)
            {
                foreach (var item in commonData.dict_ship[i])
                {
                    foreach (var item2 in item.Value)
                    {
                        commonData.dict_ship_reverse[i][item2] = item.Key;
                    }
                }
            }
        }
    }
}
