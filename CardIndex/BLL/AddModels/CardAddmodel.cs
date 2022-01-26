using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    /// <summary>
    /// A model for which contains fields and 
    /// validation, for the user to enter data about Article 
    /// </summary>
    public class CardAddmodel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You can't create card without title")]
        [MinLength(5, ErrorMessage = "Title is too short")]
        [MaxLength(70, ErrorMessage = "Title is too large")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You can't create emtpy card")]
        [MinLength(50, ErrorMessage = "Article is too short")]
        [MaxLength(5000, ErrorMessage = "Article is too large")]
        public string Body { get; set; }
        [Required(ErrorMessage = "You can't create card without author")]
        [MaxLength(100, ErrorMessage = "Ypur name is too large")]
        public string AuthorFullName { get; set; }
        [Required(ErrorMessage = "You can't create card without name")]
        [MinLength(2, ErrorMessage = "The name of theme is too short")]
        [MaxLength(20, ErrorMessage = "The name of theme is too large")]
        public string ThemeName { get; set; }

        public Theme theme { get; set; }
    }
}
