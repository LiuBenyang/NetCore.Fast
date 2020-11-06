using System;
using System.Security.Cryptography;
using System.Text;

namespace NetCore.Fast.Utility
{
    /// <summary>
    /// AES加密
    /// </summary>
    public class AESEncrypt
    {
        #region 原版可逆向加密解密

        /// <summary>
        ///原版 AES加密 不去掉==符号
        /// </summary>
        /// <param name="encryptStr">明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string BaseEncrypt(string encryptStr, string key)
        {
            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(encryptStr);
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cTransform = rDel.CreateEncryptor())
                {
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
            }
        }
        /// <summary>
        /// 原版 AES解密 不去掉==符号
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string BaseDecrypt(string decryptStr, string key)
        {
            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = Convert.FromBase64String(decryptStr);
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cTransform = rDel.CreateDecryptor())
                {
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    return UTF8Encoding.UTF8.GetString(resultArray);
                }
            }
        }


        #endregion

        #region 去掉特殊符号可逆向加密解密

        /// <summary>
        /// AES加密 转换成 Base64
        /// </summary>
        /// <param name="encryptStr">明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string encryptStr, string key)
        {
            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(encryptStr);
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cTransform = rDel.CreateEncryptor())
                {
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    return Base64.ToBase64String(resultArray);
                }
            }
        }
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Decrypt(string decryptStr, string key)
        {
            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
                byte[] toEncryptArray = Base64.FromBase64String(decryptStr); //Convert.FromBase64String(decryptStr);
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cTransform = rDel.CreateDecryptor())
                {
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    return UTF8Encoding.UTF8.GetString(resultArray);
                }
            }
        }

        #endregion
    }
}
