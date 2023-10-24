using System.ComponentModel.DataAnnotations;

namespace HypeHaven.ViewModels.ReviewViewModels
{
    public class DeleteReviewViewModel
    {
        public string Text { get; set; } = null!;

        public int Rating { get; set; }

        public int BrandId { get; set; }
        public int ProductId { get; set; }

        public string Id { get; set; } = null!;
    }
}
