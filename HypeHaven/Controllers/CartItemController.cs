using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;

namespace HypeHaven.Controllers
{
    public class CartItemController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartItemController"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="cartItemRepository">The cart item repository.</param>
        /// <param name="productRepository">The product repository.</param>
        /// <param name="cartRepository">The cart repository.</param>
        public CartItemController(IHttpContextAccessor httpContextAccessor, ICartItemRepository cartItemRepository, IProductRepository productRepository, ICartRepository cartRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        /// <summary>
        /// Adds a product to the user's cart.
        /// </summary>
        /// <param name="productId">The ID of the product to add to the
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var product = await _productRepository.GetByIdAsync(productId);

            if (product == null)
                return NotFound();

            var cart = await _cartRepository.GetCartByUserIdAsync(currentUserId);
            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = currentUserId
                };
                _cartRepository.Add(cart);
            }

            var existingCartItem = await _cartItemRepository.GetCartItemByProductId(cart.CartId, productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
                _cartItemRepository.Update(existingCartItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = 1,
                    Cart = cart
                };

                _cartItemRepository.Add(cartItem);
            }

            return RedirectToAction("ViewCart");
        }

        /// <summary>
        /// Displays the user's cart items.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ViewCart()
        {
            var cartItems = await _cartItemRepository.GetAllForSpecifedUser();
            return View(cartItems);
        }

        /// <summary>
        /// Initiates the checkout process for the user's cart.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CheckOut()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var cart = await _cartRepository.GetCartByUserIdAsync(currentUserId);
            var cartItems = await _cartItemRepository.GetAllForSpecifedUser();

            if (cart == null || cart.CartItems == null || cart.CartItems.Count == 0)
            {
                return RedirectToAction("Index", "Product");
            }

            var domain = "https://localhost:7143/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "CartItem/OrderConfirmation",
                CancelUrl = domain + "CartItem/OrderFailed",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                Currency = "pln",
                CustomerEmail = User.Identity.Name
            };

            foreach (var item in cartItems)
            {
                long unitAmount = (long)(item.Product.Price * 100);

                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = unitAmount,
                        Currency = "pln",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                        },
                    },
                    Quantity = item.Quantity,
                };

                options.LineItems.Add(sessionListItem);

                item.Product.Quantity -= item.Quantity;
                _productRepository.Update(item.Product);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        /// <summary>
        /// Displays the order confirmation page if the payment was successful, otherwise displays the order failed page.
        /// </summary>
        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());

            if (session.PaymentStatus == "paid")
                return View("OrderConfirmed");
            return View("OrderFailed");
        }

        /// <summary>
        /// Updates the quantity of a cart item in the user's cart.
        /// </summary>
        /// <param name="cartItemId">The ID of the cart item to update.</param>
        /// <param name="quantity">The new quantity of the cart item.</param>
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var cartItem = await _cartItemRepository.GetCartItemByIdAsync(cartItemId);

            if (cartItem == null)
            {
                return RedirectToAction("ViewCart");
            }

            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);

            if (quantity <= 0)
            {
                _cartItemRepository.Delete(cartItem);
            }
            else if (quantity <= product.Quantity)
            {
                cartItem.Quantity = quantity;
                _cartItemRepository.Update(cartItem);
            }
            else
            {
                TempData["ErrorMessage"] = $"The requested quantity exceeds the available stock. Available quantity is: {product.Quantity}";
            }

            return RedirectToAction("ViewCart");
        }

        /// <summary>
        /// Removes a product from the user's cart.
        /// </summary>
        /// <param name="cartItemId">The ID of the cart item to remove.</param>
        [HttpPost]
        public async Task<IActionResult> RemoveProduct(int cartItemId)
        {
            var cartItem = await _cartItemRepository.GetCartItemByIdAsync(cartItemId);

            if (cartItem != null)
                _cartItemRepository.Delete(cartItem);

            return RedirectToAction("ViewCart");
        }
    }
}
