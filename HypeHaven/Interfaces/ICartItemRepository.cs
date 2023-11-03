using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    public interface ICartItemRepository
    {
        Task<IEnumerable<CartItem>> GetAllForSpecifedUser();
        bool Add(CartItem cartItem);
        bool Update(CartItem cartItem);
        bool Delete(CartItem cartItem);
        bool Save();
        Task<CartItem> GetCartItemByProductId(int cartId, int productId);
        Task<CartItem> GetCartItemByIdAsync(int cartItemId);

    }
}
