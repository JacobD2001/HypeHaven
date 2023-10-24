using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    public interface IReviewRepository
    {
        //Task<IEnumerable<Review>> GetReviewsForSpecifedProduct(int ProductId);
        bool Add(Review review);
        bool Update(Review review);
        bool Delete(Review review);
        bool Save();
    }
}
