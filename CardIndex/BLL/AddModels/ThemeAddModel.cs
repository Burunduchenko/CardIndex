using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    public class ThemeAddModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "Theme name is too short")]
        [MaxLength(20, ErrorMessage = "Theme name is too large")]
        public string Name { get; set; }
    }
}
