using Stock_Manager_Simulator_Backend.Enums;

namespace Stock_Manager_Simulator_Backend.Models
{
    public class Rank
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public RankType RankType { get; set; }
        public float CurrentValue { get; set; }
        public float PreviousValue { get; set; }
        public DateTime Datetime { get; set; }
        public User User { get; set; } = null!;
    }
}
