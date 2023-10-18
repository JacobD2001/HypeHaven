using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.AspNetCore.Mvc;

namespace HypeHaven.Controllers
{
    public class FavoriteProduct : Controller
    {
        private readonly IFavoriteProductRepository _FavoriteProductRepository;

        public FavoriteProduct(IFavoriteProductRepository favoriteProductRepository)
        {
            _FavoriteProductRepository = favoriteProductRepository;
        }

       /* [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _FavoriteProductRepository.GetAll(); 
            return View(products);
        }*/
    }
}
