using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using UtilsLib;
namespace Battleship_client
{
    class Program
    {
        const int canvas_width = 40;
        const int canvas_height = 20;

        public const int map_size = 10;
        static char[,] mapForPaint = new char[2, map_size * map_size];//0为左图，1为右图

        static ConsoleCanvas canvas = null;
        static char[,] buffer = null;
        //static ConsoleColor[,] forecolor_buffer = null;
        //static ConsoleColor[,] backcolor_buffer = null;
        static void Main(string[] args)
        {

            string url_root = "http://127.0.0.1:8080/?timestamp=" + "";
            string url = url_root;
            canvas = new ConsoleCanvas(canvas_width, canvas_height);
            buffer = canvas.GetBuffer();
            //forecolor_buffer = canvas.GetForecolorBuffer();
            //backcolor_buffer = canvas.GetBackcolorBuffer();

            while (true)
            {
                DeliveryData data = Handler(url);
                if (!data.isPlayerReady)//人不齐时等待
                {
                    Console.WriteLine("等待玩家中......");
                    Thread.Sleep(100);
                    Console.Clear();
                    url = url_root + data.cur_timestamp;
                    continue;
                }

                canvas.ClearBuffer_DoubleBuffer();
                InitMap();
                ParseToChar(data);
                if (data.isGameOver)
                {
                    if (data.player_idx < 2)
                    {
                        ParseInfo("游戏结束！" + (data.winner_idx == data.player_idx ? "你赢了！" : "你输了..."), 15);
                    }
                    else
                    {
                        ParseInfo("观战结束！" + data.winner_idx + "号玩家获胜！", 15);
                    }
                    //ParseInfo("房间将在5秒后重启......", 19);
                    canvas.Refresh_DoubleBuffer();
                    break;
                }
               
                if (data.player_idx < 2 && data.isReady)//当玩家回合
                {
                    ParseInfo("请选择打击位置", 15);
                    canvas.Refresh_DoubleBuffer();

                    int left = 5 * 2;
                    int top = 5;
                    int idx = 44;
                    ConsoleKeyInfo cki;
                    while (true)
                    {
                        cki = BlinkAt(ref left, ref top, mapForPaint[0, idx]);
                        if (cki.Key == ConsoleKey.A)
                        {
                            if (left > 1 * 2)
                            {
                                left -= 2;
                                idx--;
                            }
                            else
                            {
                                left = 2;
                            }
                        }
                        else if (cki.Key == ConsoleKey.S)
                        {
                            if (top < 10)
                            {
                                top++;
                                idx += 10;
                            }
                            else
                            {
                                top = 10;
                            }
                        }
                        else if (cki.Key == ConsoleKey.D)
                        {
                            if (left < 10 * 2)
                            {
                                left += 2;
                                idx++;
                            }
                            else
                            {
                                left = 20;
                            }
                        }
                        else if (cki.Key == ConsoleKey.W)
                        {
                            if (top > 1)
                            {
                                top--;
                                idx -= 10;
                            }
                            else
                            {
                                top = 1;
                            }
                        }
                        else
                        {
                            if (mapForPaint[0, idx] == '＿')
                            {
                                url = url_root + data.cur_timestamp + "&mapidx=" + idx.ToString();
                                break;
                            }
                        }
                    }
                }
                else if (data.player_idx < 2)//当非玩家回合
                {
                    //canvas.ClearBuffer_DoubleBuffer();
                    //InitMap();
                    //ParseToChar(data);
                    ParseInfo("对方正在考虑中......", 15);
                    canvas.Refresh_DoubleBuffer();

                    url = url_root + data.cur_timestamp;
                    Thread.Sleep(100);
                }
                else//观众
                {
                    //canvas.ClearBuffer_DoubleBuffer();
                    //InitMap();
                    //ParseToChar(data);
                    ParseInfo("观战中......", 15);
                    canvas.Refresh_DoubleBuffer();

                    url = url_root + data.cur_timestamp;
                    Thread.Sleep(100);
                }
            }
            Console.ReadKey();
        }
        /// <summary>
        /// 使地图指定位置闪烁
        /// </summary>
        /// <param name="left">左右偏移</param>
        /// <param name="top">上下偏移</param>
        /// <param name="metadata">闪烁位置的原字符数据</param>
        static ConsoleKeyInfo BlinkAt(ref int left, ref int top, char metadata)
        {
            ConsoleKeyInfo cki;
            while (true)
            {
                Console.SetCursorPosition(left, top);
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write(metadata);
                Thread.Sleep(50);
                Console.SetCursorPosition(left, top);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(metadata);
                Thread.Sleep(50);
                //以此来跳出循环

                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.A || cki.Key == ConsoleKey.S || cki.Key == ConsoleKey.D || cki.Key == ConsoleKey.W || cki.Key == ConsoleKey.Spacebar)
                    {
                        break;
                    }
                }
            }
            return cki;
        }
        /// <summary>
        /// 从流中读取全部数据
        /// </summary>
        /// <param name="stream">传递数据的流</param>
        /// <returns>读出的数据</returns>
        static byte[] BinaryDataReader(Stream stream)
        {
            List<byte> buffer = new List<byte>();
            using (BinaryReader reader = new BinaryReader(stream))
            {
                while (true)
                {
                    byte[] temp = reader.ReadBytes(1024);
                    if (temp.Length == 0)//如果读不到数据时，说明流中数据读完了
                    {
                        break;
                    }
                    buffer.AddRange(temp);
                }
            }
            return buffer.ToArray();
        }
        /// <summary>
        /// 网络部分
        /// </summary>
        /// <param name="url">服务端的地址</param>
        /// <returns>从服务端获得的数据</returns>
        static DeliveryData Handler(string url)
        {
            DeliveryData data = null;
            try
            {
                #region 请求部分
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                //此处要设定请求的方法的原因在于
                //Create方法得到的只是web的请求，而不一定是http协议的请求，
                //因此需要将http协议中对应的方法进行指定
                request.Method = "GET";
                #endregion
                #region 响应部分
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream input = response.GetResponseStream())
                {
                    byte[] binaryData = BinaryDataReader(input);
                    data = SerializeHelper.DeserializeWithBinary<DeliveryData>(binaryData);
                }
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }

            return data;
        }
        /// <summary>
        /// 初始化地图
        /// </summary>
        static void InitMap()
        {
            #region 画布赋值坐标轴
            for (int i = 0; i < 10; i++)
            {
                buffer[0, 1 + i] = (char)('A' + i);
                buffer[0, 26 + i] = (char)('A' + i);
            }
            for (int i = 0; i < 10; i++)
            {
                buffer[1 + i, 0] = (char)('0' + i);
                buffer[1 + i, 25] = (char)('0' + i);
            }
            #endregion
            #region 画布赋值地图本体
            //Console.BackgroundColor = ConsoleColor.DarkGray;//阴影效果
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        backcolor_buffer[1 + i, 26 + j] = ConsoleColor.DarkGray;
            //    }
            //}

            #endregion 
        }
        /// <summary>
        /// 将数据解析为字符，并且置于canvas的buffer正确位置中
        /// </summary>
        /// <param name="data">响应的数据</param>
        static void ParseToChar(DeliveryData data)
        {
            //当为玩家时
            if (data.player_idx < 2)
            {
                for (int i = 0; i < map_size * map_size; i++)
                {
                    switch (data.mapData[0, i])
                    {
                        case MapGridStatus.Blank:
                            mapForPaint[1 - data.player_idx, i] = '＿';
                            break;
                        case MapGridStatus.Ship:
                            mapForPaint[1 - data.player_idx, i] = '□';
                            break;
                        case MapGridStatus.BlankChosen:
                            mapForPaint[1 - data.player_idx, i] = '○';
                            break;
                        case MapGridStatus.ShipChosen:
                            mapForPaint[1 - data.player_idx, i] = '※';
                            break;
                        default:
                            break;
                    }
                    switch (data.mapData[1, i])
                    {
                        case MapGridStatus.Blank:
                            mapForPaint[data.player_idx, i] = '＿';
                            break;
                        case MapGridStatus.Ship:
                            mapForPaint[data.player_idx, i] = '□';
                            break;
                        case MapGridStatus.BlankChosen:
                            mapForPaint[data.player_idx, i] = '○';
                            break;
                        case MapGridStatus.ShipChosen:
                            mapForPaint[data.player_idx, i] = '※';
                            break;
                        default:
                            break;
                    }
                }

            }
            //当为观察者时
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < map_size * map_size; j++)
                    {
                        switch (data.mapData[i, j])
                        {
                            case MapGridStatus.Blank:
                                mapForPaint[i, j] = '＿';
                                break;
                            case MapGridStatus.Ship:
                                mapForPaint[i, j] = '□';
                                break;
                            case MapGridStatus.BlankChosen:
                                mapForPaint[i, j] = '○';
                                break;
                            case MapGridStatus.ShipChosen:
                                mapForPaint[i, j] = '※';
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            for (int i = 0; i < map_size * map_size; i++)
            {
                buffer[1 + i / 10, 1 + i % 10] = mapForPaint[0, i];
                buffer[1 + i / 10, 26 + i % 10] = mapForPaint[1, i];
            }
        }
        /// <summary>
        /// 将提示信息显示于屏幕上
        /// </summary>
        /// <param name="str">所要显示的信息</param>
        /// <param name="row">内容所在的行位置</param>
        static void ParseInfo(string str, int row)
        {
            //清理当前行文字
            for (int i = 0; i < canvas_width; i++)
            {
                buffer[row, i] = ' ';
            }
            //写入内容
            for (int i = 0; i < str.Length; i++)
            {
                buffer[row, i] = str[i];
            }
        }
    }
}
