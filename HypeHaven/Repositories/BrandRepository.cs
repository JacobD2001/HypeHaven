using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization.Metadata;

namespace HypeHaven.Repositories
{
    /// <summary>
    /// Represents a repository for managing brands.
    /// </summary>
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor) : base(context) //calling constructor from base class(repository)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Brand>> GetAllForSpecifedUser()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            return await _context.Brands
                .Where(b => b.Id == currentUserId)                
                .ToListAsync();
        }
        public async Task<Brand> GetByIdAsync(int id)
        {
           return await _context.Brands  
          .Include(b => b.Products)
          .Include(b => b.Reviews)
          .FirstOrDefaultAsync(b => b.BrandId == id);
        }   

        public async Task<Brand> GetByIdAsyncNoTracking(int id)
        {
           return await _context.Brands
          .Include(b => b.Products)
          .Include(b => b.Reviews)
          .AsNoTracking().FirstOrDefaultAsync(b => b.BrandId == id);
        }

        public async Task<IEnumerable<Brand>> Search(string searchTerm)
        {
            return await _context.Brands
                .Where(b => b.Name.ToLower().Contains(searchTerm))
                .ToListAsync();
        }
    }
}

