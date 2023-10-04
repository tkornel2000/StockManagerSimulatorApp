using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Manager_Simulator_Backend.Models
{
    public class Stock
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string StockSymbol { get; set; } = null!;
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; } = null!;
        [Column(TypeName = "nvarchar(100)")]
        public string FullName { get; set; } = null!;
        public List<StockPrice> StocksPrices { get; set; } = new List<StockPrice>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
