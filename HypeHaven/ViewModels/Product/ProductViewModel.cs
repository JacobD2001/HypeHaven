using HypeHaven.models;
using System.ComponentModel.DataAnnotations;

namespace HypeHaven.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public string Image { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Size cannot be longer than 50 characters.")]
        public string? Size { get; set; }

        [StringLength(50, ErrorMessage = "Color cannot be longer than 50 characters.")]
        public string? Color { get; set; }

        [StringLength(50, ErrorMessage = "Material cannot be longer than 50 characters.")]
        public string? Material { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public List<Category> Categories { get; set; }
        [Required(ErrorMessage = "Brand is required.")]
        public List<Brand> Brands { get; set; }
    }
}
