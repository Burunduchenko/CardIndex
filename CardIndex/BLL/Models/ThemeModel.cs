using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ThemeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You cant't create Theme without name")]
        [MaxLength(20, ErrorMessage = "Theme name is too large")]
        public string Name { get; set; }
    }
}
