﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Manager_Simulator_Backend.Models
{
    public class StockPrice
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string StockSymbol { get; set; } = null!;
        public float Price { get; set; }
        public int Volume { get; set; }
        public float DayHigh { get; set; }
        public float DayLow { get; set; }
        public float DayOpen { get; set; }
        public long UpdateTimeInTimestamp { get; set; }
        public Stock Stock { get; set; } = null!;
    }
}
