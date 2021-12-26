using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBaseService <T>
    {
        Task<T> AddAsync(T item);
        Task Delete(int id);
        IEnumerable<T> GetAllWithDetails();
    }
}
