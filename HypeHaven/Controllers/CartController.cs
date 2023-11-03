//PLATNOSC
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HypeHaven.Controllers
{
    public class CartController : Controller
    {
        //this method retrives all products so basically shows the cart(only products that are in the cart = products that are added to cart by specifedd user
        //which means first i need to create add to cart method
/*        public IActionResult Index()
        {
            IEnumerable<Product> products = await _productRepository.GetAll();
            return View(products);

        }*/
    }
}
