using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Booking;
using Labb1Restaurant.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Labb1Restaurant.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITableRepository _tableRepository;
        public BookingService(IBookingRepository bookingRepository, ICustomerRepository customerRepository, ITableRepository tableRepository)
        {
            _bookingRepository = bookingRepository;
            _customerRepository = customerRepository;
            _tableRepository = tableRepository;
        }

        public async Task AddBookingAsync(int customerId, BookingInfoAllDTO booking)
        {

            var newCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);

            if (newCustomer == null)
            {
                throw new Exception("There must be a customer input.");
            }

            var TimeAvailabilty = await IsTableAvailableAsync(booking.FK_TableId, booking.BookingStart, booking.BookingEnd);

            if (TimeAvailabilty)
            {
                throw new Exception("Time is already booked, Please pick another date");
            }

            var newBooking = new Booking
            {
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,
                GuestAttending = booking.GuestAttending,
                FK_CustomerId = newCustomer.CustomerId,
                FK_TableId = booking.FK_TableId

            };

            await _bookingRepository.AddBookingAsync(newBooking);
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            await _bookingRepository.DeleteBookingAsync(bookingId);
        }

        public async Task<IEnumerable<BookingInfoAllDTO>> GetAllBookingsAsync()
        {
            var allBookings = await _bookingRepository.GetAllBookingsAsync();
            if (allBookings == null)
            {
                throw new Exception("There is 0 booked tables");
            }

            return allBookings.Select(booking => new BookingInfoAllDTO
            {
                BookingId = booking.BookingId,
                FK_CustomerId = booking.FK_CustomerId,
                CustomerFullName = $"{booking.Customer.FirstName} {booking.Customer.LastName}",
                CustomersPhoneNo = booking.Customer.PhoneNumber,
                GuestAttending = booking.GuestAttending,
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,
                FK_TableId = booking.FK_TableId,
                TableNumber = booking.Table.TableNumber,
            }).ToList();
        }

        public async Task<BookingInfoAllDTO> GetBookingByIdAsync(int bookingId)
        {
            var theBooking = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (theBooking == null) { return null; }

            return new BookingInfoAllDTO
            {
                BookingId = theBooking.BookingId,
                FK_CustomerId = theBooking.FK_CustomerId,
                CustomerFullName = $"{theBooking.Customer.FirstName} {theBooking.Customer.LastName}",
                CustomersPhoneNo = theBooking.Customer.PhoneNumber,
                GuestAttending = theBooking.GuestAttending,
                BookingStart = theBooking.BookingStart,
                BookingEnd = theBooking.BookingEnd,
                FK_TableId = theBooking.Table.TableId,
                TableNumber = theBooking.Table.TableNumber,
            };
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, DateTime bookingStart, DateTime bookingEnd)
        {
            var bookingsList = await _tableRepository.GetTableBookingConnectionByIdAsync(tableId);

            if (bookingsList != null)
            {
                throw new Exception("Booking already taken!");
            }

            foreach (var booking in bookingsList)
            {
                if ((booking.BookingStart == bookingStart && booking.BookingEnd == bookingEnd) ||
                    (booking.BookingStart <= bookingEnd && booking.BookingEnd >= bookingStart))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task UpdateBookingAsync(int bookingId, BookingInfoAllDTO bookingUp)
        {
            var bookingToUpdate = await _bookingRepository.GetBookingByIdAsync(bookingId);

            var checkingTable = await _tableRepository.GetTableByIdAsync(bookingToUpdate.FK_TableId);

            var availableBooking = await IsTableAvailableAsync(checkingTable.TableId, bookingUp.BookingStart, bookingUp.BookingEnd);

            if (!availableBooking)
            {
                throw new Exception("This table and time is already booked");
            }

            var newBooking = new Booking
            {
                GuestAttending = bookingToUpdate.GuestAttending,
                BookingStart = bookingToUpdate.BookingStart,
                BookingEnd = bookingToUpdate.BookingEnd,
            };

            await _bookingRepository.UpdateBookingAsync(newBooking);
        }
    }
}
