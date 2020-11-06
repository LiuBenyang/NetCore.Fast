using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Utility.Common
{
    /// <summary>
    /// 使用guid
    /// </summary>
    public class UseGuid
    {

        /// <summary>
        /// 获取Guid
        /// </summary>
        /// <returns></returns>
        public static string Get()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 获取无符号的Guid
        /// </summary>
        /// <returns></returns>
        public static string GetNoSymbol()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
