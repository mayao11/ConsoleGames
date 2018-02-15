using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UtilsLib
{
    public static class SerializeHelper
    {
        /// <summary>
        /// 将对象数据在本地转换为二进制数据
        /// </summary>
        /// <param name="obj">对象数据</param>
        /// <returns>二进制数据</returns>
        public static byte[] SerializeToBinary(object obj)
        {
            //本例中，对象数据被写入内存的流中
            //此处根据不同的应用场景，可以将流对象进行替换
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, obj);

            byte[] data = ms.ToArray();
            ms.Close();

            return data;
        }
        /// <summary>
        /// 将二进制数据在本地转换为对象数据
        /// </summary>
        /// <param name="data">二进制数据</param>
        /// <returns>object对象数据</returns>
        public static object DeserializeWithBinary(byte[] data)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            //此处将流的位置重新制定为0，是因为在之后的反序列化过程中，要从流的开始处重新解析
            ms.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            object obj = bf.Deserialize(ms);
            //要记得关闭流
            ms.Close();
            return obj;
        }
        /// <summary>
        /// 将二进制数据在本地转换为指定的对象数据
        /// </summary>
        /// <typeparam name="T">指定对象类型</typeparam>
        /// <param name="data">二进制数据</param>
        /// <returns>指定对象的数据</returns>
        public static T DeserializeWithBinary<T>(byte[] data)
        {
            return (T)DeserializeWithBinary(data);
        }
    }
}
