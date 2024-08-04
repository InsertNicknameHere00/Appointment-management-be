namespace AppointmentAPI.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class Users : IdentityUser
    {
        [Key]
        [Required]
        public int? userID { get; set; }

        [Required]
        public string? username { get; set; }

        [Required]
        public string? email { get; set; }

        [Required]
        public string? password { get; set; }

        [Required]
        public int? roleID { get; set; }

    }
}
