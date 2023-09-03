using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Repositories
{
    public interface IStockRepository
    {
        Task<List<StockPrice>> GetAllStockLastPriceAsync();
    }
}