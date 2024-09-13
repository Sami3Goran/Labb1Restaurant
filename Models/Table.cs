using System.ComponentModel.DataAnnotations;

namespace Labb1Restaurant.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }

        [Required]
        public int TableSeats { get; set; }

        [Required]    
        public int TableNumber { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
