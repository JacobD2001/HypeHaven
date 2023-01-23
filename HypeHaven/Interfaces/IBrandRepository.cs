using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAll();
        Task<Brand> GetByIdAsync(int id);

        bool Add(Brand brand);
        bool Update(Brand brand);
        bool Delete(Brand brand);
        bool Save();
    }
}
