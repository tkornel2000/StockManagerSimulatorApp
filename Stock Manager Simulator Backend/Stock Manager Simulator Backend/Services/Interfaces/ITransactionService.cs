using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Dtos.Results;

namespace Stock_Manager_Simulator_Backend.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<string> CreateBuyTransactionAsync(StockQuantityDto stockQuantityDto);
        Task<List<TransactionDto>> GetAllMyTransactionAsync();
        Task<List<StockQuantityWithStockDto>> GetAllMyAvailableStockQuantityAsync();
        Task<string> CreateSellTransactionAsync(StockQuantityDto stockQuantityDto);
    }
}