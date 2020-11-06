using System;
using System.Security.Cryptography;
using System.Text;

namespace NetCore.Fast.Utility.Encrypt
{
    /// <summary>
    ///SHA1加密
    /// </summary>
    public class SHA1Encrypt
    {
        /// <summary>
        /// 获取SHA1密文 默认大写
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <param name="e">枚举类型</param>
        /// <returns></returns>
        public static string Encrypt(string str, Encoding e)
        {
            using (var sha1 = SHA1.Create())
            {
                return BitConverter.ToString(sha1
                    .ComputeHash(e.GetBytes(str)))
                    .ToString().Replace("-", "");
            }
        }

        /// <summary>
        /// 获取SHA1密文 默认大写
        /// </summary>
        /// <param name="str">需要加密的明文</param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            return Encrypt(str, Encoding.UTF8);
        }

        /// <summary>
        /// 小写SHA1密文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptToLower(string str)
        {
            return Encrypt(str).ToLower();
        }

        /// <summary>
        /// 小写SHA1密文
        /// </summary>
        /// <param name="str"></param>
        /// <param name="e">编码</param>
        /// <returns></returns>
        public static string EncryptToLower(string str, Encoding e)
        {
            return Encrypt(str, e).ToLower();
        }
    }
}
