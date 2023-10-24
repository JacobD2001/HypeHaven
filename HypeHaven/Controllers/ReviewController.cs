using HypeHaven.Interfaces;

namespace HypeHaven.Controllers
{
    public class ReviewController
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
    }
}
