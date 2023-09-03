using Stock_Manager_Simulator_Backend.Dtos;

namespace Stock_Manager_Simulator_Backend.Services
{
    public interface IStockService
    {
        Task<List<StockPriceDto>> GetAllStockLastPriceAsync();
    }
}