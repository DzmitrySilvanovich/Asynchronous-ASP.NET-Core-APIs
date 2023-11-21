using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DAL.Domain;
using Ticketing.DAL.Domains;

namespace Ticketing.DAL.Repositories
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Venue> Venues { get; set; } = null!;
        public DbSet<Section> Sections { get; set; } = null!;
        public DbSet<Seat> Seats { get; set; } = null!;
        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<PaymentStatus> PaymentStatuses { get; set; } = null!;
        public DbSet<PriceType> PriceTypes { get; set; } = null!;
        public DbSet<SeatStatus> SeatStatuses { get; set; } = null!;
        public DbSet<ShoppingCart> ShoppingCarts { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            // uncoment for create database
           //   Database.EnsureDeleted(); // гарантируем, что бд удалена
           //   Database.EnsureCreated(); // гарантируем, что бд будет создана
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message), new[] { DbLoggerCategory.Database.Command.Name },
                      LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // uncoment for create database
            //  modelBuilder.Entity<ShoppingCart>().HasAlternateKey(c => new { c.EventId, c.SeatId });
            //  modelBuilder.Entity<Section>().HasAlternateKey(s => new { s.VenueId, s.Name });

            //  InitializatioData(modelBuilder);
        }

        private void InitializatioData (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeatStatus>().HasData(
              new SeatStatus { Id = 1, Name = "Available" },
              new SeatStatus { Id = 2, Name = "Booked" },
              new SeatStatus { Id = 3, Name = "Sold" });

            modelBuilder.Entity<PaymentStatus>().HasData(
                new PaymentStatus { Id = 1, Name = "No payment" },
                new PaymentStatus { Id = 2, Name = "Part payment" },
                new PaymentStatus { Id = 3, Name = "Full payment" },
                new PaymentStatus { Id = 4, Name = "Payment Failed" });


            modelBuilder.Entity<PriceType>().HasData(
                new PriceType { Id = 1, Name = "Adult" },
                new PriceType { Id = 2, Name = "Child" },
                new PriceType { Id = 3, Name = "VIP" },
                new PriceType { Id = 4, Name = "Admission" });

            modelBuilder.Entity<Venue>().HasData(
                new Venue { Id = 1, Name = "Venue1" },
                new Venue { Id = 2, Name = "Venue2" },
                new Venue { Id = 3, Name = "Venue3" });

            modelBuilder.Entity<Event>().HasData(
                new Event { Id = 1, Name = "Event1", EventDate = DateTime.Now.AddDays(-3) },
                new Event { Id = 2, Name = "Event2", EventDate = DateTime.Now },
                new Event { Id = 3, Name = "Event3", EventDate = DateTime.Now.AddDays(+3) });

            modelBuilder.Entity<Section>().HasData(
                new Section { Id = 1, Name = "Section1", VenueId = 1, PriceTypeId = 1 },
                new Section { Id = 2, Name = "Section2", VenueId = 1, PriceTypeId = 2 },
                new Section { Id = 3, Name = "Section1", VenueId = 2, PriceTypeId = 3 });

            modelBuilder.Entity<Seat>().HasData(
                new Seat { Id = 1, SectionId = 1, RowNumber = 1, SeatNumber = 1, SeatStatusId = 1 },
                new Seat { Id = 2, SectionId = 1, RowNumber = 1, SeatNumber = 2, SeatStatusId = 1 },
                new Seat { Id = 3, SectionId = 1, RowNumber = 1, SeatNumber = 3, SeatStatusId = 1 });

            modelBuilder.Entity<Cart>().HasData(
              new Cart { Id = new Guid("F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4") });
        }
    }
}
