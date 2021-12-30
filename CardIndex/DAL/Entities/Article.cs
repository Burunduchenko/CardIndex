using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string AuthorFullName { get; set; }
        public DateTime Created { get; set; }
        [ForeignKey(nameof(Theme))]
        public int ThemeId { get; set; }

        public virtual Theme Theme { get; set; }
        public virtual ICollection<ArticleRate> ArticleRates { get; set; }

    }
}
