using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    /// <summary>
    /// Represents a repository for managing cartItems.
    /// </summary>
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartItemRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<CartItem>> GetAllForSpecifedUser()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            return await _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => ci.Cart.UserId == currentUserId) // Filter by user ID from the Cart model
            .ToListAsync();
        }

        public async Task<CartItem> GetCartItemByProductId(int cartId, int productId)
        {
            return await _context.CartItems
                .Include(ci => ci.Cart) // Include the related Cart for reference
                .Where(ci => ci.Cart.CartId == cartId && ci.ProductId == productId)
                .FirstOrDefaultAsync();
        }
        public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);
        }
    }
}
