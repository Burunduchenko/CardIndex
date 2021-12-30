using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBaseService<Add, Viev> where Add : class
    {
        Task<Viev> AddAsync(Add item);
        Task DeleteAsync(int id);
        Task<IEnumerable<Viev>> GetAllWithDetailsAsync();

    }
}
