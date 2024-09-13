using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1Restaurant.Data.Repos
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Labb1RestaurantContext _context;

        public BookingRepository(Labb1RestaurantContext context)
        {
            _context = context;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            var bookingList = await _context.Bookings.ToListAsync();
            return bookingList;
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            return booking;
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}
