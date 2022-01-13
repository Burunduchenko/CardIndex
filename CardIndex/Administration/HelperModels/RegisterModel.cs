using System.ComponentModel.DataAnnotations;

namespace Administration.HelperModels
{
    /// <summary>
    /// Validation class and fields to be 
    /// filled in by the user to register
    /// </summary>
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
    }
}
