namespace AppointmentAPI.Entities
{
    using AppointmentAPI.Entities.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public StatusType Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual Users User { get; set; } = null!;

        public int? ClientId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        public virtual SalonService Service { get; set; } = null!;
    }
}
