using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Respositories
{
    /// <summary>
    /// A class that is designed to interact with the Entity Article 
    /// </summary>
    public class ArticleRepository : IRepository<Article>
    {
        private readonly ICardContext _cardDbContext;
        private readonly DbSet<Article> _articles;

        public ArticleRepository(ICardContext cardDbContext)
        {
            _cardDbContext = cardDbContext;
            _articles = _cardDbContext.Set<Article>();
        }

        public async Task<Article> AddAsync(Article item)
        {
            await _articles.AddAsync(item);
            _cardDbContext.SaveChanges();
            return item;
        }

        public void DeleteById(int id)
        {
            var result = _articles.Find(id);
            _articles.Remove(result);
            _cardDbContext.SaveChanges();
        }

        public IEnumerable<Article> GetAll()
        {
            var articles = _articles;
            return articles;
        }

        public async Task<IEnumerable<Article>> GetAllWithDetailsAsync()
        {
            var articles = await _articles.Include(x => x.ArticleRates).ToListAsync();
            return articles;
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            var article = await _articles.FindAsync(id);
            return article;
        }

        public async Task<Article> GetByIdWithDetaileAsync(int id)
        {
            var articles = _articles.Include(x => x.ArticleRates);
            var resultArticle = await articles.FirstOrDefaultAsync(x => x.Id == id);
            return resultArticle;
        }

        public Article Update(Article item)
        {
            var res = _articles.Find(item.Id);
            res.Title = item.Title;
            res.Body = item.Body;
            res.ThemeId = item.ThemeId;
            _cardDbContext.SaveChanges();
            return item;
        }
    }
}
