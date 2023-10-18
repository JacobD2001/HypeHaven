using HypeHaven.Interfaces;
using HypeHaven.models;

namespace HypeHaven.Repositories
{
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
