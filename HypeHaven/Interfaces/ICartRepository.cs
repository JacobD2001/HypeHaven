using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetCartByUserIdAsync(string userId);
       /* bool Add(Cart cart);
        bool Save();*/
    }
}
