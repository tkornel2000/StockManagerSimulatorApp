using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Manager_Simulator_Backend.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string StockSymbol { get; set; } = null!;
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public long TimeInTimestamp { get; set; }
        public Stock Stock { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
