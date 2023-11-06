using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    /// <summary>
    /// Represents a  repository for managing favorite products.
    /// </summary>
    public class FavoriteProductRepository : IFavoriteProductRepository
    {
        private readonly HypeHavenContext _context;

        public FavoriteProductRepository(HypeHavenContext context)
        {
            _context = context;
        }

        //returns true if the product is in the user's favorites
        public bool IsFavorite(string userId, int productId)
        {
            return _context.FavoriteProducts.Any(fp => fp.UserId == userId && fp.ProductId == productId);
        }
    }
}
