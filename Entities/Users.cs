namespace AppointmentAPI.Entities
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Hosting;
    using System.ComponentModel.DataAnnotations;

    public class Users
    {
        public Users()
        {
            this.Appointments = new List<Appointment>();
        }

        [Key]
        public int? UserID { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [Required]
        public int? RoleID { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
