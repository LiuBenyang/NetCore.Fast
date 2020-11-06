using System;

namespace NetCore.Fast.Utility.Encrypt
{
    /// <summary>
    /// 使用加密
    /// </summary>
    public class UseEncrypt
    {
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Decrypt(string key, string text, DecryptType type)
        {

            switch (type)
            {
                case DecryptType.AES:
                    return AESEncrypt.BaseDecrypt(text, key);
                case DecryptType.AESUnsigned:
                    return AESEncrypt.Decrypt(text, key);
                default:
                    throw new Exception("未实现的解密方式");
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Encrypt(string key, string text, EncryptType type)
        {
            switch (type)
            {
                case EncryptType.AES:
                    return AESEncrypt.BaseEncrypt(text, key);
                case EncryptType.AESUnsigned:
                    return AESEncrypt.Encrypt(text, key);
                default:
                    throw new Exception("未实现的解密方式");
            }
        }

        /// <summary>
        /// 无逆向加密
        /// </summary>
        /// <param name="text">加密的明文</param>
        /// <param name="type">加密类型</param>
        /// <returns></returns>
        public static string Encrypt(string text, NoReverseEncryptType type)
        {
            switch (type)
            {
                case NoReverseEncryptType.MD5:
                    return MD5Encrypt.Encrypt(text);
                case NoReverseEncryptType.SHA1:
                    return SHA1Encrypt.Encrypt(text);
                default:
                    throw new Exception("未实现的解密方式");
            }
        }
        /// <summary>
        /// 无逆向加密
        /// </summary>
        /// <param name="text">加密的明文</param>
        /// <returns></returns>
        public static string MD5(string text)
        {
            return Encrypt(text, NoReverseEncryptType.MD5);
        }

        /// <summary>
        /// 无逆向加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SHA1(string text)
        {
            return Encrypt(text, NoReverseEncryptType.SHA1);
        }

        /// <summary>
        /// 无逆向加密
        /// </summary>
        /// <param name="text">加密明文</param>
        /// <param name="salt">加密盐</param>
        /// <returns></returns>
        public static string SHA256(string text, string salt)
        {
            //加盐
            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
            //加密
            byte[] hashBytes = new System.Security.Cryptography.SHA256Managed().ComputeHash(passwordAndSaltBytes);

            string hashString = Convert.ToBase64String(hashBytes);

            return hashString;
        }
    }
}
