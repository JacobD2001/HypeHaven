using System.ComponentModel.DataAnnotations;

namespace HypeHaven.ViewModels.ReviewViewModels
{
    public class EditReviewViewModel
    {

        [Required(ErrorMessage = "Text is required.")]
        [StringLength(100, ErrorMessage = "Text cannot be longer than 100 characters.")]
        public string Text { get; set; } = null!;

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        public int BrandId { get; set; }
        public int ProductId { get; set; }

        public string Id { get; set; } = null!;
    }
}
