using Microsoft.EntityFrameworkCore;
using Stock_Manager_Simulator_Backend.Data;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public Task<List<Transaction>> GetAllTransactionByUserAsync(int userId)
        {
            return _context.Transactions.Where(x => x.UserId == userId).Include(x => x.Stock).ThenInclude(x => x.StocksPrices).ToListAsync();
        }

        public Task<List<StockQuantityDto>> GetAllCurrentStockQuantityAsyncByUser(int userId)
        {
            return _context.Transactions
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.StockSymbol)
                .Select(x => new StockQuantityDto
                {
                    StockSymbol = x.Key,
                    Quantity = x.Sum(x => x.Quantity)
                }).ToListAsync();
        }

        public Task<StockQuantityDto> GetAvailableStockQuantityByUserAndSymbolAsync
            (int userId, string stockSymbol)
        {
            return _context.Transactions
                .Where(x => x.UserId == userId)
                .Where(x => x.StockSymbol == stockSymbol)
                .GroupBy(x => x.StockSymbol)
                .Select(x => new StockQuantityDto
                {
                    StockSymbol = x.Key,
                    Quantity = x.Sum(x => x.Quantity)
                }).FirstAsync();
        }

        public Task<List<StockQuantityDto>> GetAllAvailableStockQuantityByUserAsync(int userId)
        {
            return _context.Transactions
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.StockSymbol)
                .Select(x => new StockQuantityDto
                {
                    StockSymbol = x.Key,
                    Quantity = x.Sum(x => x.Quantity)
                }).ToListAsync();
        }

        public async Task<float> GetCurrentStockValueByUser(int userId)
        {
            return (await _context.Transactions
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.StockSymbol)
                .Select(x => new
                {
                    StockSymbol = x.Key,
                    Value = x.Sum(x => x.Quantity) * x.First().Stock.StocksPrices.OrderByDescending(x => x.UpdateTimeInTimestamp).First().Price
                }).ToListAsync()).Sum(x => x.Value);
        }

    }
}
