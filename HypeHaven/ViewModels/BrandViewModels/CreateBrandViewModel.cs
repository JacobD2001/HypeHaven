using HypeHaven.models;
using System.ComponentModel.DataAnnotations;

namespace HypeHaven.ViewModels.BrandViewModels
{
    public class CreateBrandViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Location cannot be longer than 100 characters.")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; }

        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone number can only contain numbers.")]
        public string? PhoneNumber { get; set; }


        [Url(ErrorMessage = "Invalid Instagram URL.")]
        public string? Instagram { get; set; }

        [Url(ErrorMessage = "Invalid Facebook URL.")]
        public string? Facebook { get; set; }

        [Url(ErrorMessage = "Invalid Pinterest URL.")]
        public string? Pinterest { get; set; }

        [Url(ErrorMessage = "Invalid Tiktok URL.")]
        public string? Tiktok { get; set; }

        [Url(ErrorMessage = "Invalid Video URL.")]
        public string? Video { get; set; }

        public int CategoryId { get; set; }

        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Category is required.")]
        public List<Category> Categories { get; set; }


    }
}
