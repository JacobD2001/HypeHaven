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
