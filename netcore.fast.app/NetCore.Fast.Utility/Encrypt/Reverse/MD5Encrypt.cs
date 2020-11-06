using System;
using System.Security.Cryptography;
using System.Text;

namespace NetCore.Fast.Utility.Encrypt
{
    /// <summary>
    /// 无逆向加密
    /// </summary>
    public class MD5Encrypt
    {
        #region MD5加密
        /// <summary>
        /// 获取MD5密文 默认大写
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <param name="e">枚举类型</param>
        /// <returns></returns>
        public static string Encrypt(string str, Encoding e)
        {
            using (var md5 = MD5.Create())
            {
                return BitConverter.ToString(md5.ComputeHash(e.GetBytes(str)))
                    .ToString().Replace("-", "");
            }
        }

        /// <summary>
        /// 获取MD5密文 默认大写
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            return Encrypt(str, Encoding.UTF8);
        }

        /// <summary>
        /// 小写MD5密文
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <returns></returns>
        public static string EncryptToLower(string str)
        {
            return Encrypt(str).ToLower();
        }

        /// <summary>
        /// 小写MD5密文
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <param name="e">编码格式</param>
        /// <returns></returns>
        public static string EncryptToLower(string str, Encoding e)
        {
            return Encrypt(str, e).ToLower();
        }
        #endregion
    }

}
