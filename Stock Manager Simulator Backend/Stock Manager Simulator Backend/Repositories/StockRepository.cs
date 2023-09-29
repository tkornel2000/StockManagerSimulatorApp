using Microsoft.EntityFrameworkCore;
using Stock_Manager_Simulator_Backend.Data;
using Stock_Manager_Simulator_Backend.Models;
using Stock_Manager_Simulator_Backend.Repositories.Interfaces;

namespace Stock_Manager_Simulator_Backend.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockPrice>> GetAllStockLastPriceAsync()
        {
            var stockPriceList = new List<StockPrice>();
            foreach (var stockSymbol in Constans.StockConstans.AllStockSymbol)
            {
                stockPriceList.Add(await _context.StocksPrices.Include(x => x.Stock).OrderBy(x => x.Id).LastAsync(x => x.StockSymbol == stockSymbol));
            }
            return stockPriceList;
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
