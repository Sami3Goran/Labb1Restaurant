using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labb1Restaurant.Models
{
    public class Booking
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, 250)]
        public int GuestAttending { get; set; }




        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan BookingStart { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan BookingEnd { get; set; }





        [ForeignKey("Customer")]
        public int FK_CustomerId { get; set; }
        [Required]
        public Customer Customer { get; set; }

        [ForeignKey("Table")]
        public int FK_TableId { get; set; }
        [Required]
        public Table Table { get; set; }
    }
}
