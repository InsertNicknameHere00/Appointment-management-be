using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentAPI.Entities
{
    public class OrderItem
    {
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
