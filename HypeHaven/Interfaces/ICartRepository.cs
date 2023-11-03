using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
        bool Add(Cart cart);
        bool Save();
    }
}
