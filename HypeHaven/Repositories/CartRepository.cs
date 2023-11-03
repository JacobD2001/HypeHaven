using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Add(Cart cart)
        {
            _context.Add(cart);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        //TODO - INCLUDE PROB
        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
