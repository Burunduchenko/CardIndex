using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Respositories
{
    public class ArticleRateRepository : IBaseRepository<ArticleRate>
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


        public async Task<IEnumerable<ArticleRate>> GetAllWithDetails()
        {
            var articlerates = await _articlesRates.Include(x => x.Article).ToListAsync();
            return articlerates;
        }

        public async Task<ArticleRate> GetByIdWithDetaileAsync(int id)
        {
            var articleRates = _articlesRates.Include(x => x.Article);
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
