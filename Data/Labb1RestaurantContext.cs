using Labb1Restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1Restaurant.Data
{
    public class Labb1RestaurantContext : DbContext
    {
        public Labb1RestaurantContext(DbContextOptions<Labb1RestaurantContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData
                (
                new Customer { Id = 1, FirstName = "Giovanni", LastName = "Rossi", Email = "Giovanni.Rossi@outlook.com", PhoneNumber = "0709876543" },
                new Customer { Id = 2, FirstName = "Dimitris", LastName = "Nikolaou", Email = "Dimitris.Nikolaou@hotmail.com", PhoneNumber = "0708765432" },
                new Customer { Id = 3, FirstName = "Khalid", LastName = "Al-Mansoori", Email = "Khalid.AlMansoori@yahoo.com", PhoneNumber = "0707654321" }
                );

            modelBuilder.Entity<Table>().HasData
                (
                    new Table { Id = 1, TableNumber = 1, TableSeats = 4 },
                    new Table { Id = 2, TableNumber = 2, TableSeats = 4 },
                    new Table { Id = 3, TableNumber = 3, TableSeats = 6 },
                    new Table { Id = 4, TableNumber = 4, TableSeats = 6 },
                    new Table { Id = 5, TableNumber = 5, TableSeats = 8 },
                    new Table { Id = 6, TableNumber = 6, TableSeats = 8 }
                );

            modelBuilder.Entity<Menu>().HasData
                (
                    new Menu { Id = 1, FoodName = "Tagliatelle al Tartufo", FoodInfo = "Homemade tagliatelle pasta tossed in a rich truffle cream sauce, topped with shaved black truffle and Parmesan.", FoodPrice = 195, IsPopular = true, IsAvailable = true },
                    new Menu { Id = 2, FoodName = "Bouillabaisse", FoodInfo = "A classic French seafood stew with fish, shellfish, and aromatic herbs, served with a side of rouille sauce and crusty bread.", FoodPrice = 250, IsPopular = true, IsAvailable = true },
                    new Menu { Id = 3, FoodName = "Lasagna alla Bolognese", FoodInfo = "Layers of fresh pasta filled with slow-cooked beef ragù, béchamel sauce, and Parmesan, baked to perfection.", FoodPrice = 180, IsPopular = false, IsAvailable = true },
                    new Menu { Id = 4, FoodName = "Salade Niçoise", FoodInfo = "A traditional French salad with tuna, hard-boiled eggs, olives, anchovies, and a lemony vinaigrette dressing.", FoodPrice = 135, IsPopular = false, IsAvailable = true },
                    new Menu { Id = 5, FoodName = "Boeuf Bourguignon", FoodInfo = "A traditional French dish featuring tender beef slow-cooked in red wine with carrots, onions, and mushrooms, served with mashed potatoes.", FoodPrice = 210, IsPopular = true, IsAvailable = true },
                    new Menu { Id = 6, FoodName = "Vitello Tonnato", FoodInfo = "Thinly sliced veal served cold, topped with a creamy tuna sauce, capers, and fresh parsley.", FoodPrice = 185, IsPopular = false, IsAvailable = true },
                    new Menu { Id = 7, FoodName = "Chateaubriand", FoodInfo = "A tender center-cut beef fillet, grilled to perfection and served with a béarnaise sauce and crispy fries.", FoodPrice = 380, IsPopular = true, IsAvailable = true }

                );

            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    Id = 1,
                    FK_CustomerId = 1,
                    FK_TableId = 1,
                    BookingDate = new DateTime(2024, 11, 7),
                    BookingStart = new TimeSpan(18, 0, 0),
                    BookingEnd = new TimeSpan(19, 0, 0),
                    GuestAttending = 2
                },
                new Booking
                {
                    Id = 2,
                    FK_CustomerId = 2,
                    FK_TableId = 2,
                    BookingDate = new DateTime(2024, 11, 7),
                    BookingStart = new TimeSpan(20, 0, 0),
                    BookingEnd = new TimeSpan(21, 0, 0),
                    GuestAttending = 4
                },
                new Booking 
                {
                    Id = 3,
                    FK_CustomerId = 3,
                    FK_TableId = 3,
                    BookingDate = new DateTime(2024, 11, 7),
                    BookingStart = new TimeSpan(21, 0, 0),
                    BookingEnd = new TimeSpan(22, 0, 0),
                    GuestAttending = 6
                });
        }
    }


}
// "server=(localdb)\MSSQLLocalDB;Database=Labb1RestaurantContext;Trusted_Connection=True;"