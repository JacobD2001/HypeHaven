using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    /// <summary>
    /// Represents a  repository for managing favorite products.
    /// </summary>
    public class FavoriteProductRepository : Repository<FavoriteProduct>, IFavoriteProductRepository
    {
        private readonly HypeHavenContext _context;

        public FavoriteProductRepository(HypeHavenContext context) : base(context)
        {
            _context = context;
        }

        //returns true if the product is in the user's favorites
        public bool IsFavorite(string userId, int productId)
        {
            return _context.FavoriteProducts.Any(fp => fp.UserId == userId && fp.ProductId == productId);
        }

        public FavoriteProduct GetUserFavoriteProduct(string currentUserId, int productId)
        {
            // Use Entity Framework to find the UserFavoriteProduct entry
            return _context.FavoriteProducts
                .FirstOrDefault(fp => fp.UserId == currentUserId && fp.ProductId == productId);
        }

        public async Task<IEnumerable<FavoriteProduct>> GetFavoriteProductsForSpecifedUser(string userId)
        {
            return await _context.FavoriteProducts
                .Include(fp => fp.Product)
                .Where(p => p.IsFavorite && p.UserId == userId)
                .ToListAsync();
        }

        //not implemented(IRepository)
        #region not implemented
    
        public Task<IEnumerable<FavoriteProduct>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FavoriteProduct>> GetAllForSpecifedUser()
        {
            throw new NotImplementedException();
        }

        public Task<FavoriteProduct> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FavoriteProduct> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }

     

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool Update(FavoriteProduct entity)
        {
            throw new NotImplementedException();
        }

      
        #endregion
    }
}
