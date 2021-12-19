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
    public class ThemeRepository : IRepository<Theme>
    {
        private readonly ICardContext _cardDbContext;
        private readonly DbSet<Theme> _themes;

        public ThemeRepository(ICardContext cardDbContext)
        {
            _cardDbContext = cardDbContext;
            _themes = _cardDbContext.Set<Theme>();    
        }

        public async Task<Theme> AddAsync(Theme item)
        {
            await _themes.AddAsync(item);
            _cardDbContext.SaveChanges();
            return item;
        }

        public void DeleteById(int id)
        {
            var result = _themes.Find(id);
             _themes.Remove(result);
            _cardDbContext.SaveChanges();
        }

        public IEnumerable<Theme> GetAll()
        {
            var themes = _themes;
            return themes;
        }

        public IEnumerable<Theme> GetAllWithDetails()
        {
            var themes = _themes.Include(x => x.Article);
            return themes;
        }

        public async Task<Theme> GetByIdAsync(int id)
        {
            var theme = await _themes.FindAsync(id);
            return theme;
        }

        public async Task<Theme> GetByIdWithDetaileAsync(int id)
        {
            var themes = _themes.Include(x => x.Article);
            var theme = await themes.FirstOrDefaultAsync(x => x.Id == id);
            return theme;
        }

        public Theme Update(Theme item)
        {
            _themes.Update(item);
            _cardDbContext.SaveChanges();
            return item;
        }
    }
}
