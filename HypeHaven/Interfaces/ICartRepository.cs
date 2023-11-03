using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetAllForSpecifedUser();
        bool Add(CartItem cartItem);
        bool Update(CartItem cartItem);
        bool Delete(CartItem cartItem);
        bool Save();

    }
}
