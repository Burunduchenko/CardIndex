using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
    /// <summary>
    /// Model that includes the user's 
    /// email and specified access rights
    /// </summary>
    public class AssignUserToRolesModel
    {
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Role field is empty")]
        [MinLength(1, ErrorMessage = "There is no entered roles")]
        public string[] Roles { get; set; }
    }
}
