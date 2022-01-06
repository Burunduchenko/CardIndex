using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    /// <summary>
    /// A model for which contains fields and 
    /// validation, for the user to enter data about Theme
    /// </summary>
    public class ThemeAddModel
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Theme name is too short")]
        [MaxLength(20, ErrorMessage = "Theme name is too large")]
        public string Name { get; set; }
    }
}
