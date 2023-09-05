using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Dtos
{
    public class StockPriceDto
    {
        public string StockSymbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public float Price { get; set; }
        public int Volume { get; set; }
        public float DayHigh { get; set; }
        public float DayLow { get; set; }
        public float DayOpen { get; set; }
        public long UpdateTimeInTimestamp { get; set; }
    }
}
