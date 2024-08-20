using AppointmentAPI.Entities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentAPI.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        //[ForeignKey("userId")]
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public string OrderAddress { get; set; }
        public decimal TotalPrice { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        /* ForeignKey[("UserId")]
         virtual Users User { get; set; }*/
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }
    }
}
