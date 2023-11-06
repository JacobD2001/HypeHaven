using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    //this interface is used to abstract the implementation of getall method which is used to retrive all categores from database
    public interface ICategoryRepository : IRepository<Category>
    {
        //TODO - delete this interface completly(di?)

        //Task<IEnumerable<Category>> GetAll();
        //Task<Category> GetByIdAsync(int id);
        //Task<Category> GetByIdAsyncNoTracking(int id);


    }
}
