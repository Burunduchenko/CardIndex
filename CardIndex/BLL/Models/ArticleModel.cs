using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ArticleModel
    {
        public int Id { get; set; }
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
        public DateTime Created { get; set; }
        public double AvgRate { get; set; }

        public virtual int ThemeId { get; set; }
        public virtual ICollection<int> ArticleRatesIds { get; set; }
    }
}
