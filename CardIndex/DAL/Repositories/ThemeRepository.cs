using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Respositories
{
    /// <summary>
    /// A class that is designed to interact with the Entity Theme
    /// </summary>
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

        public async Task<IEnumerable<Theme>> GetAllWithDetailsAsync()
        {
            var themes = await _themes.Include(x => x.Articles).ToListAsync();
            return themes;
        }

        public async Task<Theme> GetByIdAsync(int id)
        {
            var theme = await _themes.FindAsync(id);
            return theme;
        }

        public async Task<Theme> GetByIdWithDetaileAsync(int id)
        {
            var themes = _themes.Include(x => x.Articles);
            var theme = await themes.FirstOrDefaultAsync(x => x.Id == id);
            return theme;
        }

        public Theme Update(Theme item)
        {
            var elem = _themes.Find(item.Id);
            elem.Name = item.Name;
            _cardDbContext.SaveChanges();
            return item;
        }
    }
}
