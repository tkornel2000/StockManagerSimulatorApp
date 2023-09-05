namespace Stock_Manager_Simulator_Backend.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string StockSymbol { get; set; } = null!;
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        //public bool IsBuy { get; set; } Nem kell, mert negatív számmal felviszem a mennyiséget
        public long TimeInTimestamp { get; set; }
        public Stock Stock { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
