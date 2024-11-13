using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Booking;

namespace Labb1Restaurant.Services.IServices
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingPersonDTO>> GetAllBookingsAsync();
        Task<BookingInfoAllDTO> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<BookingPersonDTO>> GetBookingByCustomerIdAsync(int customerId);
        Task<IEnumerable<BookingPersonDTO>> GetBookingByTableIdAsync(int tableId);
        Task<IEnumerable<BookingPersonDTO>> GetBookingByDateAsync(DateTime date);
        Task<IEnumerable<BookingPersonDTO>> GetBookingByTableIdAndDateAsync(int tableId, DateTime date);
        
        Task<bool> IsTableAvailableAsync(int tableId, TimeSpan bookingStart, TimeSpan bookingEnd);

        Task AddBookingAsync(BookingDTO booking);

        Task UpdateBookingAsync(UpdateBookingDTO booking);

        Task DeleteBookingAsync(int bookingId);
    }
}
