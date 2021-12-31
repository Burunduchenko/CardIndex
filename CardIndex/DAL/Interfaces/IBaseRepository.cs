using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    /// <summary>
    /// The interface is designed to 
    /// describe the main functions of Theme 
    /// and Article Rate Repositories
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllWithDetailsAsync();
        Task<T> GetByIdWithDetaileAsync(int id);
        Task<T> AddAsync(T item);
        void DeleteById(int id);

    }
}
