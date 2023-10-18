namespace HypeHaven.Interfaces
{
    public interface IFavoriteProductRepository
    {
        bool IsFavorite(string userId, int productId);
    }
}
