﻿
namespace HypeHaven.models
{
    /// <summary>
    /// Represents a favoriteProduct.
    /// </summary>
    public partial class FavoriteProduct
    {
        public int FavoriteProductId { get; set; }
        public string UserId { get; set; } = null!;
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public bool IsFavorite { get; set; }

    }
}
