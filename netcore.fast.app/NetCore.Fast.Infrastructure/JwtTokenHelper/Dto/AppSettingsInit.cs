using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Infrastructure.JwtTokenHelper.Dto
{
    public class AppSettingsInit
    {
        public static JwtSettings jwtSettings;

        #region 读取JWT配置 public void JwtSettingInit(IConfiguration configuration)

        /// <summary>
        /// 读取JWT配置
        /// </summary>
        /// <param name="configuration"></param>
        public static void JwtSettingInit(IConfiguration configuration)
        {
            jwtSettings = new JwtSettings();
            jwtSettings.Audience = configuration["JwtSettings:Audience"];
            jwtSettings.Issuer = configuration["JwtSettings:Issuer"];
            jwtSettings.SecretKey = configuration["JwtSettings:SecretKey"];
            jwtSettings.ExpireTime = configuration["JwtSettings:ExpireTime"];
        }
        #endregion
    }
}
