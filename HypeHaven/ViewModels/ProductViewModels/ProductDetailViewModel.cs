using HypeHaven.models;
using System.ComponentModel.DataAnnotations;

namespace HypeHaven.ViewModels.ProductViewModels
{
    /// <summary>
    /// View model for detail of a product.
    /// </summary>
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

    }
}
