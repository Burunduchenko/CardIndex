using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithDetaileAsync(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithDetails();
        Task<T> AddAsync(T item);
        T Update(T item);
        void DeleteById(int id);
    }
}
