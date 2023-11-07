using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    /// <summary>
    /// Represents the interface for the product repository.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Gets all products for the specified brand.
        /// </summary>
        /// <param name="id">The ID of the brand.</param>
        /// <returns>All products for the specified brand.</returns>
        Task<IEnumerable<Product>> GetAllForSpecifedBrand(int id);

        /// <summary>
        /// Searches for products that match the specified search term.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns>The matching products.</returns>
        Task<IEnumerable<Product>> Search(string searchTerm);

       
        /// <summary>
        /// Gets the reviews for the specified product.
        /// </summary>
        /// <param name="ProductId">The ID of the product.</param>
        /// <returns>The reviews for the specified product.</returns>
        Task<IEnumerable<Review>> GetReviewsForSpecifedProduct(int ProductId);

        /// <summary>
        /// Sorts the specified products by price.
        /// </summary>
        /// <param name="products">The products to sort.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <returns>The sorted products.</returns>
        Task<IEnumerable<Product>> SortProductsByPrice(IEnumerable<Product> products, string sortOrder);

        /// <summary>
        /// Filters the specified products by category.
        /// </summary>
        /// <param name="products">The products to filter.</param>
        /// <param name="categoryId">The ID of the category.</param>
        /// <returns>The filtered products.</returns>
        Task<IEnumerable<Product>> FilterProductsByCategory(IEnumerable<Product> products, int categoryId);
    }
}
