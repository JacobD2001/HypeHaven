using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization.Metadata;

namespace HypeHaven.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BrandRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Add(Brand brand)
        {
            _context.Add(brand);
            return Save();
        }

        public bool Delete(Brand brand)
        {
            _context.Remove(brand);
            return Save();
        }

        public async Task<IEnumerable<Brand>> GetAll()
        {
            return await _context.Brands.ToListAsync();
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

        public bool Save()
        {
            var saved = _context.SaveChanges(); //savechanges returns int 
            return saved > 0 ? true : false; //if saved > 0 return true, else return false 
        }

        public bool Update(Brand brand)
        {
            _context.Update(brand);
            return Save();
        }
    }

}

