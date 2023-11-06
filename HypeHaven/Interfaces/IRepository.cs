using System.Collections.Generic;
using System.Threading.Tasks;

namespace HypeHaven.Interfaces
{
    /// <summary>
    /// Represents the interface for the repository.
    /// </summary>
    /// <typeparam name="T">The type of entity.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>All entities.</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Gets all entities for the specified user.
        /// </summary>
        /// <returns>All entities for the specified user.</returns>
        Task<IEnumerable<T>> GetAllForSpecifedUser();

        /// <summary>
        /// Gets the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>The entity with the specified ID.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Gets the entity with the specified ID without tracking changes.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <returns>The entity with the specified ID without tracking changes.</returns>
        Task<T> GetByIdAsyncNoTracking(int id);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>True if the entity was added successfully, false otherwise.</returns>
        bool Add(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>True if the entity was updated successfully, false otherwise.</returns>
        bool Update(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>True if the entity was deleted successfully, false otherwise.</returns>
        bool Delete(T entity);

        /// <summary>
        /// Saves changes to the repository.
        /// </summary>
        /// <returns>True if changes were saved successfully, false otherwise.</returns>
        bool Save();
    }
}
