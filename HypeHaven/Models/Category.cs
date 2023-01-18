using System;
using System.Collections.Generic;

namespace HypeHaven.models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? ParentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Category> InverseParent { get; } = new List<Category>();

    public virtual Category? Parent { get; set; }
}
