using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
