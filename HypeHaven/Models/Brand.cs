using System;
using System.Collections.Generic;

namespace HypeHaven.models;

public partial class Brand
{
    public int BrandId { get; set; }

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

    public DateTime DateAdded { get; set; }

    public string Id { get; set; } = null!;

    public int CategoryId { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();
}
