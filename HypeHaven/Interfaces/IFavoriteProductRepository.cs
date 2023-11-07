using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    /// <summary>
    /// Represents the interface for the favorite product repository.
    /// </summary>
    public interface IFavoriteProductRepository : IRepository<FavoriteProduct>
    {
        /// <summary>
        /// Determines whether the specified product is a favorite of the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>True if the product is a favorite of the user, false otherwise.</returns>
        bool IsFavorite(string userId, int productId);

        /// <summary>
        /// Retrieves a user's favorite product entry from the database.
        /// </summary>
        /// <param name="currentUserId">The user's ID for whom to retrieve the favorite product entry.</param>
        /// <param name="productId">The ID of the product to check if it's a favorite.</param>
        /// <returns>The user's favorite product entry if it exists, or null if not found.</returns>
        FavoriteProduct GetUserFavoriteProduct(string currentUserId, int productId);

        /// <summary>
        /// Gets the user's favorite products.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The user's favorite products.</returns>
        Task<IEnumerable<FavoriteProduct>> GetFavoriteProductsForSpecifedUser(string userId);
    }
}
