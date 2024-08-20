using System.ComponentModel.DataAnnotations;

namespace AppointmentAPI.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation to Orders
        public Orders Orders { get; set; }
    }
}
