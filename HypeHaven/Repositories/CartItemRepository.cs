using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartItemRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Add(CartItem cartItem)
        {
            _context.Add(cartItem);
            return Save();
        }

        public bool Delete(CartItem cartItem)
        {
            _context.Remove(cartItem);
            return Save();
        }

        public async Task<IEnumerable<CartItem>> GetAllForSpecifedUser()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            return await _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.Cart.UserId == currentUserId) // Filter by user ID from the Cart model
            .ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();  
            return saved > 0 ? true : false; 
        }

        public bool Update(CartItem cartItem)
        {
            _context.Update(cartItem);
            return Save();
        }
    }
}
