using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Manager_Simulator_Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public string Username { get; set; } = null!;
        [Column(TypeName = "varchar(64)")]
        public string PasswordHash { get; set; } = null!;
        [Column(TypeName = "nvarchar(20)")]
        public string Firstname { get; set; } = null!;
        [Column(TypeName = "nvarchar(20)")]
        public string Lastname { get; set; } = null!;
        public DateTime BirthOfDate { get; set; }
        [Column(TypeName = "nvarchar(40)")]
        public string Email { get; set; } = null!;
        public bool IsMan { get; set; }
        public float Money { get; set; }
        public float StockValue { get; set; }
        public bool IsDelete { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<Rank> Ranks { get; set; } = new List<Rank>();
    }
}
