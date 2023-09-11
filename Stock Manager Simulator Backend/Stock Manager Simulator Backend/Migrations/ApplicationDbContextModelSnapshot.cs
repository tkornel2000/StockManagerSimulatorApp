﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stock_Manager_Simulator_Backend.Data;

#nullable disable

namespace Stock_Manager_Simulator_Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Stock_Manager_Simulator_Backend.Models.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("CurrentValue")
                        .HasColumnType("real");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("datetime2");

                    b.Property<float>("PreviousValue")
                        .HasColumnType("real");

                    b.Property<int>("RankType")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("Stock_Manager_Simulator_Backend.Models.Stock", b =>
                {
                    b.Property<string>("StockSymbol")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StockSymbol");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("Stock_Manager_Simulator_Backend.Models.StockPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("DayHigh")
                        .HasColumnType("real");

                    b.Property<float>("DayLow")
                        .HasColumnType("real");

                    b.Property<float>("DayOpen")
                        .HasColumnType("real");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("StockSymbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("UpdateTimeInTimestamp")
                        .HasColumnType("bigint");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockSymbol");

                    b.ToTable("StocksPrices");
                });

            modelBuilder.Entity("Stock_Manager_Simulator_Backend.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("StockSymbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("TimeInTimestamp")
                        .HasColumnType("bigint");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockSymbol");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Stock_Manager_Simulator_Backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("BirthOfDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMan")
                        .HasColumnType("bit");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Money")
                        .HasColumnType("real");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("StockValue")
                        .HasColumnType("real");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Stock_Manager_Simulator_Backend.Models.Rank", b =>
                {
                    b.HasOne("Stock_Manager_Simulator_Backend.Models.User", "User")
                        .WithMany("Ranks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Stock_Manager_Simulator_Backend.Models.StockPrice", b =>
                {
                    b.HasOne("Stock_Manager_Simulator_Backend.Models.Stock", "Stock")
                        .WithMany("StocksPrices")
                        .HasForeignKey("StockSymbol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Stock_Manager_Simulator_Backend.Models.Transaction", b =>
                {
                    b.HasOne("Stock_Manager_Simulator_Backend.Models.Stock", "Stock")
                        .WithMany("Transactions")
                        .HasForeignKey("StockSymbol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Stock_Manager_Simulator_Backend.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Stock_Manager_Simulator_Backend.Models.Stock", b =>
                {
                    b.Navigation("StocksPrices");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Stock_Manager_Simulator_Backend.Models.User", b =>
                {
                    b.Navigation("Ranks");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
