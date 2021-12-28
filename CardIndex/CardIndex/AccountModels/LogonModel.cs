using System.ComponentModel.DataAnnotations;

namespace CardIndex.AccountModels
{
    public class LogonModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
