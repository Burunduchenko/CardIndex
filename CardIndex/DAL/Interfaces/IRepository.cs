using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
