using NetCore.Fast.Infrastructure.AutoMapper;
using NetCore.Fast.Infrastructure.Repositories;
using NetCore.Fast.Services.User.Dto;
using System;
using System.Linq;

namespace NetCore.Fast.Services.User
{
    public class UserService : IUserService
    {
        IRepository<Entities.User> _Repository;
        public UserService(IRepository<Entities.User> repository)
        {
            _Repository = repository;
        }

        public bool Exist(string name, out UserDto user)
        {
            var sql = "SELECT * FROM [User] WHERE Username = @name";

            var reuslt = _Repository.Query(sql, new { name });
            user = reuslt.FirstOrDefault().MapTo<UserDto>();

            return reuslt.Any();
        }

        public UserDto GetUser(Guid id)
        {
            return _Repository.Get(id).MapTo<UserDto>();
        }

    }
}
