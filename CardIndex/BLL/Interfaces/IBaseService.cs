using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    /// <summary>
    /// The interface is designed 
    /// for basic functions of Article Rate Service and Theme Service
    /// </summary>
    /// <typeparam name="Add">Model for user add</typeparam>
    /// <typeparam name="Viev">Model for user viev</typeparam>
    public interface IBaseService<Add, Viev> where Add : class
    {
        Task<Viev> AddAsync(Add item);
        Task DeleteAsync(int id);
        Task<IEnumerable<Viev>> GetAllWithDetailsAsync();

    }
}
