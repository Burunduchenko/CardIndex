using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage = "First name is too hort")]
        [MaxLength(50, ErrorMessage = "First name is too large")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        [MinLength(2, ErrorMessage = "Last name is too hort")]
        [MaxLength(50,ErrorMessage = "Last name is too large")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime Created { get; set; }
        [Required(ErrorMessage = "Login is required")]
        [MinLength(4, ErrorMessage = "Login is too hort")]
        [MaxLength(20, ErrorMessage = "Login is too large")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password is too hort")]
        [MaxLength(32, ErrorMessage = "Password is too large")]
        public string Password { get; set; }

        public virtual ICollection<int> ArticleRatesIds { get; set; }
    }
}

