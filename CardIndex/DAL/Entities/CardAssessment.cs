using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CardAssessment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Rate { get; set; }
        public string Comment { get; set; }

        public DateTime Date { get; set; }
        [ForeignKey(nameof(Card))]
        public int CardId { get; set; }
        public string UserId { get; set; }

        public virtual Card Card { get; set; }
    }
}
