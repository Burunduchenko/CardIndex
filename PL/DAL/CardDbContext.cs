using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CardDbContext : DbContext, ICardContext
    {

        public CardDbContext(DbContextOptions<CardDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<Article> articles { get; set; }
        public virtual DbSet<ArticleRate> articleRate { get; set; }
        public virtual DbSet<Theme> theme { get; set; }
        public virtual DbSet<User> users { get; set; }

    }
}
