using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public enum MySymbol
    {
        /// <summary>  
        /// 缺省符号  
        /// </summary>  
        DEFAULT = (UInt16)0xA1A1,
        /// <summary>  
        /// @@  
        /// </summary>  
        AT = (UInt16)0x4040,
        /// <summary>  
        /// ##  
        /// </summary>  
        WELL = (UInt16)0x2323,
        /// <summary>  
        /// ※  
        /// </summary>  
        RICE = (UInt16)0xA1F9,
        /// <summary>  
        /// ☆  
        /// </summary>  
        STAR_EMPTY = (UInt16)0xA1EE,
        /// <summary>  
        /// ★  
        /// </summary>  
        STAR_SOLID = (UInt16)0xA1EF,
        /// <summary>  
        /// ○  
        /// </summary>  
        RING_EMPTY = (UInt16)0xA1F0,
        /// <summary>  
        /// ●  
        /// </summary>  
        RING_SOLID = (UInt16)0xA1F1,
        /// <summary>  
        /// ◇  
        /// </summary>  
        RHOMB_EMPTY = (UInt16)0xA1F3,
        /// <summary>  
        /// ◆  
        /// </summary>  
        RHOMB_SOLID = (UInt16)0xA1F4,
        /// <summary>  
        /// □  
        /// </summary>  
        RECT_EMPTY = (UInt16)0xA1F5,
        /// <summary>  
        /// ■  
        /// </summary>  
        RECT_SOLID = (UInt16)0xA1F6,
        /// <summary>  
        /// △  
        /// </summary>  
        TRIANGLE_EMPTY = (UInt16)0xA1F7,
        /// <summary>  
        /// ▲  
        /// </summary>  
        TRIANGLE_SOLID = (UInt16)0xA1F8,
    }

    internal sealed class MySymbolHelper
    {
        public static string GetStringFromSymbol(MySymbol symbol)
        {
            UInt16 symbalValue = (UInt16)symbol;
            byte[] bytes = { (byte)((symbalValue & 0xFF00) >> 8), (byte)(symbalValue & 0x00FF) };
            return Encoding.Default.GetString(bytes);
        }

        public static string GetSymbolHex(string symbol)
        {
            if (MyText.GetLength(symbol) > 2 || symbol == "")
            {
                throw new ArgumentOutOfRangeException("符号不能为空且长度不能超过2字节！");
            }
            string hex = "0x";
            byte[] bytes = Encoding.Default.GetBytes(symbol);
            foreach (byte b in bytes)
            {
                hex += string.Format("{0:X}", b);
            }
            return hex;
        }
    }
}
