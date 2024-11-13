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

        private IQueryable<Booking> GetDetailedBookings()
        {
            return _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Table);
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            var bookingList = await GetDetailedBookings().ToListAsync();
            return bookingList;
        }

        public async Task<IEnumerable<Booking>> GetBookingAsync(int tableId, DateTime date, TimeSpan bookingStart, TimeSpan bookingEnd, int? bookingId = null)
        {
            var bookings = await _context.Bookings.Where
                (b => b.FK_TableId == tableId &&
                 b.BookingDate == date &&
                 b.Id != bookingId &&
                 b.BookingStart < bookingEnd &&
                 b.BookingEnd > bookingStart).ToListAsync();

            return bookings;
        }

        public async Task<IEnumerable<Booking>> GetBookingByCustomerIdAsync(int customerId)
        {
            var customerBooked = await GetDetailedBookings().Where(b => b.FK_CustomerId == customerId).ToListAsync();
            return customerBooked;
        }

        public async Task<IEnumerable<Booking>> GetBookingByDateAsync(DateTime date)
        {
            var dateForBooking = await GetDetailedBookings().Where(b => b.BookingDate.Date == date.Date).ToListAsync();
            return dateForBooking;
        }

        public async Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            var uniqueBooking = await GetDetailedBookings()
                .FirstOrDefaultAsync(b => b.Id == bookingId);
            return uniqueBooking;
        }

        public async Task<IEnumerable<Booking>> GetBookingByTableIdAndDateAsync(int tableId, DateTime date)
        {
            var dateAndTable = await GetDetailedBookings().Where
                (b => b.BookingDate.Date == date.Date && b.FK_TableId == tableId).ToListAsync();
            return dateAndTable;
        }

        public async Task<IEnumerable<Booking>> GetBookingByTableIdAsync(int tableId)
        {
            var bookedTable = await GetDetailedBookings().Where(r => r.FK_TableId == tableId).ToListAsync();
            return bookedTable;
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime date, TimeSpan bookingStart, TimeSpan bookingEnd, int? bookingIdToIgnore)
        {
            return await _context.Bookings.AnyAsync
                (b => b.FK_TableId == tableId &&
                       b.BookingDate == date &&
                       b.Id != bookingIdToIgnore &&
                       b.BookingStart < bookingEnd &&
                       b.BookingEnd > bookingStart);
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime date, TimeSpan bookingStart, TimeSpan bookingEnd)
        {
            return await _context.Bookings.AnyAsync
                (b => b.FK_TableId == tableId &&
                b.BookingDate == date &&
                b.BookingStart < bookingEnd &&
                b.BookingEnd > bookingStart);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

    }
}
