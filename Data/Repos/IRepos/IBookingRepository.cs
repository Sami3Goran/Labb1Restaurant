using Labb1Restaurant.Models;

namespace Labb1Restaurant.Data.Repos.IRepos
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int bookingId);

        Task<IEnumerable<Booking>> GetBookingByDateAsync(DateTime date);
        Task<IEnumerable<Booking>> GetBookingByTableIdAsync(int tableId);
        Task<IEnumerable<Booking>> GetBookingByCustomerIdAsync(int customerId);
        Task<IEnumerable<Booking>> GetBookingByTableIdAndDateAsync(int tableId, DateTime date);
        Task<IEnumerable<Booking>> GetBookingAsync(int tableId, DateTime date, TimeSpan bookingStart, TimeSpan bookingEnd, int? bookingId = null);

        Task AddBookingAsync(Booking booking);

        Task UpdateBookingAsync(Booking booking);

        Task DeleteBookingAsync(Booking booking);

        Task<bool> IsTableAvailableAsync(int tableId, DateTime date, TimeSpan bookingStart, TimeSpan bookingEnd, int? bookingIdToIgnore);
        Task<bool> IsTableAvailableAsync(int tableId, DateTime date, TimeSpan bookingStart, TimeSpan bookingEnd);

    }
}
