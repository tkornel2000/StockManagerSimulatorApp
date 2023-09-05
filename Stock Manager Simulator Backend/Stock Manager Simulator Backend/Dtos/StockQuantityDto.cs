namespace Stock_Manager_Simulator_Backend.Dtos
{
    public class StockQuantityDto
    {
        public string StockSymbol { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
