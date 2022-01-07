using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
    /// <summary>
    /// Model that includes the user's 
    /// email and specified access rights
    /// </summary>
    public class ManipWithUserRole
    {
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(2, ErrorMessage = "The name of theme is too short")]
        [MaxLength(20, ErrorMessage = "The name of theme is too large")]
        public string Role { get; set; }
    }
}
