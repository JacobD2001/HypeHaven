using HypeHaven.models;
using System.ComponentModel.DataAnnotations;

namespace HypeHaven.ViewModels.ProductViewModels
{
    /// <summary>
    /// View model for products.
    /// </summary>
    public class CrudProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, ErrorMessage = "Description cannot be longer than 100 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, 1000000, ErrorMessage = "Price must be a positive number and can't be bigger than 1000000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; } = null!;

        public List<string> Size { get; } = new List<string>
        {
            "Small",
            "Medium",
            "Large",
            "Extra Large"
        };

        public string SelectedSize { get; set; }

        public List<string> ColorPalette { get; } = new List<string>
        {
        "red",
        "orange",
        "yellow",
        "green",
        "blue",
        };

        public string? SelectedColor { get; set; }

        [StringLength(25, ErrorMessage = "Material cannot be longer than 25 characters.")]
        public string? Material { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, 1000000, ErrorMessage = "Quantity must be a positive integer.")]
        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public List<Category> Categories { get; set; }
        [Required(ErrorMessage = "Brand is required.")]
        public List<Brand> Brands { get; set; }

        public string? URL { get; set; }
    }
}
