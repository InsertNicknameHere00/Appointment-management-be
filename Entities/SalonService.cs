using System.ComponentModel.DataAnnotations;

namespace AppointmentAPI.Entities
{
    public class SalonService
    {
        [Key]
        [Required]
        public int ServiceId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ServiceTitle { get; set; }

        [MaxLength(255)]
        public string ServiceDescription { get; set; }

        //public virtual SalonService SalonService {get;set;}
    }
}
