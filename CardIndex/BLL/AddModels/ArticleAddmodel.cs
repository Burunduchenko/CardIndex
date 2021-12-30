using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    public class ArticleAddmodel
    {
        [Required(ErrorMessage = "You can't create article without title")]
        [MinLength(5)]
        [MaxLength(70)]
        public string Title { get; set; }
        [Required(ErrorMessage = "You can't create emtpy article")]
        [MinLength(50)]
        [MaxLength(5000)]
        public string Body { get; set; }
        [Required(ErrorMessage = "You can't create article without author")]
        [MinLength(8)]
        [MaxLength(100)]
        public string AuthorFullName { get; set; }
        public string ThemeName { get; set; }
    }
}
