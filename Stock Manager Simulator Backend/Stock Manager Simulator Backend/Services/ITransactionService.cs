using Stock_Manager_Simulator_Backend.Dtos;

namespace Stock_Manager_Simulator_Backend.Services
{
    public interface ITransactionService
    {
        Task<string> CreateBuyTransactionAsync(StockQuantityDto stockQuantityDto);
        Task<List<TransactionDto>> GetAllMyTransactionAsync();
        Task<List<StockQuantityDto>> GetAllMyAvailableStockQuantityAsync();
        Task<string> CreateSellTransactionAsync(StockQuantityDto stockQuantityDto);
    }
}