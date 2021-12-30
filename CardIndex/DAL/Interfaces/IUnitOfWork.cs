using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Article> ArticleRepo { get; }
        IRepository<Theme> ThemeRepo { get; }
        IBaseRepository<ArticleRate> ArticleRateRepo { get; }

        int SaveChanges();
    }
}
