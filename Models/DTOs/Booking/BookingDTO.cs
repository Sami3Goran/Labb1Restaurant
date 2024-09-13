namespace Labb1Restaurant.Models.DTOs.Booking
{
    public class BookingDTO
    {
        public int FK_CustomerId { get; set; }
        public int FK_TableId { get; set; }
        public int GuestAttending { get; set; }
        public DateTime BookingStart { get; set; }

    }
}
