using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task CreateTransactionAsync(Transaction transaction);
        Task<List<Transaction>> GetAllTransactionByUserAsync(int userId);
        public Task<StockQuantityDto> GetAvailableStockQuantityByUserAndSymbolAsync
            (int userId, string stockSymbol);
        Task<List<StockQuantityWithStockDto>> GetAllAvailableStockQuantityByUserAsync(int userId);
    }
}