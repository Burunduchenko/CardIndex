using System.ComponentModel.DataAnnotations;

namespace BLL.AddModels
{
    public class ArticleRateAddModel
    {
        [Required(ErrorMessage = "Rate field is Required")]
        public int Rate { get; set; }
        [MinLength(10, ErrorMessage = "Comment is too short")]
        [MaxLength(500, ErrorMessage = "Comment is too large")]
        public string Comment { get; set; }
        public string ArticleName { get; set; }
        public string UserLogin { get; set; }
    }
}
