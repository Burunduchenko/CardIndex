using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    /// <summary>
    /// A model for which contains fields and 
    /// validation, for the user to enter data about Article 
    /// </summary>
    public class ArticleAddmodel
    {
        [Required(ErrorMessage = "You can't create article without title")]
        [MinLength(5, ErrorMessage = "Title is too short")]
        [MaxLength(70, ErrorMessage = "Title is too large")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You can't create emtpy article")]
        [MinLength(50, ErrorMessage = "Article is too short")]
        [MaxLength(5000, ErrorMessage = "Article is too large")]
        public string Body { get; set; }
        [Required(ErrorMessage = "You can't create article without author")]
        [MaxLength(100, ErrorMessage = "Ypur name is too large")]
        public string AuthorFullName { get; set; }
        [Required(ErrorMessage = "You can't create articlke without name")]
        [MinLength(2, ErrorMessage = "The name of theme is too short")]
        [MaxLength(20, ErrorMessage = "The name of theme is too large")]
        public string ThemeName { get; set; }
    }
}
