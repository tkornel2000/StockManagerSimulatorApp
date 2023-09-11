using Microsoft.EntityFrameworkCore;
using Stock_Manager_Simulator_Backend.Models;
using System.Reflection.Metadata;

namespace Stock_Manager_Simulator_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Stock> Stocks { get; set; } = null!;
        public DbSet<StockPrice> StocksPrices { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<Rank> Ranks { get; set; } = null!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.StocksPrices)
                .WithOne(e => e.Stock)
                .HasForeignKey(e => e.StockSymbol)
                .HasPrincipalKey(e => e.StockSymbol);
            
            modelBuilder.Entity<User>()
                .HasMany(e => e.Transactions)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.Transactions)
                .WithOne(e => e.Stock)
                .HasForeignKey(e => e.StockSymbol)
                .HasPrincipalKey(e => e.StockSymbol);
            
            modelBuilder.Entity<User>()
                .HasMany(e => e.Ranks)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id);
        }
    }
}
