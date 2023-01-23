using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAll();

    }
}
