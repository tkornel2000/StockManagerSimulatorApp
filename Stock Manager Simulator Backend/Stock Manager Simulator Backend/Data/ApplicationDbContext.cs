using Microsoft.EntityFrameworkCore;
using Stock_Manager_Simulator_Backend.Models;
using System.Reflection.Metadata;

namespace Stock_Manager_Simulator_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Stock>? Stocks { get; set; }
        public DbSet<StockPrice>? StocksPrices { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>()
                .HasMany(e => e.StocksPrices)
                .WithOne(e => e.Stock)
                .HasForeignKey(e => e.StockSymbol)
                .HasPrincipalKey(e => e.StockSymbol);
        }
    }
}
