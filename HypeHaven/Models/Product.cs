using System;
using System.Collections.Generic;

namespace HypeHaven.models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string Image { get; set; } = null!;

    public string? Size { get; set; }

    public string? Color { get; set; }

    public string? Material { get; set; }

    public int Quantity { get; set; }

    public DateTime DateAdded { get; set; }

    public int BrandId { get; set; }
    //TEST
    public string UserId { get; set; }

    public int CategoryId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();
}
