using System.ComponentModel.DataAnnotations;

namespace CardIndex.AccountModels
{
    public class CreateRoleModel
    {
        [Required, MinLength(5), MaxLength(20)]
        public string RoleName { get; set; }
    }
}
