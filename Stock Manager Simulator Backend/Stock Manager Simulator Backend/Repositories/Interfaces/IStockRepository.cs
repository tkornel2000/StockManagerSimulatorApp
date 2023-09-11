using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Repositories.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockPrice>> GetAllStockLastPriceAsync();
        Task<StockPrice> GetSpecificStockLastPriceAsync(string stockSymbol);
    }
}