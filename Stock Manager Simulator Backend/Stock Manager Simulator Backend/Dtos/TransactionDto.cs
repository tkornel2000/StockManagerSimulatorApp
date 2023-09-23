using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Dtos
{
    public class TransactionDto
    {
        public string StockSymbol { get; set; } = null!;
        public string StockName { get; set; } = null!;
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float CurrentPrice { get; set; }
        public bool IsPurchase { get; set; }

        public long TimeInTimestamp { get; set; }
    }
}
