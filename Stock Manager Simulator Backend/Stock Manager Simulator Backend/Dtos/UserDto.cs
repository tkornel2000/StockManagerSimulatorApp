﻿namespace Stock_Manager_Simulator_Backend.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime BirthOfDate { get; set; }
        public string Email { get; set; } = null!;
        public bool IsMan { get; set; }
        public float Money { get; set; }
        public float StockValue { get; set; }
    }
}
