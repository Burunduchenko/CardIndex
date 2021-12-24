using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ArticleRateModel
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public DateTime Date { get; set; }
        public int ArticleId { get; set; }
        public int UserId { get; set; }
    }
}
