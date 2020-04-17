using AutoMapper;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.products;
using DOT_NET_CORE_WEBAPI_SQLITE.DTO.users;
using DOT_NET_CORE_WEBAPI_SQLITE.Models;

namespace DOT_NET_CORE_WEBAPI_SQLITE.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {   
            CreateMap<User, UserResponseDto>();
            CreateMap<Product, ProductResponseDto>();
        }
    }
}