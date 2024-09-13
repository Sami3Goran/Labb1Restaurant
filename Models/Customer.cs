using System.ComponentModel.DataAnnotations;

namespace Labb1Restaurant.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
