using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Booking;

namespace Labb1Restaurant.Services.IServices
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingInfoAllDTO>> GetAllBookingsAsync();
        Task<BookingInfoAllDTO> GetBookingByIdAsync(int bookingId);
        Task<bool> IsTableAvailableAsync(int tableId, DateTime bookingStart, DateTime bookingEnd);

        Task AddBookingAsync(int customerId, BookingInfoAllDTO booking);

        Task UpdateBookingAsync(int bookingId, BookingInfoAllDTO bookingUp);

        Task DeleteBookingAsync(int bookingId);
    }
}
