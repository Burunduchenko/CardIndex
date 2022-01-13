using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
    /// <summary>
    /// Model that includes the user's 
    /// email and specified access rights
    /// </summary>
    public class ManipWithUserRole
    {
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
