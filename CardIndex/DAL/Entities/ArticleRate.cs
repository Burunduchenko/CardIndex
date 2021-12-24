using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ArticleRate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Rate field is Required")]
        public int Rate { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey(nameof(Article))]
        public int ArticleId { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public virtual Article Article { get; set; }
        public virtual User User { get; set; }
    }
}
