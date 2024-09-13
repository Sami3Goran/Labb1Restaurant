using Labb1Restaurant.Models;

namespace Labb1Restaurant.Data.Repos.IRepos
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int bookingId);

        Task AddBookingAsync(Booking booking);

        Task UpdateBookingAsync(Booking booking);

        Task DeleteBookingAsync(int bookingId);

    }
}
