using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
    /// <summary>
    /// Validation class and fields that must be 
    /// filled in by the administrator to create the role
    /// </summary>
    public class CreateRoleModel
    {
        public string RoleName { get; set; }
    }
}
