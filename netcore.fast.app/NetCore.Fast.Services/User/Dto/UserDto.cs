using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NetCore.Fast.Services.User.Dto
{
    public class UserDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; } = string.Empty;

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; } = 0;

        /// <summary>
        /// 密码盐
        /// </summary>
        public string PSalt { get; set; } = string.Empty;
    }

    public class UserLoginDto
    {
        [Display(Name = "用户名")]
        [Required]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
