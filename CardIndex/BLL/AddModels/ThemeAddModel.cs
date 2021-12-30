using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    public class ThemeAddModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Theme name is too large")]
        public string Name { get; set; }
    }
}
