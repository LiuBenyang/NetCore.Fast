using NetCore.Fast.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Services.User
{
    public interface IUserService
    {
        UserDto GetUser(Guid id);

        bool Exist(string name, out UserDto user);
    }
}
