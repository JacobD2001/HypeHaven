using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Add(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool Delete(Review review)
        {
            _context.Remove(review);
            return Save();
        }

     /*   public async Task<IEnumerable<Review>> GetReviewsForSpecifedProduct(int ProductId)
        {
            return await _context.Reviews
                .Where(r => r.ProductId == ProductId)
                .ToListAsync();
        }*/

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true: false;
        }

        public bool Update(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
