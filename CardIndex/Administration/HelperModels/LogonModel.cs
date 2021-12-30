using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
    public class LogonModel
    {
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(4, ErrorMessage = "Provided password unsafe")]
        [MaxLength(16, ErrorMessage = "Provided password too large")]
        public string Password { get; set; }
    }
}
