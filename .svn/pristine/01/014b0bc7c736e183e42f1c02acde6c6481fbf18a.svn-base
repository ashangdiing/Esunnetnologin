using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Esunnet.Phone.Phone.Util
{
    public class OpStruct
    {
        //// <summary>
        /// 结构体转byte数组
        /// </summary>
        /// <param name=”structObj”>要转换的结构体</param>
        /// <returns>转换后的byte数组</returns>
        public static byte[] StructToBytes(object structObj)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(structObj);
            //创建byte数组
            byte[] bytes = new byte[size];
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回byte数组
            return bytes;
        }
        /// <summary>
        /// byte数组转结构体
        /// </summary>
        /// <param name=”bytes”>byte数组</param>
        /// <param name=”type”>结构体类型</param>
        /// <returns>转换后的结构体</returns>
        public static T BytesToStuct<T>(byte[] bytes)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(typeof(T));
            //byte数组长度小于结构体的大小
            if (size > bytes.Length)
            {
                //返回空
                return default(T);
            }
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            object obj = Marshal.PtrToStructure(structPtr, typeof(T));
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回结构体
            //Console.WriteLine("大小：{0}:值：{1}", size, Sbw.DbClient.ToJson.Serialize(obj));
            return (T)obj;
        }
        public static long BytesToInt(params byte[] bytes)
        {
            long addr = bytes[0] | bytes[1] << 8 | bytes[2] << 16 | bytes[3] << 24;
            return addr;
        }
        public static string BytesToString(byte[] bytes, int begin, int length)
        {
            byte[] bb = new byte[length];
            for (int j = begin; j < begin + bb.Length; j++)
            {
                bb[j - begin] = bytes[j];
            }
            return System.Text.ASCIIEncoding.Default.GetString(bb);
        }
    }
}
