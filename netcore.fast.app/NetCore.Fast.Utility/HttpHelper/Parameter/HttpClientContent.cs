using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NetCore.Fast.Utility.HttpHelper
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public class HttpClientContent
    {
        /// <summary>
        /// 请求路径
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 请求数据
        /// </summary>
        public string Data { get; set; } = string.Empty;


        /// <summary>
        /// 自定义头类型
        /// </summary>
        public Dictionary<string, string> Headers { get; set; } = null;

        /// <summary>
        /// 请求类型
        /// </summary>
        public HttpMethodType MethodType { get; set; }

        /// <summary>
        /// 转码格式
        /// </summary>
        public Encoding Encode { get; set; } = Encoding.UTF8;

        /// <summary>
        /// 请求内容类型
        /// <para>默认 application/x-www-form-urlencoded</para>
        /// </summary>
        public string ContentType { get; set; } = "application/x-www-form-urlencoded";


        /// <summary>
        /// 设置超时时间 默认为100秒
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

        /// <summary>
        /// 证书
        /// </summary>
        public List<X509Certificate> X509Certificate { get; set; }
    }
}
