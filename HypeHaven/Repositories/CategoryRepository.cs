using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly HypeHavenContext _context;

        public CategoryRepository(HypeHavenContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        //Implemented(from IRepository)
        #region NotImplemented       
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        }  
        public async Task<Category> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
        }


        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllForSpecifedUser()
        {
            throw new NotImplementedException();
        }

        public bool Add(Category entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Category entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
