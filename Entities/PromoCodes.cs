using System.ComponentModel.DataAnnotations;

namespace AppointmentAPI.Entities
{
    public class PromoCodes
    {
            [Key]
            public int PromoCodeID { get; set; }
            public string Code { get; set; }
            public int DiscountPercentage { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public bool IsActive { get; set; }

            // Navigation to Orders
            public ICollection<Orders> Orders { get; set; }
    }
}
