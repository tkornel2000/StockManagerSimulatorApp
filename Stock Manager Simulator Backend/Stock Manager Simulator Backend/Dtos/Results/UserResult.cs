using Stock_Manager_Simulator_Backend.Models;

namespace Stock_Manager_Simulator_Backend.Dtos.Results
{
    public class UserResult
    {
        public User? User { get; set; }
        public string? Error { get; set; }
    }
}
