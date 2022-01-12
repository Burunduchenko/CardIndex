using DAL.Entities;
using DAL.Interfaces;
using DAL.Respositories;

namespace DAL
{
    /// <summary>
    /// The class is designed to use a 
    /// single database context in the program
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICardContext _cardDbContext;
        private IRepository<Card> _CardRepository;
        private IRepository<Theme> _ThemeRepository;
        private IBaseRepository<CardAssessment> _CardAssessmentRepository;
        public UnitOfWork(ICardContext cardDbContext)
        {
            _cardDbContext = cardDbContext;
        }

        public IRepository<Card> CardRepository
        {
            get
            {
                if (_CardRepository == null)
                    _CardRepository = new CardRepository(_cardDbContext);
                return _CardRepository;
            }
        }
        public IRepository<Theme> ThemeRepository
        {
            get
            {
                if (_ThemeRepository == null)
                    _ThemeRepository = new ThemeRepository(_cardDbContext);
                return _ThemeRepository;
            }
        }
        public IBaseRepository<CardAssessment> CardAssessmentRepository
        {
            get
            {
                if (_CardAssessmentRepository == null)
                    _CardAssessmentRepository = new CardAssessmentRepository(_cardDbContext);
                return _CardAssessmentRepository;
            }
        }

        public int SaveChanges()
        {
            return _cardDbContext.SaveChanges();
        }
    }
}
