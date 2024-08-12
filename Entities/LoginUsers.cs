using System.ComponentModel.DataAnnotations;

namespace AppointmentAPI.Entities
{
    public class LoginUsers
    {
        [Key]

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

    }
}
