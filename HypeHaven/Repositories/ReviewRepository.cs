using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    /// <summary>
    /// Represents a repository for managing reviews.
    /// </summary>
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor) :base(context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
        .Include(r => r.Brand)
        .Include(r => r.Product)
        .FirstOrDefaultAsync(r => r.ReviewId == id);
        }

        //NotImplemented(from IRepository)
        #region NotImplemented
        public Task<IEnumerable<Review>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetAllForSpecifedUser()
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetByIdAsyncNoTracking(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
