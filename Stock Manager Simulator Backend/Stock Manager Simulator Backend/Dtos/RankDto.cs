using Stock_Manager_Simulator_Backend.Dtos;
using Stock_Manager_Simulator_Backend.Enums;

namespace Stock_Manager_Simulator_Backend.Models
{
    public class RankDto
    {
        public RankType RankType { get; set; }
        public float CurrentValue { get; set; }
        public float PreviousValue { get; set; }
        public UserDto UserDto { get; set; } = null!;
    }
}
