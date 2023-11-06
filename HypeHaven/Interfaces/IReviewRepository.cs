using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    /// <summary>
    /// Represents the interface for the review repository.
    /// </summary>
    public interface IReviewRepository : IRepository<Review>
    {
        /// <summary>
        /// Gets the review with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the review.</param>
        /// <returns>The review with the specified ID.</returns>
        Task<Review> GetReviewByIdAsync(int id);
    }
}
