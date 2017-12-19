using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class MyMap
    {
        private int[,] map;

        public MyMap()
        {
            int[,] mapArray = new int[12, 24];
            for (int x = 0; x < mapArray.GetLength(0); x++)
            {
                for (int y = 0; y < mapArray.GetLength(1); y++)
                {
                    if (x == 0 || y == mapArray.GetLength(1) - 1 || x == mapArray.GetLength(0) - 1)
                    {
                        mapArray[x, y] = (int)ConsoleColor.White;
                    }
                    else
                        mapArray[x, y] = -1;
                }
            }
            Map = mapArray;
        }

        public int[,] Map { get => map; set => map = value; }

        public void AddBlocks(TetrisBlock block)
        {
            foreach (MyPoint p in block.Blocks)
            {
                Map[p.X, p.Y + 4] = (int)block.Color;
            }
        }

        public int FullCheck()
        {
            List<int> rows = new List<int>();
            for (int y = 4; y < Map.GetLength(1) - 1; y++)
            {
                bool isFull = true;
                for (int x = 1; x < Map.GetLength(0) - 1; x++)
                {
                    if (Map[x, y] == -1)
                    {
                        isFull = false;
                    }
                }
                if (isFull)
                {
                    rows.Add(y);
                }
            }
            if (rows.Count > 0)
            {
                ClearFull(rows);
                return rows.Count;
            }
            return 0;
        }

        public bool CheckOver()
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 1; x < Map.GetLength(0) - 1; x++)
                {
                    if (Map[x, y] != -1)
                        return true;
                }
            }
            return false;
        }

        private void ClearFull(List<int> rows)
        {
            foreach (int row in rows)
            {
                for (int y = row; y >= 0; y--)
                {
                    for (int x = 1; x < Map.GetLength(0) - 1; x++)
                    {
                        if (y == 0)
                        {
                            Map[x, y] = -1;
                        }
                        else
                        {
                            Map[x, y] = Map[x, y - 1];
                        }
                    }
                }
            }
        }

        public bool CanNotRotate(TetrisBlock block)
        {
            foreach (var p in block.Blocks)
            {
                if (Map[p.X, p.Y + 4] != -1)
                    return true;
            }
            return false;
        }

        public bool IsRightTouch(TetrisBlock block)
        {
            foreach (var p in block.BoundsRight)
            {
                if (Map[p.X, p.Y + 4] != -1)
                    return true;
            }
            return false;
        }

        public bool IsLeftTouch(TetrisBlock block)
        {
            foreach (var p in block.BoundsLeft)
            {
                if (Map[p.X, p.Y + 4] != -1)
                    return true;
            }
            return false;
        }

        public bool IsDownTouch(TetrisBlock block)
        {
            foreach (var p in block.BoundsDown)
            {
                if (Map[p.X, p.Y + 4] != -1)
                    return true;
            }
            return false;
        }
    }
}
