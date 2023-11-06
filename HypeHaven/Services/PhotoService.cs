using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HypeHaven.Helpers.Models;
using HypeHaven.Interfaces;
using Microsoft.Extensions.Options;

namespace HypeHaven.NewFolder
{
    /// <summary>
    /// Service for managing photos using Cloudinary.
    /// </summary>
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoService"/> class.
        /// </summary>
        /// <param name="config">The Cloudinary settings configuration.</param>
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(account);
        }

        /// <summary>
        /// Adds a photo asynchronously.
        /// </summary>
        /// <param name="file">The file to upload.</param>
        /// <returns>The image upload result.</returns>
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file != null && file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        /// <summary>
        /// Deletes a photo asynchronously.
        /// </summary>
        /// <param name="publicId">The public ID of the photo to delete.</param>
        /// <returns>The deletion result.</returns>
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}
