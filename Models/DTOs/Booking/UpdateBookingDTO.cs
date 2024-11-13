using System.ComponentModel.DataAnnotations;

namespace Labb1Restaurant.Models.DTOs.Booking
{
    public class UpdateBookingDTO
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public int GuestAttending { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan BookingStart { get; set; }
    }
}
