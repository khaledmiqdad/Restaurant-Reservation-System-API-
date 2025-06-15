using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantReservation.Db.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("MenuItem")]
        public int ItemId { get; set; }

        public int Quantity { get; set; }

        public Order Order { get; set; }

        public MenuItem MenuItem { get; set; }
    }
}
