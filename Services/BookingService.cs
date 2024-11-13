using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Booking;
using Labb1Restaurant.Services.IServices;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task AddBookingAsync(BookingDTO booking)
        {
            
            var guestTable = await _tableRepository.GetTableByIdAsync(booking.TableId);
            if (guestTable == null)
            {
                throw new InvalidOperationException($"Table with ID:{booking.TableId} Does not exist");
            }

            var bookDuration = booking.BookingStart.Add(new TimeSpan(2, 0, 0));

            var ifTableOccupied = await _bookingRepository.IsTableAvailableAsync(booking.TableId, booking.BookingDate, booking.BookingStart, bookDuration);

            if (ifTableOccupied)
            {
                throw new InvalidOperationException($"This table with ID: {booking.TableId} is already booked, be so kind and change booking time");
            }

            if (booking.GuestAttending < 1 || booking.GuestAttending > 8)
            {
                throw new InvalidOperationException($"There can only be between 1 to 8 guests at a time");
            }

            var BookedCustomer = await _customerRepository.GetCustomerByLastNameAsync(booking.LastName);
            Customer customer;

            if (BookedCustomer == null)
            {
                customer = new Customer
                {
                    FirstName = booking.FirstName,
                    LastName = booking.LastName,
                    PhoneNumber = booking.CustomersPhoneNo,
                    Email = booking.Email
                };
                await _customerRepository.AddCustomerAsync(customer);
            }
            else
            {
                customer = BookedCustomer;
            }

            var bookNew = new Booking
            {
                FK_TableId = booking.TableId,
                FK_CustomerId = customer.Id,
                GuestAttending = booking.GuestAttending,
                BookingDate = booking.BookingDate,
                BookingStart = booking.BookingStart,
                BookingEnd = bookDuration
            };

            await _bookingRepository.AddBookingAsync(bookNew);
        }

        public async Task DeleteBookingAsync(int bookingId)
        {
            var bookings = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (bookings == null)
            {
                throw new InvalidOperationException("This booking doesn't exist");
            }

            await _bookingRepository.DeleteBookingAsync(bookings);
        }

        public async Task<IEnumerable<BookingPersonDTO>> GetAllBookingsAsync()
        {
            var listOFbooking = await _bookingRepository.GetAllBookingsAsync();
            return listOFbooking.Select(b => new BookingPersonDTO
            {
                Id = b.Id,
                CustomerId = b.FK_CustomerId,
                TableId = b.FK_TableId,
                CustomerFullName = $"{b.Customer.FirstName} {b.Customer.LastName}",
                TableNumber = b.Table.TableNumber,
                BookingDate = b.BookingDate,
                BookingStart = b.BookingStart,
                BookingEnd = b.BookingEnd
            }).ToList();
        }

        public async Task<IEnumerable<BookingPersonDTO>> GetBookingByCustomerIdAsync(int customerId)
        {
            var booking = await _bookingRepository.GetBookingByCustomerIdAsync(customerId);
            return booking.Select(b => new BookingPersonDTO
            {
                Id = b.Id,
                CustomerId = b.FK_CustomerId,
                TableId = b.FK_TableId,
                CustomerFullName = $"{b.Customer.FirstName} {b.Customer.LastName}",
                TableNumber = b.Table.TableNumber,
                BookingDate = b.BookingDate,
                BookingStart = b.BookingStart,
                BookingEnd = b. BookingEnd
            }).ToList();
        }

        public async Task<IEnumerable<BookingPersonDTO>> GetBookingByDateAsync(DateTime date)
        {
            var booking = await _bookingRepository.GetBookingByDateAsync(date);
            return booking.Select(b => new BookingPersonDTO
            {
                Id = b.Id,
                CustomerId = b.FK_CustomerId,
                TableId = b.FK_TableId,
                CustomerFullName = $"{b.Customer.FirstName} {b.Customer.LastName}",
                TableNumber = b.Table.TableNumber,
                BookingDate = b.BookingDate,
                BookingStart = b.BookingStart,
                BookingEnd = b.BookingEnd
            }).ToList();
        }

        public async Task<BookingInfoAllDTO> GetBookingByIdAsync(int bookingId)
        {
            var bookings = await _bookingRepository.GetBookingByIdAsync(bookingId);
            if (bookings == null)
            {
                throw new Exception($"This booking with booking ID:{bookingId} does not exist");
            }

            return new BookingInfoAllDTO
            {
                Id = bookings.Id,
                TableId = bookings.FK_TableId,
                CustomerId = bookings.FK_CustomerId,
                GuestAttending = bookings.GuestAttending,
                TableNumber = bookings.Table.TableNumber,
                CustomerFullName = $"{bookings.Customer.FirstName} {bookings.Customer.LastName}",
                BookingDate = bookings.BookingDate,
                BookingStart = bookings.BookingStart,
                BookingEnd = bookings.BookingEnd
            };
        }

        public async Task<IEnumerable<BookingPersonDTO>> GetBookingByTableIdAndDateAsync(int tableId, DateTime date)
        {
            var booking = await _bookingRepository.GetBookingByTableIdAndDateAsync(tableId, date);
            return booking.Select(b => new BookingPersonDTO
            {
                Id = b.Id,
                CustomerId = b.FK_CustomerId,
                TableId = b.FK_TableId,
                CustomerFullName = $"{b.Customer.FirstName} {b.Customer.LastName}",
                TableNumber = b.Table.TableNumber,
                BookingDate = b.BookingDate,
                BookingStart = b.BookingStart,
                BookingEnd = b.BookingEnd
            }).ToList();
        }

        public async Task<IEnumerable<BookingPersonDTO>> GetBookingByTableIdAsync(int tableId)
        {
            var booking = await _bookingRepository.GetBookingByTableIdAsync(tableId);
            return booking.Select(b => new BookingPersonDTO
            {
                Id = b.Id,
                CustomerId = b.FK_CustomerId,
                TableId = b.FK_TableId,
                CustomerFullName = $"{b.Customer.FirstName} {b.Customer.LastName}",
                TableNumber = b.Table.TableNumber,
                BookingDate = b.BookingDate,
                BookingStart = b.BookingStart,
                BookingEnd = b.BookingEnd
            }).ToList();
        }

        public async Task<bool> IsTableAvailableAsync(int tableId, TimeSpan bookingStart, TimeSpan bookingEnd)
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

        public async Task UpdateBookingAsync(UpdateBookingDTO booking)
        {
            var activeBooking = await _bookingRepository.GetBookingByIdAsync(booking.Id);
            if (activeBooking == null)
            {
                throw new InvalidOperationException($"Booking with ID: {booking.Id} does not exist");
            }

            var tableOfGuest = await _tableRepository.GetTableByIdAsync(booking.TableId);
            if (tableOfGuest == null)
            {
                throw new InvalidOperationException($"Table with ID: {booking.TableId} does not exist");
            }

            var dinnerCompletion = booking.BookingStart.Add(new TimeSpan(2, 0, 0));

            var tableOccupied = await _bookingRepository.IsTableAvailableAsync(booking.TableId, booking.BookingDate, booking.BookingStart, dinnerCompletion, activeBooking.Id);

            if (tableOccupied)
            {
                throw new InvalidOperationException($"Table with ID: {booking.TableId} is already booked, please pick another table");
            }

            if (booking.GuestAttending < 1 || booking.GuestAttending > 20)
            {
                throw new InvalidOperationException($"There can only attend between 1 to 20 guest at the time.");
            }

            activeBooking.GuestAttending = booking.GuestAttending;
            activeBooking.BookingDate = booking.BookingDate;
            activeBooking.BookingStart = booking.BookingStart;
            activeBooking.BookingEnd = dinnerCompletion;
            activeBooking.FK_TableId = booking.TableId;

            await _bookingRepository.UpdateBookingAsync(activeBooking);
        }

    }
}
