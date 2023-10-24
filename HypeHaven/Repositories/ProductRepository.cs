using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public ProductRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor, IFavoriteProductRepository favoriteProductRepository)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public bool Add(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool Delete(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Product product)
        {
            _context.Update(product);
            return Save();
        }


        public async Task<IEnumerable<Product>> Search(string searchTerm)
        {
            return await _context.Products
                .Where(p => p.Name.ToLower().Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<Product> AddToFavoritesAsync(int productId)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            // returns true if product is in favorite
            if (!_favoriteProductRepository.IsFavorite(currentUserId, productId))
            {
                // If not, create a FavoriteProduct object
                var favoriteProduct = new FavoriteProduct
                {
                    UserId = currentUserId,
                    ProductId = productId,
                };

                _context.FavoriteProducts.Add(favoriteProduct);
                await _context.SaveChangesAsync();
            }

            // set the IsFavorite property in the Product model 
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.IsFavorite = true; // Set the IsFavorite property to true
                await _context.SaveChangesAsync();
            }

            return product;
        }


        public async Task<Product> RemoveFromFavoritesAsync(int productId)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            // Check if the product is in the user's favorites
            if (_favoriteProductRepository.IsFavorite(currentUserId, productId))
            {
                // Remove the FavoriteProduct 
                var favoriteProduct = _context.FavoriteProducts.FirstOrDefault(fp => fp.UserId == currentUserId && fp.ProductId == productId);
                if (favoriteProduct != null)
                {
                    _context.FavoriteProducts.Remove(favoriteProduct);
                    await _context.SaveChangesAsync();
                }
            }

            //setting property to false
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.IsFavorite = false; 
                await _context.SaveChangesAsync();
            }

            return product;
        }

        public async Task<IEnumerable<Product>> GetFavoriteProducts(string userId)
        {
            return await _context.Products
                .Where(p => p.IsFavorite && p.UserId == userId)
                .ToListAsync();
        }



    }
}
