namespace AppointmentAPI.Entities
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Hosting;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;

    public class Users
    {
    /*    public Users()
        {
            this.Appointments = new List<Appointment>();
        }*/

        [Key]
        public int UserID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required]
        public string VerificationStatus { get; set; }

        public string? VerificationToken { get; set; }

        public string? ResetToken { get; set; }

        public string? Title { get; set; }

        public string? Picture { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

        //public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
