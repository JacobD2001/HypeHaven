using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using HypeHaven.Repositories;
using HypeHaven.ViewModels.ReviewViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HypeHaven.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReviewController(IReviewRepository reviewRepository, IHttpContextAccessor httpContextAccessor)
        {
            _reviewRepository = reviewRepository;
            _httpContextAccessor = httpContextAccessor;
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
                return View("Error");

            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (review.UserId == currentUserId)
            {
                var editReviewVM = new EditReviewViewModel
                {
                    Text = review.Text,
                    Rating = review.Rating,
                    ProductId = review.ProductId,
                    BrandId = review.BrandId,
                    Id = review.Id
                };

                return View(editReviewVM);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditReviewViewModel editReviewVM)
        {
            var curReview = await _reviewRepository.GetReviewByIdAsync(id);

            if (curReview == null)
                return View("Error");

            if (ModelState.IsValid)
            {
                // Update the existing review with the data from editReviewVM
                curReview.Text = editReviewVM.Text;
                curReview.Rating = editReviewVM.Rating;

                // Save the changes to the database
                _reviewRepository.Update(curReview);

                return RedirectToAction("Detail", "Product", new { id = curReview.ProductId });
            }

            // If ModelState is not valid, return the view with validation errors
            return View(editReviewVM);
        }

        //this method deletes review
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);

            if (review == null)
                return View("Error");

            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();

            if (review.UserId == currentUserId)
            {
                var deleteReviewVM = new DeleteReviewViewModel
                {
                    Text = review.Text,
                    Rating = review.Rating,
                    ProductId = review.ProductId,
                    BrandId = review.BrandId,
                    Id = review.Id
                };

                return View(deleteReviewVM);
            }
            return View();


        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);

            if (review == null)
                return View("Error");
            _reviewRepository.Delete(review);
            return RedirectToAction("Detail", "Product", new { id = review.ProductId });

        }

    }
 
}
