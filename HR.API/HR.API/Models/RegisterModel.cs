using System.ComponentModel.DataAnnotations;

namespace HR.API.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage="User Name Required")]
        public string Username { get; set; }
        [Required(ErrorMessage="Email Required")]
        public string Email { get; set; }
        [Required(ErrorMessage="Password Required")]
        public string Password { get; set; }
    }
}
