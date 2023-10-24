using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.AspNetCore.Mvc;

namespace HypeHaven.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        /*[HttpPost]
        public async Task<IActionResult> Create(Review review)
        {
            if (ModelState.IsValid)
            {
                _reviewRepository.Add(review);
                return RedirectToAction("Detail", "Product", new { id = review.ProductId });
            }
            return View(review);
        }*/

    }
}
