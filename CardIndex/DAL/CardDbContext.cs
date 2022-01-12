using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class CardDbContext : DbContext, ICardContext
    {

        public CardDbContext(DbContextOptions<CardDbContext> options) : base(options)
        {

        }

        public CardDbContext()
        {
        }

        public virtual DbSet<Card> cards { get; set; }
        public virtual DbSet<CardAssessment> cardAssessments { get; set; }
        public virtual DbSet<Theme> themes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}

