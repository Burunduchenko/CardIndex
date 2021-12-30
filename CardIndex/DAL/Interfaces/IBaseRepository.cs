using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllWithDetailsAsync();
        Task<T> GetByIdWithDetaileAsync(int id);
        Task<T> AddAsync(T item);
        void DeleteById(int id);

    }
}
