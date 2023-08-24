using Microsoft.AspNetCore.Identity;

namespace Stock_Manager_Simulator_Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime BirtOfDate { get; set; }
        public string Email { get; set; } = null!;
        public bool IsMan { get; set; }
        public float Money { get; set; }
        public float StockValue { get; set; }
        public bool IsDelete { get; set; }
    }
}
