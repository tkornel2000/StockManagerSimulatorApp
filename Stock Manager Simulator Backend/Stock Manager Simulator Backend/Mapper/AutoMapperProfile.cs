using AutoMapper;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Dtos.Results;
using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.StockValue, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.Money, opt => opt.MapFrom(src => (float)(Constans.MoneyConstans.Money)))
                .ForMember(dest => dest.IsDelete, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.IsMan, opt => opt.MapFrom(src => src.Gender=="Férfi"?true: false));

            CreateMap<User, UserDto>();

            CreateMap<StockPrice, StockPriceDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Stock.Name))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Stock.FullName));

            CreateMap<StockQuantityDto, Transaction>();
            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.StockName, opt => opt.MapFrom(src => src.Stock.Name))
                .ForMember(dest => dest.CurrentPrice, opt => opt.MapFrom(src => src.Stock.StocksPrices.OrderByDescending(x => x.UpdateTimeInTimestamp).First().Price))
                .ForMember(dest => dest.IsSell, opt => opt.MapFrom(src => src.Quantity<0));

            CreateMap<Rank, RankDto>()
                .ForMember(dest => dest.UserDto, opt => opt.MapFrom(src => src.User));
            
        }
    }
}
