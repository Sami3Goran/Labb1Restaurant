using Labb1Restaurant.Models.DTOs.Booking;
using Labb1Restaurant.Models;
using Labb1Restaurant.Services;
using Labb1Restaurant.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        // GET: api/GetAllBookings
        [HttpGet]
        [Route("GetAllBookings")]
        public async Task<ActionResult<IEnumerable<BookingPersonDTO>>> GetAllBookings()
        {
            var bookings = await _bookingsService.GetAllBookingsAsync();
            if (bookings.IsNullOrEmpty())
            {
                return NotFound("There is no Bookings yet");
            }
            return Ok(bookings);
        }

        // GET: api/GetBookingById/{bookingId}
        [HttpGet]
        [Route("GetBookingById/{id}")]
        public async Task<ActionResult<BookingInfoAllDTO>> GetBookingById(int id)
        {
            var booking = await _bookingsService.GetBookingByIdAsync(id);

            if (booking == null)
            {
                return NotFound("There is no Booking with that ID");
            }

            return Ok(booking);
        }

        // POST: api/AddBooking
        [HttpPost]
        [Route("AddBooking")]
        public async Task<ActionResult> AddBooking([FromBody] BookingDTO booking)
        {
            try
            {
                await _bookingsService.AddBookingAsync(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Booking has been added");
        }

        // PUT: api/UpdateBookingById/{bookingId}
        [HttpPut]
        [Route("UpdateBooking/{id}")]
        public async Task<ActionResult> UpdateBooking(int id, [FromBody] UpdateBookingDTO booking)
        {
            try
            {
                await _bookingsService.UpdateBookingAsync(booking);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("Updates have been done on the booking");
        }


        // DELETE: api/DeleteBookingById/{bookingId}
        [HttpDelete]
        [Route("DeleteBooking/{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            if (id == null || id == 0)
            {
                return BadRequest("Input cannot be null");
            }

            await _bookingsService.DeleteBookingAsync(id);

            return Ok("Booking has been deleted.");
        }

        // GET: api/GetBookingByCustomerId/{customerId}
        [HttpGet]
        [Route("GetBookingByCustomerId/{id}")]
        public async Task<ActionResult<IEnumerable<BookingPersonDTO>>> GetBookingByCustomerId(int id)
        {
            var bookings = await _bookingsService.GetBookingByCustomerIdAsync(id);

            if (bookings.IsNullOrEmpty())
            {
                return NotFound("No bookings found for this customer");
            }

            return Ok(bookings);
        }

        // GET: api/GetBookingByTableId/{tableId}
        [HttpGet]
        [Route("GetBookingByTableId/{id}")]
        public async Task<ActionResult<IEnumerable<BookingPersonDTO>>> GetBookingByTableId(int id)
        {
            var bookings = await _bookingsService.GetBookingByTableIdAsync(id);

            if (bookings.IsNullOrEmpty())
            {
                return NotFound("There is no bookings for that table.");
            }

            return Ok(bookings);
        }

        // GET: api/GetBookingByTableIdAndDate/{tableId}/2024-11-07
        [HttpGet]
        [Route("GetBookingByTableIdAndDate/{id}/{date}")]
        public async Task<ActionResult<IEnumerable<BookingPersonDTO>>> GetBookingByTableIdAndDate(int id, DateTime date)
        {
            var bookings = await _bookingsService.GetBookingByTableIdAndDateAsync(id, date);

            if (bookings.IsNullOrEmpty())
            {
                return Ok(new List<BookingPersonDTO>());
            }

            return Ok(bookings);
        }

        // GET: api/GetBookingByDate/2024-11-07
        [HttpGet]
        [Route("GetBookingByDate/{date}")]
        public async Task<ActionResult<IEnumerable<BookingPersonDTO>>> GetBookingByDate(DateTime date)
        {
            var bookings = await _bookingsService.GetBookingByDateAsync(date);

            if (bookings.IsNullOrEmpty())
            {
                return NotFound("No reservations found.");
            }

            return Ok(bookings);
        }
    }
}
