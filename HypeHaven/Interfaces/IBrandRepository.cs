using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    public interface IBrandRepository
    {
        Task<IEnumerable<Brand>> GetAll();
        Task<IEnumerable<Brand>> GetAllForSpecifedUser();
        Task<Brand> GetByIdAsync(int id);
        Task<Brand> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Brand>> Search(string searchTerm);


        bool Add(Brand brand);
        bool Update(Brand brand);
        bool Delete(Brand brand);
        bool Save();
    }
}
