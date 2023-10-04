using Microsoft.EntityFrameworkCore;
using Stock_Manager_Simulator_Backend.Data;
using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories.Interfaces;

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

        public Task<List<StockQuantityWithStockDto>> GetAllAvailableStockQuantityByUserAsync(int userId)
        {
            return _context.Transactions
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.StockSymbol)
                .Select(x => new StockQuantityWithStockDto
                {
                    StockSymbol = x.Key,
                    StockName = x.Select(x => x.Stock.Name).First(),
                    Quantity = x.Sum(x => x.Quantity),
                    Price = x.Select(x => x.Stock.StocksPrices.OrderByDescending(x => x.UpdateTimeInTimestamp).First().Price).First(),
                }).ToListAsync();
        }

    }
}
