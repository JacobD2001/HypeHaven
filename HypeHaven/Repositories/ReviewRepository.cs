using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.EntityFrameworkCore;

namespace HypeHaven.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly HypeHavenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewRepository(HypeHavenContext context, IHttpContextAccessor httpContextAccessor) :base(context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

      /*  public bool Add(Review review)
        {
            _context.Add(review);
            return Save();
        }

        public bool Delete(Review review)
        {
            _context.Remove(review);
            return Save();
        }*/


        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews
        .Include(r => r.Brand)
        .Include(r => r.Product)
        .FirstOrDefaultAsync(r => r.ReviewId == id);
        }

        /*   public async Task<IEnumerable<Review>> GetReviewsForSpecifedProduct(int ProductId)
           {
               return await _context.Reviews
                   .Where(r => r.ProductId == ProductId)
                   .ToListAsync();
           }*/

        //Implemented(from IRepository)
        #region NotImplemented

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Review review)
        {
            _context.Update(review);
            return Save();
        }


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
