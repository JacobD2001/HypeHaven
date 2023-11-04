//PLATNOSC
using HypeHaven.Helpers;
using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
//using Stripe.BillingPortal;
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

            // Check if the product already exists in the cart
            var existingCartItem = await _cartItemRepository.GetCartItemByProductId(cart.CartId, productId);

            if (existingCartItem != null)
            {
                // Increase the quantity of the existing cart item
                existingCartItem.Quantity += 1;
                _cartItemRepository.Update(existingCartItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = 1,
                    Cart = cart // Associate the cart with the cart item
                };

                _cartItemRepository.Add(cartItem);
            }

            return RedirectToAction("ViewCart");
        }

        [HttpGet]
        public async Task<IActionResult> ViewCart()
        {
            var cartItems = await _cartItemRepository.GetAllForSpecifedUser();
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut()
        {
          
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var cart = await _cartRepository.GetCartByUserIdAsync(currentUserId);
            var cartItems = await _cartItemRepository.GetAllForSpecifedUser();

            if (cart == null || cart.CartItems == null || cart.CartItems.Count == 0)
            {
                // Redirect to a page or display a message indicating that the cart is empty.
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
                // Calculate the unit amount based on the price and quantity in the smallest currency unit (grosze for PLN)
                long unitAmount = (long)(item.Product.Price * 100); // Convert to grosze

                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = unitAmount,  // Set the unit amount
                        Currency = "pln",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                        },
                    },
                    Quantity = item.Quantity,
                };

                options.LineItems.Add(sessionListItem);

                //substracting bought amount of product from the quantity in the database
                item.Product.Quantity -= item.Quantity;
                _productRepository.Update(item.Product);
            }

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;
            // Used for redirection
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        //no need for async
        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());

            if (session.PaymentStatus == "paid")
                return View("OrderConfirmed");
            return View("OrderFailed");
        }

        //method for updating the quantity of the product in the cart
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var cartItem = await _cartItemRepository.GetCartItemByIdAsync(cartItemId);

            // Handle the case where the cart item does not exist
            if (cartItem == null)
            {
                return RedirectToAction("ViewCart");
            }

            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);

            // Handle the case where the quantity is less than or equal to 0 (remove the product)
            if (quantity <= 0)
            {
                _cartItemRepository.Delete(cartItem);
            }
            // Handle the case when the quantity is less than the available quantity(so user can operate on this between 0 and avaliable quantity)
            else if (quantity <= product.Quantity)
            {
                cartItem.Quantity = quantity;
                _cartItemRepository.Update(cartItem);
            }
            // Handle the case where the requested quantity is higher than the available quantity
            else
            {
                TempData["ErrorMessage"] = $"The requested quantity exceeds the available stock. Available quantity is: {product.Quantity}";
            }

            return RedirectToAction("ViewCart");
        }


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
