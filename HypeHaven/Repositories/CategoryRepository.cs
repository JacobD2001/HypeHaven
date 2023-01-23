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

        
    }
}
