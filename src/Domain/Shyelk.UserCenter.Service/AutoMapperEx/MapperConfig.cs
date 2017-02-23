using AutoMapper;
using Shyelk.UserCenter.Entity;
using Shyelk.UserCenter.Models;

namespace Shyelk.UserCenter.Service.AutoMapperEx
{
    public class MapperConfig:Profile
    {
        public MapperConfig():base()
        {
            CreateMap<User,UserDto>();
            CreateMap<UserDto,User>();
        }
    }
}