using System;
using System.Collections.Generic;

namespace HypeHaven.models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int BrandId { get; set; }

    public int ProductId { get; set; }

    public string Id { get; set; } = null!;
  
    public string UserId { get; set; } 

    public int Rating { get; set; }

    public string? Text { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
