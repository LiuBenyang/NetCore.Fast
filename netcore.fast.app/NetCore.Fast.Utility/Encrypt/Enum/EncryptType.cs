namespace NetCore.Fast.Utility.Encrypt
{
    /// <summary>
    /// 加密类型
    /// </summary>
    public enum EncryptType
    {
        /// <summary>
        /// AES加密
        /// </summary>
        AES,
        /// <summary>
        /// AES去掉特殊符号加密
        /// </summary>
        AESUnsigned

    }

    /// <summary>
    /// 无逆向加密方式
    /// </summary>
    public enum NoReverseEncryptType
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        MD5,
        /// <summary>
        /// SHA1 加密
        /// </summary>
        SHA1
    }


    /// <summary>
    /// 解密类型
    /// </summary>
    public enum DecryptType
    {
        /// <summary>
        /// AES 解密类型
        /// </summary>
        AES,
        /// <summary>
        /// AESUnsigned 解密类型
        /// </summary>
        AESUnsigned
    }
}
