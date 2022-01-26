using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.HelperModels
{
    public class UpdateUser
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [MinLength(1, ErrorMessage = "Provided first name is too short")]
        [MaxLength(40, ErrorMessage = "Provided first name is too large")]
        public string FirstName { get; set; }
        [MinLength(1, ErrorMessage = "Provided last name is too short")]
        [MaxLength(40, ErrorMessage = "Provided last name is too large")]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Login is required")]
        [MinLength(4, ErrorMessage = "Provided ogin is too short")]
        [MaxLength(20, ErrorMessage = "Provided is too large")]
        public string Login { get; set; }
    }
}
