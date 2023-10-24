using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetAllForSpecifedBrand(int id);

        Task<Product> GetByIdAsync(int id);
        Task<Product> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Product>> Search(string searchTerm);
        Task<Product> AddToFavoritesAsync(int productId);
        Task<Product> RemoveFromFavoritesAsync(int productId);
        Task<IEnumerable<Product>> GetFavoriteProducts(string userId);
        Task<IEnumerable<Review>> GetReviewsForSpecifedProduct(int ProductId);



        bool Add(Product product);
        bool Update(Product product);
        bool Delete(Product product);
        bool Save();
    }
}
