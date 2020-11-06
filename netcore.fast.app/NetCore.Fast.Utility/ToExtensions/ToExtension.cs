using System;
using System.IO;
using System.Text;

namespace NetCore.Fast.Utility.ToExtensions
{
    /// <summary>
    /// to扩展方法
    /// </summary>
    public static class ToExtension
    {
        /// <summary>
        /// 转成成 int16位
        /// </summary>
        /// <param name="a">一个可转换的string</param>
        /// <returns></returns>
        public static Int16 ToInt16(this string a)
        {
            return Convert.ToInt16(a);
        }

        /// <summary>
        /// 转成成 int32位
        /// </summary>
        /// <param name="a">一个可转换的string</param>
        /// <returns></returns>
        public static Int32 ToInt32(this string a)
        {
            return Convert.ToInt32(a);
        }

        /// <summary>
        /// 转成成 int16位
        /// </summary>
        /// <param name="a">一个可转换的string</param>
        /// <returns></returns>
        public static Int64 ToInt64(this string a)
        {
            return Convert.ToInt64(a);
        }


        /// <summary>
        /// Stream 转 Bytes
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            stream.Dispose();
            stream.Close();
            return bytes;
        }

        /// <summary>
        /// Stream 转 String
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="encoding">转码 默认 UTF8</param>
        /// <returns></returns>
        public static string ToNextString(this Stream stream, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            using (var s = new StreamReader(stream, encoding))
            {
                return s.ReadToEnd();
            }
        }

        /// <summary>
        /// 字符串Base64解码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToBase64Decode(this string str, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            byte[] bytes = Convert.FromBase64String(str);
            string decode;
            try
            {
                decode = encoding.GetString(bytes);
            }
            catch
            {
                decode = str;
            }
            return decode;
        }
    }
}

