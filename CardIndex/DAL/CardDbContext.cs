using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

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

        public virtual DbSet<Article> articles { get; set; }
        public virtual DbSet<ArticleRate> articleRate { get; set; }
        public virtual DbSet<Theme> theme { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}

