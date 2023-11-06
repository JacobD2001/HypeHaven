using HypeHaven.ViewModels.ProductViewModels;
using HypeHaven.ViewModels.ReviewViewModels;

namespace HypeHaven.ViewModels.Helpers
{
    /// <summary>
    /// Composite View model for merging AddReviewViewModel and ProductDetailViewModel.
    /// </summary>
    public class CompositeViewModel
    {
        public AddReviewViewModel AddReviewViewModel { get; set; } = null!;
        public ProductDetailViewModel ProductDetailViewModel { get; set; } = null!;
        public IHttpContextAccessor HttpContextAccessor { get; set; }
    }
}
