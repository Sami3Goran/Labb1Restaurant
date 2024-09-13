using Labb1Restaurant.Models.DTOs.Booking;
using Labb1Restaurant.Models;
using Labb1Restaurant.Services;
using Labb1Restaurant.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Labb1Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingsService;
        public BookingController(IBookingService bookingsService)
        {
            _bookingsService = bookingsService;
        }

        // GET: api/booking
        [HttpGet]
        [Route("getallbookings")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            var bookings = await _bookingsService.GetAllBookingsAsync();
            return Ok(bookings);
        }

        // GET: api/booking/{id}
        [HttpGet]
        [Route("getbookingbyid/{bookingId}")]
        public async Task<ActionResult<Booking>> GetBookingById(int bookingId)
        {
            var booking = await _bookingsService.GetBookingByIdAsync(bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        // POST: api/booking
        [HttpPost]
        [Route("addbooking")]
        public async Task<ActionResult> AddBooking(int customerId, BookingInfoAllDTO booking)
        {
            if (customerId == null)
            {
                return BadRequest("Cannot be null.");
            }

            await _bookingsService.AddBookingAsync(customerId, booking);

            return Ok("Booking has been added");
        }

        // PUT: api/booking/{id}
        [HttpPut]
        [Route("updatebookingbyid")]
        public async Task<ActionResult> UpdateBooking(int bookingId, BookingInfoAllDTO bookingUp)
        {
            if (bookingId == null)
            {
                return BadRequest("You must write an ID");
            }

            var booking = await _bookingsService.GetBookingByIdAsync(bookingId);
            if (booking == null)
            {
                return NotFound();
            }

            await _bookingsService.UpdateBookingAsync(bookingId, bookingUp);
            return Ok("Update successful");
        }


        // DELETE: api/booking/{id}
        [HttpDelete]
        [Route("deletebookingbyid/{bookingId}")]
        public async Task<ActionResult> DeleteBooking(int bookingId)
        {
            if (bookingId == null || bookingId == 0)
            {
                return BadRequest("Input cannot be null");
            }

            await _bookingsService.DeleteBookingAsync(bookingId);

            return Ok("Booking has been deleted.");
        }

    }
}
