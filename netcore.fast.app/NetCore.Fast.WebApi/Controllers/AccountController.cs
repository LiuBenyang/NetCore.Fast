using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Fast.Infrastructure.AutoMapper;
using NetCore.Fast.Infrastructure.JwtTokenHelper;
using NetCore.Fast.Infrastructure.JwtTokenHelper.Dto;
using NetCore.Fast.Services.User;
using NetCore.Fast.Services.User.Dto;
using NetCore.Fast.Utility.Encrypt;

namespace NetCore.Fast.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IUserService _User;
        public AccountController(IUserService user)
        {
            this._User = user;
        }

        [HttpPost, Route("Login")]
        public IActionResult Login(UserLoginDto loginDto)
        {
            if (!_User.Exist(loginDto.Username, out UserDto userDto))
                return BadRequest("账号不存在！");

            var pwd = UseEncrypt.SHA256(loginDto.Password, userDto.PSalt);
            if (!pwd.Equals(userDto.Password))
                return BadRequest("账号或密码错误！");

            var tokenModel = userDto.MapTo<TokenModel>();
            var token = JwtTokenHelper.IssueJwt(tokenModel);

            return Ok(new { UserData = tokenModel, Token = token });
        }

        [Authorize]
        [HttpGet, Route("GetUser")]
        public string GetUser()
        {
            var x = 0;
            var y = 9;
            var i = y / x;

            return HttpContext.Items["UserData"].ToString();
        }


        [Authorize]
        [HttpGet, Route("GetBad")]
        public IActionResult GetBad()
        {
            try
            {
                var x = 0;
                var y = 9;
                var i = y / x;

                return Ok(HttpContext.Items["UserData"].ToString());
            }
            catch
            {
                return BadRequest("错误返回");
            }


        }
    }
}
