using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    /// <summary>
    /// A model for which contains fields and 
    /// validation, for the user to enter data about Article Rate
    /// </summary>
    public class ArticleRateAddModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Rate field is Required")]
        public int Rate { get; set; }
        [MinLength(10, ErrorMessage = "Comment is too short")]
        [MaxLength(500, ErrorMessage = "Comment is too large")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "You did not specify an article title")]
        [MinLength(5, ErrorMessage = "Title is too short")]
        [MaxLength(70, ErrorMessage = "Title is too large")]
        public string ArticleTitle { get; set; }
        [Required(ErrorMessage = "Login is required")]
        [MinLength(4, ErrorMessage = "Provided ogin is too short")]
        [MaxLength(20, ErrorMessage = "Provided is too large")]
        public string UserLogin { get; set; }
    }
}
