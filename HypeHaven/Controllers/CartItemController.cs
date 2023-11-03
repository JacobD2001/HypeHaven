//PLATNOSC
using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HypeHaven.Controllers
{
    public class CartItemController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public CartItemController(IHttpContextAccessor httpContextAccessor, ICartItemRepository cartItemRepository, IProductRepository productRepository, ICartRepository cartRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                return NotFound();

            // Check if the user has an existing cart or create a new one
            var cart = await _cartRepository.GetCartByUserIdAsync(currentUserId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = currentUserId //if cart dosen't exist create a new cart and assaign it to a certain user
                };
                _cartRepository.Add(cart);
            }

            var cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = 1,
                Cart = cart  // Associate the cart with the cart item
            };

            _cartItemRepository.Add(cartItem);

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> ViewCart()
        {
            var cartItems = await _cartItemRepository.GetAllForSpecifedUser();
            return View(cartItems);
        }

    }
}
