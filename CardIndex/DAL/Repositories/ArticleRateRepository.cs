﻿using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Respositories
{
    public class ArticleRateRepository : IRepository<ArticleRate>
    {

        private readonly ICardContext _cardDbContext;
        private readonly DbSet<ArticleRate> _articlesRates;

        public ArticleRateRepository(ICardContext cardDbContext)
        {
            _cardDbContext = cardDbContext;
            _articlesRates = _cardDbContext.Set<ArticleRate>();
        }

        public async Task<ArticleRate> AddAsync(ArticleRate item)
        {
            await _articlesRates.AddAsync(item);
            _cardDbContext.SaveChanges();
            return item;
        }

        public void DeleteById(int id)
        {
            var result = _articlesRates.Find(id);
            _articlesRates.Remove(result);
            _cardDbContext.SaveChanges();
        }

        public IEnumerable<ArticleRate> GetAll()
        {
            var articleRates = _articlesRates;
            return articleRates;
        }

        public IEnumerable<ArticleRate> GetAllWithDetails()
        {
            var articlerates = _articlesRates.Include(x => x.Article).Include(x => x.User);
            return articlerates;
        }

        public async Task<ArticleRate> GetByIdAsync(int id)
        {
            var articleRate = await _articlesRates.FindAsync(id);
            return articleRate;
        }

        public async Task<ArticleRate> GetByIdWithDetaileAsync(int id)
        {
            var articleRates = _articlesRates.Include(x => x.Article).Include(x => x.User);
            var result = await articleRates.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public ArticleRate Update(ArticleRate item)
        {
            _articlesRates.Update(item);
            _cardDbContext.SaveChanges();
            return item;
        }
    }
}