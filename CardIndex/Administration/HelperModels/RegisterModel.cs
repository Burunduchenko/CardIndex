using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
    /// <summary>
    /// Validation class and fields to be 
    /// filled in by the user to register
    /// </summary>
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(4, ErrorMessage = "Provided password unsafe")]
        [MaxLength(16, ErrorMessage = "Provided password too large")]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [MinLength(1, ErrorMessage = "Provided first name is too short")]
        [MaxLength(40, ErrorMessage = "Provided first name is too large")]
        public string FirstName { get; set; }
        [MinLength(1, ErrorMessage = "Provided last name is too short")]
        [MaxLength(40, ErrorMessage = "Provided last name is too large")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Login is required")]
        [MinLength(4, ErrorMessage = "Provided ogin is too short")]
        [MaxLength(20, ErrorMessage = "Provided is too large")]
        public string Login { get; set; }
    }
}
