using System.Collections.Generic;
using System.Threading.Tasks;

namespace HypeHaven.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllForSpecifedUser();
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsyncNoTracking(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();
    }
}
