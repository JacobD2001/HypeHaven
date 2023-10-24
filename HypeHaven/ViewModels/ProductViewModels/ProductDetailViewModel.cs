using HypeHaven.models;
using System.ComponentModel.DataAnnotations;

namespace HypeHaven.ViewModels.ProductViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Review> Reviews { get; set; }

        


        /*   public string Name { get; set; }
           public decimal Price { get; set; }
           public IFormFile Image { get; set; } = null!;
           public string Description { get; set; }
           public string? Size { get; set; }
           public string? Color { get; set; }
           public string? Material { get; set; }
           public int Quantity { get; set; }*/



    }
}
