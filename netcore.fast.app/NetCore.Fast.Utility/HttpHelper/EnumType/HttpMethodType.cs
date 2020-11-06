using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Utility.HttpHelper
{
    /// <summary>
    /// 请求类型
    /// </summary>
    public enum HttpMethodType
    {
        /// <summary>
        /// Post 请求
        /// </summary>
        Post,
        /// <summary>
        /// Get 请求
        /// </summary>
        Get,
        /// <summary>
        /// Delete 请求
        /// </summary>
        Delete,
        /// <summary>
        /// Put 请求
        /// </summary>
        Put
    }
}
