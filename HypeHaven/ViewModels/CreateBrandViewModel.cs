using HypeHaven.models;

namespace HypeHaven.ViewModels
{
    public class CreateBrandViewModel
    {//todo Id
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Location { get; set; }

        public string? Image { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Instagram { get; set; }

        public string? Facebook { get; set; }

        public string? Pinterest { get; set; }

        public string? Tiktok { get; set; }

        public string? Video { get; set; }
        public int CategoryId { get; set; }
        public string Id { get; set; } = null!;

        public List<Category> Categories { get; set; }


    }
}
