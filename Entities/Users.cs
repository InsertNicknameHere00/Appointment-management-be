namespace AppointmentAPI.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Users
    {
        [Key]
        [Required]
        public int userID { get; set; }

        //[Required]   i removed it for now because it wants in the login username and still wants it but now it is emty.
        //Will have to be looked for future changes.
        public string username { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public int roleID { get; set; }

    }
}
