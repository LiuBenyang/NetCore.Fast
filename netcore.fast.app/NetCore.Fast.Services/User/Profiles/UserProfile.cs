using AutoMapper;
using NetCore.Fast.Infrastructure.JwtTokenHelper.Dto;
using NetCore.Fast.Services.User.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Fast.Services.User.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.User, UserDto>();
            CreateMap<UserDto, Entities.User>();

            CreateMap<UserDto, TokenModel>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.ContactPhone));
        }
    }
}
