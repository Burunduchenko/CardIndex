using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="You can't create article without title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You can't create emtpy article")]
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Theme> Themes { get; set; }
        public virtual ICollection<ArticleRate>  ArticleRates { get; set; }

    }
}
