using AutoMapper;
using Demo.Entities;

namespace Demo.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Register, User>();
        }
    }
}
