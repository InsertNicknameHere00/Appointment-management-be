using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppointmentAPI.Entities
{
    public class AdminService
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ServiceId")]
        public int ServiceId { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [Required]
        public int ServiceDuration { get; set; }
        [Required]
        public decimal ServicePrice { get; set; }

       // public virtual Users User {get;set;}
        //public virtual SalonService SalonService { get; set; }
    }
}
