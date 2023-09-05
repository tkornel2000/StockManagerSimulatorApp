using Microsoft.EntityFrameworkCore;
using Stock_Manager_Simulator_Backend.Data;
using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<StockPrice>> GetAllStockLastPriceAsync()
        {
            return _context.StocksPrices
            .OrderByDescending(x => x.UpdateTimeInTimestamp)
            .GroupBy(x => x.StockSymbol)
            .Select(x => new StockPrice
            {
                StockSymbol = x.Key,
                DayHigh = x.First().DayHigh,
                DayLow = x.First().DayLow,
                DayOpen = x.First().DayOpen,
                Price = x.First().Price,
                Id = x.First().Id,
                Stock = x.First().Stock,
                UpdateTimeInTimestamp = x.First().UpdateTimeInTimestamp,
                Volume = x.First().Volume
            })
            .ToListAsync();
        }

        public Task<StockPrice> GetSpecificStockLastPriceAsync(string stockSymbol)
        {
            return _context.StocksPrices
                .Where(x => x.StockSymbol == stockSymbol)
                .OrderByDescending(x => x.UpdateTimeInTimestamp)
                .FirstAsync();
        }
    }
}
