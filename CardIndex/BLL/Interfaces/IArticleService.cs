using BLL.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IArticleService : IService<ArticleModel>
    {
        IEnumerable<ArticleModel> GetByTheme(string theme);
        IEnumerable<ArticleModel> GetByName(string name);
        IEnumerable<ArticleModel> GetByLength(int length);
        IEnumerable<ArticleModel> GetByRangeOfRate(double max, double min);

    }
}
