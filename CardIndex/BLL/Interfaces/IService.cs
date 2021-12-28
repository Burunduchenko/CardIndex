using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IService<T> : IBaseService<T> where T : class
    {
        Task<T> GetByIdWithDetailsAsync(int id);
        Task<T> Update(T item);

    }
}

