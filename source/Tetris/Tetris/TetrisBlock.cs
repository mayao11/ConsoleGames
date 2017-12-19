using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public enum BlockType
    {
        /// <summary>
        /// 田
        /// </summary>
        Cube = 0,
        /// <summary>
        /// ▔上
        /// </summary>
        LongOne = 1,
        /// <summary>
        /// ┃右
        /// </summary>
        LongTwo = 2,
        /// <summary>
        /// ﹂
        /// </summary>
        JOne = 3,
        /// <summary>
        /// ┏
        /// </summary>
        JTwo = 4,
        /// <summary>
        /// ﹁
        /// </summary>
        JThree = 5,
        /// <summary>
        /// ┛
        /// </summary>
        JFour = 6,
        /// <summary>
        /// ┛
        /// </summary>
        LOne = 7,
        /// <summary>
        /// ┗
        /// </summary>
        LTwo = 8,
        /// <summary>
        /// ┏
        /// </summary>
        LThree = 9,
        /// <summary>
        /// ┓
        /// </summary>
        LFour = 10,
        /// <summary>
        /// ┻
        /// </summary>
        TOne = 11,
        /// <summary>
        /// ┣
        /// </summary>
        TTwo = 12,
        /// <summary>
        /// ┳
        /// </summary>
        TThree = 13,
        /// <summary>
        /// ┫
        /// </summary>
        TFour = 14,
        /// <summary>
        /// <para>□■■</para>
        /// <para>■■□</para>
        /// <para>□□□</para>
        /// </summary>
        OZOne = 15,
        /// <summary>
        /// <para>□■□</para>
        /// <para>□■■</para>
        /// <para>□□■</para>
        /// </summary>
        OZTwo = 16,
        /// <summary>
        /// <para>□□□</para>
        /// <para>□■■</para>
        /// <para>■■□</para>
        /// </summary>
        ZOne = 17,
        /// <summary>
        /// <para>□□■</para>
        /// <para>□■■</para>
        /// <para>□■□</para>
        /// </summary>
        ZTwo = 18,
    }

    public class TetrisBlock
    {
        private MyPoint origin;
        private List<MyPoint> blocks;
        private List<MyPoint> boundsDown;
        private List<MyPoint> boundsLeft;
        private List<MyPoint> boundsRight;
        private ConsoleColor color;
        private BlockType type;
        private static Random random = new Random();

        public TetrisBlock(MyPoint origin)
        {
            Origin = origin;
            Blocks = new List<MyPoint>();
            BoundsDown = new List<MyPoint>();
            BoundsRight = new List<MyPoint>();
            BoundsLeft = new List<MyPoint>();
            Color = ConsoleColor.DarkCyan;
        }

        public TetrisBlock(MyPoint origin, ConsoleColor color)
        {
            Origin = origin;
            Blocks = new List<MyPoint>();
            BoundsDown = new List<MyPoint>();
            BoundsRight = new List<MyPoint>();
            BoundsLeft = new List<MyPoint>();
            Color = color;
        }


        public MyPoint Origin { get => origin; set => origin = value; }
        public List<MyPoint> Blocks { get => blocks; set => blocks = value; }
        public List<MyPoint> BoundsDown { get => boundsDown; set => boundsDown = value; }
        public List<MyPoint> BoundsLeft { get => boundsLeft; set => boundsLeft = value; }
        public List<MyPoint> BoundsRight { get => boundsRight; set => boundsRight = value; }
        public BlockType Type { get => type; set => type = value; }
        public ConsoleColor Color { get => color; set => color = value; }

        public void AddBlock(MyPoint p)
        {
            Blocks.Add(p);
        }

        public void AddBoundDown(MyPoint p)
        {
            BoundsDown.Add(p);
        }

        public void AddBoundLeft(MyPoint p)
        {
            BoundsLeft.Add(p);
        }

        public void AddBoundRight(MyPoint p)
        {
            BoundsRight.Add(p);
        }

        /// <summary>
        /// 随机生成一个方块
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static TetrisBlock GetRandom()
        {
            BlockType type = (BlockType)random.Next(0, 19);
            return GetBlocks(type, new MyPoint(0, 0));
        }

        public static TetrisBlock GetRandomNext()
        {
            BlockType type = (BlockType)random.Next(0, 19);
            return GetBlocks(type, new MyPoint(12, 13));
        }

        public static TetrisBlock RotateBlocks(BlockType type, MyPoint origin)
        {
            switch (type)
            {
                case BlockType.Cube:
                    return GetCube(origin);
                case BlockType.LongOne:
                    return GetLongTwo(origin);
                case BlockType.LongTwo:
                    return GetLongOne(origin);
                case BlockType.JOne:
                    return GetJTwo(origin);
                case BlockType.JTwo:
                    return GetJThree(origin);
                case BlockType.JThree:
                    return GetJFour(origin);
                case BlockType.JFour:
                    return GetJOne(origin);
                case BlockType.LOne:
                    return GetLTwo(origin);
                case BlockType.LTwo:
                    return GetLThree(origin);
                case BlockType.LThree:
                    return GetLFour(origin);
                case BlockType.LFour:
                    return GetLOne(origin);
                case BlockType.OZOne:
                    return GetOZTwo(origin);
                case BlockType.OZTwo:
                    return GetOZOne(origin);
                case BlockType.TOne:
                    return GetTTwo(origin);
                case BlockType.TTwo:
                    return GetTThree(origin);
                case BlockType.TThree:
                    return GetTFour(origin);
                case BlockType.TFour:
                    return GetTOne(origin);
                case BlockType.ZOne:
                    return GetZTwo(origin);
                case BlockType.ZTwo:
                    return GetZOne(origin);
                default:
                    break;
            }
            return GetCube(origin);
        }

        public static TetrisBlock GetBlocks(BlockType type)
        {
            return GetBlocks(type, new MyPoint(0, 0));
        }

        /// <summary>
        /// 返回一个指定类型的方块
        /// </summary>
        /// <param name="type">方块类型</param>
        /// <param name="origin">原点坐标</param>
        /// <returns></returns>
        private static TetrisBlock GetBlocks(BlockType type, MyPoint origin)
        {
            switch (type)
            {
                case BlockType.Cube:
                    return GetCube(new MyPoint(5, -2) + origin);
                case BlockType.LongOne:
                    return GetLongOne(new MyPoint(6, -1) + origin);
                case BlockType.LongTwo:
                    return GetLongTwo(new MyPoint(6, -3) + origin);
                case BlockType.JOne:
                    return GetJOne(new MyPoint(6, -1) + origin);
                case BlockType.JTwo:
                    return GetJTwo(new MyPoint(5, -2) + origin);
                case BlockType.JThree:
                    return GetJThree(new MyPoint(5, -2) + origin);
                case BlockType.JFour:
                    return GetJFour(new MyPoint(6, -2) + origin);
                case BlockType.LOne:
                    return GetLOne(new MyPoint(5, -1) + origin);
                case BlockType.LTwo:
                    return GetLTwo(new MyPoint(5, -2) + origin);
                case BlockType.LThree:
                    return GetLThree(new MyPoint(6, -2) + origin);
                case BlockType.LFour:
                    return GetLFour(new MyPoint(6, -2) + origin);
                case BlockType.OZOne:
                    return GetOZOne(new MyPoint(5, -2) + origin);
                case BlockType.OZTwo:
                    return GetOZTwo(new MyPoint(6, -2) + origin);
                case BlockType.TOne:
                    return GetTOne(new MyPoint(6, -1) + origin);
                case BlockType.TTwo:
                    return GetTTwo(new MyPoint(5, -2) + origin);
                case BlockType.TThree:
                    return GetTThree(new MyPoint(5, -2) + origin);
                case BlockType.TFour:
                    return GetTFour(new MyPoint(6, -2) + origin);
                case BlockType.ZOne:
                    return GetZOne(new MyPoint(5, -2) + origin);
                case BlockType.ZTwo:
                    return GetZTwo(new MyPoint(6, -2) + origin);
                default:
                    break;
            }
            return GetCube(new MyPoint(5, -2) + origin);
        }

        private static TetrisBlock GetCube(MyPoint origin)
        {
            TetrisBlock cube = new TetrisBlock(origin)
            {
                Type = BlockType.Cube,
                Color = ConsoleColor.Yellow
            };
            cube.AddBlock(origin + new MyPoint(0, 0));
            cube.AddBlock(origin + new MyPoint(1, 0));
            cube.AddBlock(origin + new MyPoint(0, 1));
            cube.AddBlock(origin + new MyPoint(1, 1));
            cube.AddBoundLeft(origin + new MyPoint(-1, 0));
            cube.AddBoundLeft(origin + new MyPoint(-1, 1));
            cube.AddBoundRight(origin + new MyPoint(2, 0));
            cube.AddBoundRight(origin + new MyPoint(2, 1));
            cube.AddBoundDown(origin + new MyPoint(0, 2));
            cube.AddBoundDown(origin + new MyPoint(1, 2));
            return cube;
        }
        /// <summary>
        /// 获取一个初始下落位置的LongOne块
        /// </summary>
        /// <returns></returns>
        private static TetrisBlock GetLongOne(MyPoint origin)
        {
            TetrisBlock longOne = new TetrisBlock(origin)
            {
                Type = BlockType.LongOne,
                Color = ConsoleColor.Cyan
            };
            longOne.AddBlock(origin + new MyPoint(-2, 0));
            longOne.AddBlock(origin + new MyPoint(-1, 0));
            longOne.AddBlock(origin + new MyPoint(0, 0));
            longOne.AddBlock(origin + new MyPoint(1, 0));
            longOne.AddBoundLeft(origin + new MyPoint(-3, 0));
            longOne.AddBoundRight(origin + new MyPoint(2, 0));
            longOne.AddBoundDown(origin + new MyPoint(-2, 1));
            longOne.AddBoundDown(origin + new MyPoint(-1, 1));
            longOne.AddBoundDown(origin + new MyPoint(0, 1));
            longOne.AddBoundDown(origin + new MyPoint(1, 1));
            return longOne;
        }
        private static TetrisBlock GetLongTwo(MyPoint origin)
        {
            TetrisBlock longTwo = new TetrisBlock(origin)
            {
                Type = BlockType.LongTwo,
                Color = ConsoleColor.Cyan
            };
            longTwo.AddBlock(origin + new MyPoint(0, -1));
            longTwo.AddBlock(origin + new MyPoint(0, 0));
            longTwo.AddBlock(origin + new MyPoint(0, 1));
            longTwo.AddBlock(origin + new MyPoint(0, 2));
            longTwo.AddBoundLeft(origin + new MyPoint(-1, -1));
            longTwo.AddBoundLeft(origin + new MyPoint(-1, 0));
            longTwo.AddBoundLeft(origin + new MyPoint(-1, 1));
            longTwo.AddBoundLeft(origin + new MyPoint(-1, 2));
            longTwo.AddBoundRight(origin + new MyPoint(1, -1));
            longTwo.AddBoundRight(origin + new MyPoint(1, 0));
            longTwo.AddBoundRight(origin + new MyPoint(1, 1));
            longTwo.AddBoundRight(origin + new MyPoint(1, 2));
            longTwo.AddBoundDown(origin + new MyPoint(0, 3));
            return longTwo;
        }
        private static TetrisBlock GetJOne(MyPoint origin)
        {
            TetrisBlock JOne = new TetrisBlock(origin)
            {
                Type = BlockType.JOne,
                Color = ConsoleColor.Blue
            };
            JOne.AddBlock(origin + new MyPoint(-1, -1));
            JOne.AddBlock(origin + new MyPoint(-1, 0));
            JOne.AddBlock(origin + new MyPoint(0, 0));
            JOne.AddBlock(origin + new MyPoint(1, 0));
            JOne.AddBoundLeft(origin + new MyPoint(-2, -1));
            JOne.AddBoundLeft(origin + new MyPoint(-1, -1));
            JOne.AddBoundRight(origin + new MyPoint(0, -1));
            JOne.AddBoundRight(origin + new MyPoint(2, 0));
            JOne.AddBoundDown(origin + new MyPoint(-1, 1));
            JOne.AddBoundDown(origin + new MyPoint(0, 1));
            JOne.AddBoundDown(origin + new MyPoint(1, 1));
            return JOne;
        }
        private static TetrisBlock GetJTwo(MyPoint origin)
        {
            TetrisBlock JTwo = new TetrisBlock(origin)
            {
                Type = BlockType.JTwo,
                Color = ConsoleColor.Blue
            };
            JTwo.AddBlock(origin + new MyPoint(0, -1));
            JTwo.AddBlock(origin + new MyPoint(1, -1));
            JTwo.AddBlock(origin + new MyPoint(0, 0));
            JTwo.AddBlock(origin + new MyPoint(0, 1));
            JTwo.AddBoundLeft(origin + new MyPoint(-1, -1));
            JTwo.AddBoundLeft(origin + new MyPoint(-1, 0));
            JTwo.AddBoundLeft(origin + new MyPoint(-1, 1));
            JTwo.AddBoundRight(origin + new MyPoint(2, -1));
            JTwo.AddBoundRight(origin + new MyPoint(1, 0));
            JTwo.AddBoundRight(origin + new MyPoint(1, 1));
            JTwo.AddBoundDown(origin + new MyPoint(1, 0));
            JTwo.AddBoundDown(origin + new MyPoint(0, 2));
            return JTwo;
        }
        private static TetrisBlock GetJThree(MyPoint origin)
        {
            TetrisBlock JThree = new TetrisBlock(origin)
            {
                Type = BlockType.JThree,
                Color = ConsoleColor.Blue
            };
            JThree.AddBlock(origin + new MyPoint(-1, 0));
            JThree.AddBlock(origin + new MyPoint(0, 0));
            JThree.AddBlock(origin + new MyPoint(1, 0));
            JThree.AddBlock(origin + new MyPoint(1, 1));
            JThree.AddBoundLeft(origin + new MyPoint(-2, 0));
            JThree.AddBoundLeft(origin + new MyPoint(0, 1));
            JThree.AddBoundRight(origin + new MyPoint(2, 0));
            JThree.AddBoundRight(origin + new MyPoint(2, 1));
            JThree.AddBoundDown(origin + new MyPoint(-1, 1));
            JThree.AddBoundDown(origin + new MyPoint(0, 1));
            JThree.AddBoundDown(origin + new MyPoint(1, 2));
            return JThree;
        }
        private static TetrisBlock GetJFour(MyPoint origin)
        {
            TetrisBlock JFour = new TetrisBlock(origin)
            {
                Type = BlockType.JFour,
                Color = ConsoleColor.Blue
            };
            JFour.AddBlock(origin + new MyPoint(0, -1));
            JFour.AddBlock(origin + new MyPoint(0, 0));
            JFour.AddBlock(origin + new MyPoint(-1, 1));
            JFour.AddBlock(origin + new MyPoint(-0, 1));
            JFour.AddBoundLeft(origin + new MyPoint(-1, -1));
            JFour.AddBoundLeft(origin + new MyPoint(-1, 0));
            JFour.AddBoundLeft(origin + new MyPoint(-2, 1));
            JFour.AddBoundRight(origin + new MyPoint(1, -1));
            JFour.AddBoundRight(origin + new MyPoint(1, 0));
            JFour.AddBoundRight(origin + new MyPoint(1, 1));
            JFour.AddBoundDown(origin + new MyPoint(-1, 2));
            JFour.AddBoundDown(origin + new MyPoint(0, 2));
            return JFour;
        }
        private static TetrisBlock GetLOne(MyPoint origin)
        {
            TetrisBlock LOne = new TetrisBlock(origin)
            {
                Type = BlockType.LOne,
                Color = ConsoleColor.DarkYellow
            };
            LOne.AddBlock(origin + new MyPoint(1, -1));
            LOne.AddBlock(origin + new MyPoint(-1, 0));
            LOne.AddBlock(origin + new MyPoint(0, 0));
            LOne.AddBlock(origin + new MyPoint(1, 0));
            LOne.AddBoundLeft(origin + new MyPoint(1, -1));
            LOne.AddBoundLeft(origin + new MyPoint(-2, 0));
            LOne.AddBoundRight(origin + new MyPoint(2, -1));
            LOne.AddBoundRight(origin + new MyPoint(2, 0));
            LOne.AddBoundDown(origin + new MyPoint(-1, 1));
            LOne.AddBoundDown(origin + new MyPoint(0, 1));
            LOne.AddBoundDown(origin + new MyPoint(1, 0));
            return LOne;
        }
        private static TetrisBlock GetLTwo(MyPoint origin)
        {
            TetrisBlock LTwo = new TetrisBlock(origin)
            {
                Type = BlockType.LTwo,
                Color = ConsoleColor.DarkYellow
            };
            LTwo.AddBlock(origin + new MyPoint(0, -1));
            LTwo.AddBlock(origin + new MyPoint(0, 0));
            LTwo.AddBlock(origin + new MyPoint(0, 1));
            LTwo.AddBlock(origin + new MyPoint(1, 1));
            LTwo.AddBoundLeft(origin + new MyPoint(-1, -1));
            LTwo.AddBoundLeft(origin + new MyPoint(-1, 0));
            LTwo.AddBoundLeft(origin + new MyPoint(-1, 1));
            LTwo.AddBoundRight(origin + new MyPoint(1, -1));
            LTwo.AddBoundRight(origin + new MyPoint(1, 0));
            LTwo.AddBoundRight(origin + new MyPoint(2, 1));
            LTwo.AddBoundDown(origin + new MyPoint(0, 2));
            LTwo.AddBoundDown(origin + new MyPoint(1, 2));
            return LTwo;
        }
        private static TetrisBlock GetLThree(MyPoint origin)
        {
            TetrisBlock LThree = new TetrisBlock(origin)
            {
                Type = BlockType.LThree,
                Color = ConsoleColor.DarkYellow
            };
            LThree.AddBlock(origin + new MyPoint(-1, 0));
            LThree.AddBlock(origin + new MyPoint(0, 0));
            LThree.AddBlock(origin + new MyPoint(1, 0));
            LThree.AddBlock(origin + new MyPoint(-1, 1));
            LThree.AddBoundLeft(origin + new MyPoint(-2, 0));
            LThree.AddBoundLeft(origin + new MyPoint(-2, 1));
            LThree.AddBoundRight(origin + new MyPoint(2, 0));
            LThree.AddBoundRight(origin + new MyPoint(0, 1));
            LThree.AddBoundDown(origin + new MyPoint(-1, 2));
            LThree.AddBoundDown(origin + new MyPoint(0, 1));
            LThree.AddBoundDown(origin + new MyPoint(1, 1));
            return LThree;
        }
        private static TetrisBlock GetLFour(MyPoint origin)
        {
            TetrisBlock LFour = new TetrisBlock(origin)
            {
                Type = BlockType.LFour,
                Color = ConsoleColor.DarkYellow
            };
            LFour.AddBlock(origin + new MyPoint(-1, -1));
            LFour.AddBlock(origin + new MyPoint(0, -1));
            LFour.AddBlock(origin + new MyPoint(0, 0));
            LFour.AddBlock(origin + new MyPoint(0, 1));
            LFour.AddBoundLeft(origin + new MyPoint(-2, -1));
            LFour.AddBoundLeft(origin + new MyPoint(-1, 0));
            LFour.AddBoundLeft(origin + new MyPoint(-1, 1));
            LFour.AddBoundRight(origin + new MyPoint(1, -1));
            LFour.AddBoundRight(origin + new MyPoint(1, 0));
            LFour.AddBoundRight(origin + new MyPoint(1, 1));
            LFour.AddBoundDown(origin + new MyPoint(-1, 0));
            LFour.AddBoundDown(origin + new MyPoint(0, 2));
            return LFour;
        }
        private static TetrisBlock GetTOne(MyPoint origin)
        {
            TetrisBlock TOne = new TetrisBlock(origin)
            {
                Type = BlockType.TOne,
                Color = ConsoleColor.Magenta
            };
            TOne.AddBlock(origin + new MyPoint(0, -1));
            TOne.AddBlock(origin + new MyPoint(-1, 0));
            TOne.AddBlock(origin + new MyPoint(0, 0));
            TOne.AddBlock(origin + new MyPoint(1, 0));
            TOne.AddBoundLeft(origin + new MyPoint(-1, -1));
            TOne.AddBoundLeft(origin + new MyPoint(-2, 0));
            TOne.AddBoundRight(origin + new MyPoint(1, -1));
            TOne.AddBoundRight(origin + new MyPoint(2, 0));
            TOne.AddBoundDown(origin + new MyPoint(-1, 1));
            TOne.AddBoundDown(origin + new MyPoint(0, 1));
            TOne.AddBoundDown(origin + new MyPoint(1, 1));
            return TOne;
        }
        private static TetrisBlock GetTTwo(MyPoint origin)
        {
            TetrisBlock TTwo = new TetrisBlock(origin)
            {
                Type = BlockType.TTwo,
                Color = ConsoleColor.Magenta,
            };
            TTwo.AddBlock(origin + new MyPoint(0, -1));
            TTwo.AddBlock(origin + new MyPoint(0, 0));
            TTwo.AddBlock(origin + new MyPoint(1, 0));
            TTwo.AddBlock(origin + new MyPoint(0, 1));
            TTwo.AddBoundLeft(origin + new MyPoint(-1, -1));
            TTwo.AddBoundLeft(origin + new MyPoint(-1, 0));
            TTwo.AddBoundLeft(origin + new MyPoint(-1, 1));
            TTwo.AddBoundRight(origin + new MyPoint(1, -1));
            TTwo.AddBoundRight(origin + new MyPoint(2, 0));
            TTwo.AddBoundRight(origin + new MyPoint(1, 1));
            TTwo.AddBoundDown(origin + new MyPoint(0, 2));
            TTwo.AddBoundDown(origin + new MyPoint(1, 1));
            return TTwo;
        }
        private static TetrisBlock GetTThree(MyPoint origin)
        {
            TetrisBlock TThree = new TetrisBlock(origin)
            {
                Type = BlockType.TThree,
                Color = ConsoleColor.Magenta
            };
            TThree.AddBlock(origin + new MyPoint(-1, 0));
            TThree.AddBlock(origin + new MyPoint(0, 0));
            TThree.AddBlock(origin + new MyPoint(1, 0));
            TThree.AddBlock(origin + new MyPoint(0, 1));
            TThree.AddBoundLeft(origin + new MyPoint(-2, 0));
            TThree.AddBoundLeft(origin + new MyPoint(-1, 1));
            TThree.AddBoundRight(origin + new MyPoint(2, 0));
            TThree.AddBoundRight(origin + new MyPoint(1, 1));
            TThree.AddBoundDown(origin + new MyPoint(-1, 1));
            TThree.AddBoundDown(origin + new MyPoint(0, 2));
            TThree.AddBoundDown(origin + new MyPoint(1, 1));
            return TThree;
        }
        private static TetrisBlock GetTFour(MyPoint origin)
        {
            TetrisBlock TFour = new TetrisBlock(origin)
            {
                Type = BlockType.TFour,
                Color = ConsoleColor.Magenta
            };
            TFour.AddBlock(origin + new MyPoint(0, -1));
            TFour.AddBlock(origin + new MyPoint(-1, 0));
            TFour.AddBlock(origin + new MyPoint(0, 0));
            TFour.AddBlock(origin + new MyPoint(0, 1));
            TFour.AddBoundLeft(origin + new MyPoint(-1, -1));
            TFour.AddBoundLeft(origin + new MyPoint(-2, 0));
            TFour.AddBoundLeft(origin + new MyPoint(-1, 1));
            TFour.AddBoundRight(origin + new MyPoint(1, -1));
            TFour.AddBoundRight(origin + new MyPoint(1, 0));
            TFour.AddBoundRight(origin + new MyPoint(1, 1));
            TFour.AddBoundDown(origin + new MyPoint(-1, 1));
            TFour.AddBoundDown(origin + new MyPoint(0, 2));
            return TFour;
        }
        private static TetrisBlock GetOZOne(MyPoint origin)
        {
            TetrisBlock OZOne = new TetrisBlock(origin)
            {
                Type = BlockType.OZOne,
                Color = ConsoleColor.Green
            };
            OZOne.AddBlock(origin + new MyPoint(0, 0));
            OZOne.AddBlock(origin + new MyPoint(1, 0));
            OZOne.AddBlock(origin + new MyPoint(-1, 1));
            OZOne.AddBlock(origin + new MyPoint(0, 1));
            OZOne.AddBoundLeft(origin + new MyPoint(-1, 0));
            OZOne.AddBoundLeft(origin + new MyPoint(-2, 1));
            OZOne.AddBoundRight(origin + new MyPoint(2, 0));
            OZOne.AddBoundRight(origin + new MyPoint(1, 1));
            OZOne.AddBoundDown(origin + new MyPoint(-1, 2));
            OZOne.AddBoundDown(origin + new MyPoint(0, 2));
            OZOne.AddBoundDown(origin + new MyPoint(1, 1));
            return OZOne;
        }
        private static TetrisBlock GetOZTwo(MyPoint origin)
        {
            TetrisBlock OZTwo = new TetrisBlock(origin)
            {
                Type = BlockType.OZTwo,
                Color = ConsoleColor.Green
            };
            OZTwo.AddBlock(origin + new MyPoint(0, -1));
            OZTwo.AddBlock(origin + new MyPoint(0, 0));
            OZTwo.AddBlock(origin + new MyPoint(1, 0));
            OZTwo.AddBlock(origin + new MyPoint(1, 1));
            OZTwo.AddBoundLeft(origin + new MyPoint(-1, -1));
            OZTwo.AddBoundLeft(origin + new MyPoint(-1, 0));
            OZTwo.AddBoundLeft(origin + new MyPoint(0, 1));
            OZTwo.AddBoundRight(origin + new MyPoint(1, -1));
            OZTwo.AddBoundRight(origin + new MyPoint(2, 0));
            OZTwo.AddBoundRight(origin + new MyPoint(2, 1));
            OZTwo.AddBoundDown(origin + new MyPoint(0, 1));
            OZTwo.AddBoundDown(origin + new MyPoint(1, 2));
            return OZTwo;
        }
        private static TetrisBlock GetZOne(MyPoint origin)
        {
            TetrisBlock ZOne = new TetrisBlock(origin)
            {
                Type = BlockType.ZOne,
                Color = ConsoleColor.Red
            };
            ZOne.AddBlock(origin + new MyPoint(-1, 0));
            ZOne.AddBlock(origin + new MyPoint(0, 0));
            ZOne.AddBlock(origin + new MyPoint(0, 1));
            ZOne.AddBlock(origin + new MyPoint(1, 1));
            ZOne.AddBoundLeft(origin + new MyPoint(-2, 0));
            ZOne.AddBoundLeft(origin + new MyPoint(-1, 1));
            ZOne.AddBoundRight(origin + new MyPoint(1, 0));
            ZOne.AddBoundRight(origin + new MyPoint(2, 1));
            ZOne.AddBoundDown(origin + new MyPoint(-1, 1));
            ZOne.AddBoundDown(origin + new MyPoint(0, 2));
            ZOne.AddBoundDown(origin + new MyPoint(1, 2));
            return ZOne;
        }
        private static TetrisBlock GetZTwo(MyPoint origin)
        {
            TetrisBlock ZTwo = new TetrisBlock(origin)
            {
                Type = BlockType.ZTwo,
                Color = ConsoleColor.Red
            };
            ZTwo.AddBlock(origin + new MyPoint(0, -1));
            ZTwo.AddBlock(origin + new MyPoint(0, 0));
            ZTwo.AddBlock(origin + new MyPoint(-1, 0));
            ZTwo.AddBlock(origin + new MyPoint(-1, 1));
            ZTwo.AddBoundLeft(origin + new MyPoint(-1, -1));
            ZTwo.AddBoundLeft(origin + new MyPoint(-2, 0));
            ZTwo.AddBoundLeft(origin + new MyPoint(-2, 1));
            ZTwo.AddBoundRight(origin + new MyPoint(1, -1));
            ZTwo.AddBoundRight(origin + new MyPoint(1, 0));
            ZTwo.AddBoundRight(origin + new MyPoint(0, 1));
            ZTwo.AddBoundDown(origin + new MyPoint(0, 1));
            ZTwo.AddBoundDown(origin + new MyPoint(-1, 2));
            return ZTwo;
        }

        private void Move(MyPoint p, List<MyPoint> pList)
        {
            for (int i = 0; i < pList.Count; i++)
            {
                pList[i] = pList[i] + p;
            }
        }

        public void Move(MyPoint p)
        {
            Origin = Origin + p;
            Move(p, Blocks);
            Move(p, BoundsDown);
            Move(p, BoundsLeft);
            Move(p, BoundsRight);
        }
    }
}
