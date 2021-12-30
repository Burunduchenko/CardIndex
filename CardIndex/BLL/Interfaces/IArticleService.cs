using BLL.AddModels;
using BLL.VievModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IArticleService /*: IBaseService<ArticleAddmodel, ArticelVievModel>*/
    {
        Task<IEnumerable<ArticelVievModel>> GetByTheme(string theme);
        Task<ArticelVievModel> GetByName(string name);
        Task<IEnumerable<ArticelVievModel>> GetByLength(int length);
        Task<IEnumerable<ArticelVievModel>> GetByRangeOfRate(double max, double min);
        Task<ArticelVievModel> GetByIdWithDetailsAsync(int id);
        Task<ArticelVievModel> Update(ArticleAddmodel item);
        Task<ArticelVievModel> AddAsync(ArticleAddmodel item);
        Task Delete(int id);
        Task<IEnumerable<ArticelVievModel>> GetAllWithDetails();
    }
}
