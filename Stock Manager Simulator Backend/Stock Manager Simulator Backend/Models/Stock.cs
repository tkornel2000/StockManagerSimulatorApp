using System.ComponentModel.DataAnnotations;

namespace Stock_Manager_Simulator_Backend.Models
{
    public class Stock
    {
        [Key]
        public string StockSymbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public List<StockPrice> StocksPrices { get; set; } = new List<StockPrice>();
    }
}
