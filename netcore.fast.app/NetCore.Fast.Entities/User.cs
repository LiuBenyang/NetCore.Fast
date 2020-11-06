using Dapper.Contrib.Extensions;
using System;

namespace NetCore.Fast.Entities
{

    /// <summary>
    ///  表名 User
    /// </summary>
    [Table("[User]")]
    public class User
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// 主键Id
        /// </summary>
        [ExplicitKey]
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
        /// 部门
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 生日
        /// </summary>
        public string Birth { get; set; } = string.Empty;

        /// <summary>
        /// 密码盐
        /// </summary>
        public string PSalt { get; set; } = string.Empty;
    }
}
