using Labb1Restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1Restaurant.Data
{
    public class Labb1RestaurantContext : DbContext
    {
        public Labb1RestaurantContext(DbContextOptions<Labb1RestaurantContext> options) : base (options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Menu> Menus { get; set; }
    }
}
// "server=(localdb)\MSSQLLocalDB;Database=Labb1RestaurantContext;Trusted_Connection=True;"