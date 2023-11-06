using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    /// <summary>
    /// Represents the interface for the favorite product repository.
    /// </summary>
    public interface IFavoriteProductRepository
    {
        /// <summary>
        /// Determines whether the specified product is a favorite of the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>True if the product is a favorite of the user, false otherwise.</returns>
        bool IsFavorite(string userId, int productId);
    }
}
