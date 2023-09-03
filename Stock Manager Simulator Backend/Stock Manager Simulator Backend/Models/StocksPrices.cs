namespace Stock_Manager_Simulator_Backend.Models
{
    public class StocksPrice
    {
        public int Id { get; set; }
        public string StockSymbol { get; set; } = null!;
        public float Price { get; set; }
        public int Volume { get; set; }
        public float DayHigh { get; set; }
        public float DayLow { get; set; }
        public float DayOpen { get; set; }
        public int UpdateTimeInTimestamp { get; set; }
        public Stock Stock { get; set; } = null!;
    }
}
