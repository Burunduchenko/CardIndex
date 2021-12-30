using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
    public class CreateRoleModel
    {
        [Required(ErrorMessage = "Role name is required")]
        [MinLength(2, ErrorMessage = "The name of theme is too short")]
        [MaxLength(20, ErrorMessage = "The name of theme is too large")]
        public string RoleName { get; set; }
    }
}
