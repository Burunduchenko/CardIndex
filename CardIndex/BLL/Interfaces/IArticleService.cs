using BLL.AddModels;
using BLL.VievModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticelVievModel>> GetByThemeAsync(string theme);
        Task<ArticelVievModel> GetByNameAsync(string name);
        Task<IEnumerable<ArticelVievModel>> GetByLengthAsync(int length);
        Task<IEnumerable<ArticelVievModel>> GetByRangeOfRateAsync(double max, double min);
        Task<ArticelVievModel> GetByIdWithDetailsAsync(int id);
        Task<ArticelVievModel> UpdateAsync(ArticleAddmodel item);
        Task<ArticelVievModel> AddAsync(ArticleAddmodel item);
        Task DeleteAsync(int id);
        Task<IEnumerable<ArticelVievModel>> GetAllWithDetailsAsync();
    }
}
