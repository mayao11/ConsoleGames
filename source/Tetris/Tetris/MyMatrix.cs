using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public enum RotateMode
    {
        /// <summary>  
        /// 不旋转  
        /// </summary>  
        None,
        /// <summary>  
        /// 顺时针旋转90度  
        /// </summary>  
        R90,
        /// <summary>  
        /// 顺时针旋转180度  
        /// </summary>  
        R180,
        /// <summary>  
        /// 顺时针旋转270度  
        /// </summary>  
        R270,
        /// <summary>  
        /// 逆时针旋转90度  
        /// </summary>  
        L90,
        /// <summary>  
        /// 逆时针旋转180度  
        /// </summary>  
        L180,
        /// <summary>  
        /// 逆时针旋转270度  
        /// </summary>  
        L270
    }

    public struct MyMatrix
    {
        private int[,] matrix;
        private int rows;
        private int cols;
        private int count;
        private RotateMode rotate;

        public int Rows { get => rows;}
        public int Cols { get => cols;}
        public int[,] Matrix { get => matrix; set => matrix = value; }
        public int Count { get => count;}
        public RotateMode Rotate { get => rotate; set => rotate = value; }

        public MyMatrix(int[,] matrix)
        {
            this.matrix = matrix;
            rows = matrix.GetLength(0);
            cols = matrix.GetLength(1);
            count = rows * cols;
            rotate = RotateMode.None;
        }
        public MyMatrix(int rows, int cols)
        {
            matrix = new int[rows, cols];
            this.rows = rows;
            this.cols = cols;
            count = rows * cols;
            rotate = RotateMode.None;
        }

        //索引访问
        public int this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0 || x > rows - 1 || y > cols - 1)
                    throw new ArgumentOutOfRangeException();
                return matrix[x, y];
            }
            set
            {
                if (x < 0 || y < 0 || x > rows - 1 || y > cols - 1)
                    throw new ArgumentOutOfRangeException();
                matrix[x, y] = value;
            }
        }

        public void PrintMatrix()
        {
            for(int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Console.Write(Matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
