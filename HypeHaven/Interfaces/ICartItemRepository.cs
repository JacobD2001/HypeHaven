using HypeHaven.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HypeHaven.Interfaces
{
    /// <summary>
    /// Represents the interface for the cart item repository.
    /// </summary>
    public interface ICartItemRepository : IRepository<CartItem>
    {
        /// <summary>
        /// Gets all cart items for the specified user.
        /// </summary>
        /// <returns>All cart items for the specified user.</returns>
        Task<IEnumerable<CartItem>> GetAllForSpecifedUser();

        /// <summary>
        /// Gets the cart item with the specified product ID.
        /// </summary>
        /// <param name="cartId">The ID of the cart.</param>
        /// <param name="productId">The ID of the product.</param>
        /// <returns>The cart item with the specified product ID.</returns>
        Task<CartItem> GetCartItemByProductId(int cartId, int productId);

        /// <summary>
        /// Gets the cart item with the specified ID.
        /// </summary>
        /// <param name="cartItemId">The ID of the cart item.</param>
        /// <returns>The cart item with the specified ID.</returns>
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);
    }
}
