using CloudinaryDotNet.Actions;

namespace HypeHaven.Interfaces
{
    /// <summary>
    /// Represents the interface for the photo service.
    /// </summary>
    public interface IPhotoService
    {
        /// <summary>
        /// Adds a photo asynchronously.
        /// </summary>
        /// <param name="file">The file to add.</param>
        /// <returns>The result of the image upload.</returns>
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        /// <summary>
        /// Deletes a photo asynchronously.
        /// </summary>
        /// <param name="publicId">The public ID of the photo to delete.</param>
        /// <returns>The result of the deletion.</returns>
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}
