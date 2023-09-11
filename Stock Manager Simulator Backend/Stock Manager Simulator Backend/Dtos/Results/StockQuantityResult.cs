using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Dtos.Results
{
    public class StockQuantityResult
    {
        public string StockSymbol { get; set; } = null!;
        public string StockName { get; set; } = null!;
        public int Quantity { get; set; }
        public float StockPrice { get; set; }
    }
}
