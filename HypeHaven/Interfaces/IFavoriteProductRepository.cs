using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    public interface IFavoriteProductRepository
    {
       // Task<IEnumerable<FavoriteProduct>> GetAll();

        bool IsFavorite(string userId, int productId);
    }
}
