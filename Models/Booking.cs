using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb1Restaurant.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int GuestAttending { get; set; }

        [Required]
        public DateTime BookingStart { get; set; }

        [Required]
        public DateTime BookingEnd { get; set; }

        [ForeignKey("Customer")]
        public int FK_CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Table")]
        public int FK_TableId { get; set; }
        public Table Table { get; set; }
    }
}
