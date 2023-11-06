using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    /// <summary>
    /// Represents a repository for managing carts.
    /// </summary>
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        //Implemented(from IRepository)
        #region NotImplemented
        public Task<IEnumerable<Cart>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cart>> GetAllForSpecifedUser()
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Cart> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Cart entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Cart entity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
