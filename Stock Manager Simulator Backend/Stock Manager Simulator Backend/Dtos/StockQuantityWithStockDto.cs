using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Dtos
{
    public class StockQuantityWithStockDto
    {
        public string StockSymbol { get; set; } = null!;
        public int Quantity { get; set; }
        public string StockName { get; set; } = null!;
        public float Price { get; set; }
    }
}
