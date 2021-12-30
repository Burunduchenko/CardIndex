using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBaseService<Add, Viev> where Add : class
    {
        Task<Viev> AddAsync(Add item);
        Task Delete(int id);
        Task<IEnumerable<Viev>> GetAllWithDetails();

    }
}
