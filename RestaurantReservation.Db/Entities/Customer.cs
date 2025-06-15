using System.ComponentModel.DataAnnotations;

namespace RestaurantReservation.Db.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
