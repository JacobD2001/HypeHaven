using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly HypeHavenContext _context;

        public BrandRepository(HypeHavenContext context)
        {
            _context = context;
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
        //todo - implement getbyidasync
        public async Task<Brand> GetByIdAsync(int id)
        {
           return await _context.Brands
          .Include(b => b.CategoryId)
          .Where(c => c.CategoryId == id)
          .Include(b => b.Products)
          .Include(b => b.Reviews)
          .Include(b => b.Id)
          .SingleOrDefaultAsync(b => b.BrandId == id);
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

