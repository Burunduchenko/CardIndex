using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBaseRepository <T>
    {
        Task<IEnumerable<T>> GetAllWithDetails();
        Task<T> GetByIdWithDetaileAsync(int id);
        Task<T> AddAsync(T item);
        void DeleteById(int id);

    }
}
