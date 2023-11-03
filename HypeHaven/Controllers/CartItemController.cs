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

            var cartItem = new CartItem
            {
                ProductId = productId,
                Quantity = 1,
                Cart = cart  // Associate the cart with the cart item
            };

            _cartItemRepository.Add(cartItem);

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

            var domain = "https://localhost:7143/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + "CartItem/OrderConfirmation",
                CancelUrl = domain + "CartItem/OrderCancelled",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                Currency = "pln",  // Set the currency here
            };

            foreach (var item in cartItems)
            {
                // Calculate the unit amount based on the price and quantity in the smallest currency unit (grosze for PLN).
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
            }

            var service = new SessionService();
            Session session = service.Create(options);

            // Used for redirection
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }



        /*
                [HttpPost]
                public async Task<IActionResult> CheckOut()
                {
                    var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
                    var cart = await _cartRepository.GetCartByUserIdAsync(currentUserId);
                    var cartItems = await _cartItemRepository.GetAllForSpecifedUser();

                    var domain = "https://localhost:7143/";

                    var options = new SessionCreateOptions
                    {
                        SuccessUrl = domain + $"CartItem/OrderConfirmation",
                        CancelUrl = domain + $"CartItem/OrderCancelled",
                        LineItems = new List<SessionLineItemOptions>(),
                        Mode = "payment",              
                    };

                    foreach (var item in cartItems)
                    {
                        var sessionListItem = new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)item.Product.Price * item.Product.Quantity,
                                Currency = "pln",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Product.Name, // = item.Product.ToString(),
                                },
                            },
                            Quantity = item.Quantity,
                        };
                        options.LineItems.Add(sessionListItem);
                    }

                    var service = new SessionService();
                    Session session = service.Create(options);

                    //used for redirection
                    Response.Headers.Add("Location", session.Url);
                    return new StatusCodeResult(303);

                   *//* var options = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string>
                        {
                            "card",
                        },
                        LineItems = new List<SessionLineItemOptions>(),
                        Mode = "payment",
                        SuccessUrl = "https://localhost:5001/CartItem/CheckoutSuccess?session_id={{CHECKOUT_SESSION_ID}}",
                        CancelUrl = "https://localhost:5001/CartItem/CheckoutCancel",
                    };

                    foreach (var item in cartItems)
                    {
                        options.LineItems.Add(new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                UnitAmount = (long)item.Product.Price * 100,
                                Currency = "pln",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.Product.Name,
                                },
                            },
                            Quantity = item.Quantity,
                        });
                    }

                    var service = new SessionService();
                    Session session = service.Create(options);

                    return Redirect(session.Url);
                }*//*
                   }
        */

    }
}
