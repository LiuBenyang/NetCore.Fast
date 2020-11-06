using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Infrastructure.JwtTokenHelper.Dto
{

    /// <summary>
    /// Jwt配置模型
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// 接收者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 发布者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 过期时间 || 单位：day
        /// </summary>
        public string ExpireTime { get; set; }
    }
}
