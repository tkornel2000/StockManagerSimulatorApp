using AutoMapper;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.StockValue, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Money, opt => opt.MapFrom(src => 2000000.0f))
                .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => false));
        }
    }
}
