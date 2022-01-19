using BLL.AddModels;
using BLL.VievModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICardService
    {
        Task<IEnumerable<CardVievModel>> GetByThemeAsync(string themeName);
        Task<CardVievModel> GetByNameAsync(string name);
        Task<IEnumerable<CardVievModel>> GetByLengthAsync(int length);
        Task<IEnumerable<CardVievModel>> GetByRangeOfRateAsync(double max, double min);
        Task<CardVievModel> UpdateAsync(CardAddmodel item);
        Task<CardVievModel> AddAsync(CardAddmodel item);
        Task DeleteAsync(int id);
        Task<IEnumerable<CardVievModel>> GetAllWithDetailsAsync();
    }
}
