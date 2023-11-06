using HypeHaven.Interfaces;
using HypeHaven.models;
using Microsoft.AspNetCore.Mvc;

namespace HypeHaven.Controllers
{
    /// <summary>
    /// Controller for managing favorite products.
    /// </summary>
    public class FavoriteProductController : Controller
    {
        private readonly IFavoriteProductRepository _FavoriteProductRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoriteProductController"/> class.
        /// </summary>
        /// <param name="favoriteProductRepository">The favorite product repository.</param>   
        public FavoriteProductController(IFavoriteProductRepository favoriteProductRepository)
        {
            _FavoriteProductRepository = favoriteProductRepository;
        }
    }
}
