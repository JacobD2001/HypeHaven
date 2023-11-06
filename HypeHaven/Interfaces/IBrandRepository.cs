using HypeHaven.models;

namespace HypeHaven.Interfaces
{
    /// <summary>
    /// Represents the interface for the brand repository.
    /// </summary>
    public interface IBrandRepository : IRepository<Brand>
    {
        /// <summary>
        /// Searches for brands that match the specified search term.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns>The matching brands.</returns>
        Task<IEnumerable<Brand>> Search(string searchTerm);
    }
}
