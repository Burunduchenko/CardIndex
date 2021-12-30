using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
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
