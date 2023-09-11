using AutoMapper;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories.Interfaces;
using Stock_Manager_Simulator_Backend.Services.Interfaces;

namespace Stock_Manager_Simulator_Backend.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _mapper = mapper;
        }
        public async Task<List<StockPriceDto>> GetAllStockLastPriceAsync()
        {
            var stockPrices = await _stockRepository.GetAllStockLastPriceAsync();
            var stockPriceDtos = stockPrices
                .Select(stockPrice => _mapper.Map<StockPriceDto>(stockPrice))
                .ToList();
            return stockPriceDtos;
        }
    }
}
