using DAL.Entities;
using DAL.Interfaces;
using DAL.Respositories;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICardContext _cardDbContext;
        private IRepository<Article> _ArticleRepo;
        private IRepository<Theme> _ThemeRepo;
        private IBaseRepository<ArticleRate> _ArticleRateRepo;
        public UnitOfWork(ICardContext cardDbContext)
        {
            _cardDbContext = cardDbContext;
        }

        public IRepository<Article> ArticleRepo
        {
            get
            {
                if (_ArticleRepo == null)
                    _ArticleRepo = new ArticleRepository(_cardDbContext);
                return _ArticleRepo;
            }
        }
        public IRepository<Theme> ThemeRepo
        {
            get
            {
                if (_ThemeRepo == null)
                    _ThemeRepo = new ThemeRepository(_cardDbContext);
                return _ThemeRepo;
            }
        }
        public IBaseRepository<ArticleRate> ArticleRateRepo
        {
            get
            {
                if (_ArticleRateRepo == null)
                    _ArticleRateRepo = new ArticleRateRepository(_cardDbContext);
                return _ArticleRateRepo;
            }
        }

        public int SaveChanges()
        {
            return _cardDbContext.SaveChanges();
        }
    }
}
