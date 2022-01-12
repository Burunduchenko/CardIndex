using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Respositories
{
    /// <summary>
    /// A class that is designed to interact with the Entity Article Rate
    /// </summary>
    public class CardAssessmentRepository : IBaseRepository<CardAssessment>
    {

        private readonly ICardContext _cardDbContext;
        private readonly DbSet<CardAssessment> _articlesRates;

        public CardAssessmentRepository(ICardContext cardDbContext)
        {
            _cardDbContext = cardDbContext;
            _articlesRates = _cardDbContext.Set<CardAssessment>();
        }

        public async Task<CardAssessment> AddAsync(CardAssessment item)
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


        public async Task<IEnumerable<CardAssessment>> GetAllWithDetailsAsync()
        {
            var articlerates = await _articlesRates.Include(x => x.Card).ToListAsync();
            return articlerates;
        }

        public async Task<CardAssessment> GetByIdWithDetaileAsync(int id)
        {
            var articleRates = _articlesRates.Include(x => x.Card);
            var result = await articleRates.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public CardAssessment Update(CardAssessment item)
        {
            _articlesRates.Update(item);
            _cardDbContext.SaveChanges();
            return item;
        }
    }
}
