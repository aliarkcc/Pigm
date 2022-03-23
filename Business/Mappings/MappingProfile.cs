using AutoMapper;
using Entities.Concrete;
using Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDetailDto, User>();
            CreateMap<User, UserDetailDto>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<UserAddDto, User>();
            CreateMap<User, UserAddDto>();

            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<UserDto, UserUpdateDto>();
            CreateMap<UserUpdateDto, UserDto>();
        }
    }
}
