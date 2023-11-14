using HypeHaven.models;
using System.ComponentModel.DataAnnotations;

namespace HypeHaven.ViewModels.BrandViewModels
{
    /// <summary>
    /// View model for brands.
    /// </summary>
    public class BrandViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        public string Name { get; set; } = null!;

        [StringLength(100, ErrorMessage = "Description cannot be longer than 100 characters.")]
        public string? Description { get; set; }

        [StringLength(20, ErrorMessage = "Location cannot be longer than 20 characters.")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public IFormFile Image { get; set; }

        [StringLength(30, ErrorMessage = "Email cannot be longer than 30 characters.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [StringLength(9, ErrorMessage = "Phone number cannot be longer than 9 characters.")]
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

        public string? Video { get; set; }

        public int CategoryId { get; set; }

        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Category is required.")]
        public List<Category> Categories { get; set; }
        public string? URL { get; set; }


    }
}
