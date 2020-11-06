using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Infrastructure.JwtTokenHelper.Dto
{

    /// <summary>
    /// Token 参数模型
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
    }
}
