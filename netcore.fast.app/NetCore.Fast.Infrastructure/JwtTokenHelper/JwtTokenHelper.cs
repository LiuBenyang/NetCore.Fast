using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NetCore.Fast.Utility.Cache;
using NetCore.Fast.Infrastructure.JwtTokenHelper.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetCore.Fast.Infrastructure.JwtTokenHelper
{
    public class JwtTokenHelper
    {
        public static JwtSecurityTokenHandler _jwt = new JwtSecurityTokenHandler();

        #region 生成颁发JWT字符串 public static string IssueJwt(TokenModel tokenModel)

        /// <summary>
        /// 生成颁发JWT字符串
        /// </summary>
        /// <param name="tokenModel"></param>
        /// <returns></returns>
        public static string IssueJwt(TokenModel tokenModel)
        {
            var token = string.Empty;
            try
            {
                // 过期时间
                DateTime expTime = DateTime.Now.AddDays(
                    Convert.ToDouble(AppSettingsInit.jwtSettings.ExpireTime));

                var claims = new Claim[]
                    {
                        new Claim("UserId",tokenModel.UserId.ToString()),
                        new Claim("UserName",tokenModel.UserName),
                        new Claim("RoleId",tokenModel.RoleId.ToString()),
                        new Claim("Phone",tokenModel.Phone),
                        new Claim(ClaimTypes.Expiration,expTime.ToString())
                    };

                // 密钥
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsInit.jwtSettings.SecretKey));
                // 签名
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(
                    issuer: AppSettingsInit.jwtSettings.Issuer,
                    audience: AppSettingsInit.jwtSettings.Audience,
                    claims: claims,
                    expires: expTime,
                    signingCredentials: creds
                    );

                token = _jwt.WriteToken(jwt);
            }
            catch
            {

            }

            if (!string.IsNullOrEmpty(token))
            {
                // 存入服务器缓存
                UseCache.Instance.Add($"Jwt:{tokenModel.UserId}", token,
                    TimeSpan.FromDays(Convert.ToDouble(AppSettingsInit.jwtSettings.ExpireTime)));

                token = $"{JwtBearerDefaults.AuthenticationScheme} {token}";
            }

            return token;
        }

        #endregion

        #region 解析JWT字符串得到登录用户信息 public static TokenModel SerializeJWT(string token)

        /// <summary>
        /// 解析JWT字符串得到登录用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static TokenModel SerializeJWT(string token)
        {
            var tokenModel = new TokenModel();
            JwtSecurityToken jwtToken = _jwt.ReadJwtToken(token);
            try
            {
                var JwtList = jwtToken.Payload;
                tokenModel.UserId = Guid.Parse(JwtList["UserId"].ToString());
                tokenModel.UserName = JwtList["UserName"].ToString();
                tokenModel.RoleId = Convert.ToInt32(JwtList["RoleId"]);
                tokenModel.Phone = JwtList["Phone"].ToString();
            }
            catch
            {
            }

            return tokenModel;
        }

        #endregion
    }
}
