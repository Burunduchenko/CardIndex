using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
    /// <summary>
    /// Validation class and fields that must be 
    /// filled in by the administrator to create the role
    /// </summary>
    public class CreateRoleModel
    {
        [Required(ErrorMessage = "Role name is required")]
        [MinLength(2, ErrorMessage = "The name of theme is too short")]
        [MaxLength(20, ErrorMessage = "The name of theme is too large")]
        public string RoleName { get; set; }
    }
}
