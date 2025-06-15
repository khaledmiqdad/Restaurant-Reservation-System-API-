using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.Entities
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }

        [ForeignKey("Table")]
        public int TableId { get; set; }

        public DateTime ReservationDate { get; set; }

        public int PartySize { get; set; }

        public Customer Customer { get; set; }

        public Restaurant Restaurant { get; set; }

        public Table Table { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
