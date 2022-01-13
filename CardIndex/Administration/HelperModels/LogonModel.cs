using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
    /// <summary>
    /// Validation class and fields that must 
    /// be filled in by the user to log on
    /// </summary>
    public class LogonModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
