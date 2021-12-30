using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> : IBaseRepository<T>
    {

        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        T Update(T item);
    }
}
