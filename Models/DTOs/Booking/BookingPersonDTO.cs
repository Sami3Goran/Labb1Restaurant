using System.ComponentModel.DataAnnotations;

namespace Labb1Restaurant.Models.DTOs.Booking
{
    public class BookingPersonDTO
    {
        public int Id { get; set; }
        public string CustomerFullName { get; set; }
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan BookingStart { get; set; }
        public TimeSpan BookingEnd { get; set; }
    }
}
