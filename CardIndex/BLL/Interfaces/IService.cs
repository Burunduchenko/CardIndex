using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IService<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithDetailsAsync(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithDetails();
        Task<T> AddAsync(T item);
        Task<T> Update(T item);
        void Delete(int id);

    }
}

