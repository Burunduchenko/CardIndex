using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace DAL
{
    public class CardDbContext : DbContext, ICardContext
    {

        public CardDbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Article> articles { get; set; }
        public virtual DbSet<ArticleRate> articleRate { get; set; }
        public virtual DbSet<Theme> theme { get; set; }
        public virtual DbSet<User> users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["CardDB"].ConnectionString);
        //}
    }
}

