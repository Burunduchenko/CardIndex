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
    public class CardRepository : IRepository<Card>
    {
        private readonly ICardContext _cardDbContext;
        private readonly DbSet<Card> _articles;

        public CardRepository(ICardContext cardDbContext)
        {
            _cardDbContext = cardDbContext;
            _articles = _cardDbContext.Set<Card>();
        }

        public async Task<Card> AddAsync(Card item)
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

        public IEnumerable<Card> GetAll()
        {
            var articles = _articles;
            return articles;
        }

        public async Task<IEnumerable<Card>> GetAllWithDetailsAsync()
        {
            var articles = await _articles.Include(x => x.CardAssessments).ToListAsync();
            return articles;
        }

        public async Task<Card> GetByIdAsync(int id)
        {
            var article = await _articles.FindAsync(id);
            return article;
        }

        public async Task<Card> GetByIdWithDetaileAsync(int id)
        {
            var articles = _articles.Include(x => x.CardAssessments);
            var resultArticle = await articles.FirstOrDefaultAsync(x => x.Id == id);
            return resultArticle;
        }

        public Card Update(Card item)
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
