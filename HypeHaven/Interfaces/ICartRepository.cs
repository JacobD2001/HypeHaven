using HypeHaven.models;
using System.Threading.Tasks;

namespace HypeHaven.Interfaces
{
    /// <summary>
    /// Represents the interface for the cart repository.
    /// </summary>
    public interface ICartRepository : IRepository<Cart>
    {
        /// <summary>
        /// Gets the cart for the specified user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The cart for the specified user.</returns>
        Task<Cart> GetCartByUserIdAsync(string userId);
    }
}
