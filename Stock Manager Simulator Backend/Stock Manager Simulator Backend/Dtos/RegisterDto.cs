namespace Stock_Manager_Simulator_Backend.Dtos
{
    public class RegisterDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime BirtOfDate { get; set; }
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;
    }
}
