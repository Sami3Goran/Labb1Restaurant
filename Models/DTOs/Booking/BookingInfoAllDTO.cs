namespace Labb1Restaurant.Models.DTOs.Booking
{
    public class BookingInfoAllDTO
    {
        public int BookingId { get; set; }
        public string CustomerFullName { get; set; }
        public string CustomersPhoneNo { get; set; }
        public int FK_CustomerId { get; set; }
        public int FK_TableId { get; set; }
        public int TableNumber { get; set; }
        public int GuestAttending { get; set; }
        public DateTime BookingStart { get; set; }
        public DateTime BookingEnd { get; set; }
    }
}
