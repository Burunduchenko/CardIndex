using DAL.Entities;

namespace DAL.Interfaces
{
    /// <summary>
    /// The interface is designed to describe the class UnitOfWork
    /// </summary>
    public interface IUnitOfWork
    {
        IRepository<Article> ArticleRepo { get; }
        IRepository<Theme> ThemeRepo { get; }
        IBaseRepository<ArticleRate> ArticleRateRepo { get; }

        int SaveChanges();
    }
}
