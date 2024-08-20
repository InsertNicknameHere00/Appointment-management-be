using System.ComponentModel.DataAnnotations;

namespace AppointmentAPI.Entities
{
    public class Product
    {
        [Required]
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string ProductImage { get; set; }
    }
}
