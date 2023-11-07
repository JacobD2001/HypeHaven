using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using HypeHaven.Repositories;
using HypeHaven.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    /// <summary>
    /// Represents a  repository for managing products.
    /// </summary>
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public ProductRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor, IFavoriteProductRepository favoriteProductRepository) : base(context) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<IEnumerable<Product>> GetAllForSpecifedBrand(int id)
        {
            return await _context.Products
                .Where(p => p.BrandId == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsForSpecifedProduct(int ProductId)
        {
            return await _context.Reviews
                .Where(r => r.ProductId == ProductId)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.OrderItems)
                .Include(p => p.Orders)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.OrderItems)
                .Include(p => p.Orders)
                .Include(p => p.Reviews)
                .AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> Search(string searchTerm)
        {
            return await _context.Products
                .Where(p => p.Name.ToLower().Contains(searchTerm))
                .ToListAsync();
        }

      
        public async Task<IEnumerable<Product>> SortProductsByPrice(IEnumerable<Product> products, string sortOrder)
        {
            switch (sortOrder)
            {
                case "Price":
                    return products.OrderBy(p => p.Price);
                case "price_desc":
                    return products.OrderByDescending(p => p.Price);
                default:
                    return products;
            }
        }

        public async Task<IEnumerable<Product>> FilterProductsByCategory(IEnumerable<Product> products, int categoryId)
        {
            return products.Where(p => p.CategoryId == categoryId);
        }

        //implemented (from IRepository)
        public Task<IEnumerable<Product>> GetAllForSpecifedUser()
        {
            throw new NotImplementedException();
        }
    }
}

