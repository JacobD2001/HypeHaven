using HypeHaven.models;

namespace HypeHaven.ViewModels
{
    public class CreateProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string Image { get; set; } = null!;

        public string? Size { get; set; }

        public string? Color { get; set; }

        public string? Material { get; set; }

        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
       // public virtual Brand Brand { get; set; } = null!;
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
