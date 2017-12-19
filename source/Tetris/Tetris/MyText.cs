using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    /// <summary>
    /// 字符串处理类，主要是针对中英文字符显示长度不一致的问题。
    /// </summary>
    sealed internal class MyText
    {
        /// <summary>
        /// 获取字符串的显示长度，英文字符一位，中文两位
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int GetLength(string text)
        {
            byte[] bytes;
            int len = 0;
            for (int i = 0; i < text.Length; i++) {
                bytes = Encoding.Default.GetBytes(text.Substring(i, 1));
                len += bytes.Length > 1 ? 2 : 1;
            }
            return len;
        }

        /// <summary>
        /// 按照给定的长度截取字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutText(string text, int len)
        {
            if (len < 0 || len > GetLength(text))
            {
                throw new ArgumentOutOfRangeException();
            }
            int charLen = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length && charLen < len; i++)
            {
                charLen += GetLength(text.Substring(i, 1));
                sb.Append(text[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 在指定的位置按照指定的长度截取字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="index"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutText(string text, int index, int len)
        {
            if (index < 0 || index > text.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (len < 0 || len > GetLength(text))
            {
                throw new ArgumentOutOfRangeException();
            }
            text = text.Substring(index, text.Length - index);
            return CutText(text, len);
        }

        public static string BrText(string text, int cols)
        {
            if (cols < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            int len = 0;
            int charLen = 0;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                len = GetLength(text.Substring(i, 1));
                charLen += len;
                if (charLen > (cols * 2))
                {
                    sb.Append(Environment.NewLine);
                    charLen = len;
                }
                sb.Append(text[i]);
            }
            return sb.ToString();
        }
    }
}
