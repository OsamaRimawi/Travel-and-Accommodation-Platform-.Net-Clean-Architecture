using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBP.Domain.Entites;

namespace TBP.Infrastructure.Database
{
    public class TBPDbContext : DbContext
    {
        public TBPDbContext() { }

        public TBPDbContext(DbContextOptions<TBPDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<FeaturedDeal> FeaturedDeals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TBPDbContext).Assembly);
            modelBuilder.Seed();
        }
    }
}
