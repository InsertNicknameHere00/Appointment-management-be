using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppointmentAPI.Entities
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderAddress { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public int? PromoCodeID { get; set; }

        // Navigation properties
        public PromoCodes PromoCode { get; set; }
        //public Users User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
