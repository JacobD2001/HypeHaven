//using HypeHaven.models;
//PLATNOSC

namespace HypeHaven.models
{
    /// <summary>
    /// Represents a cartItem.
    /// </summary>
    public class CartItem
    {
        public int CartItemId { get; set; } 
        public int CartId { get; set; } // Foreign key for the Cart
        public Cart Cart { get; set; } // Navigation property to access the related Cart
        public int ProductId { get; set; } // Foreign key for the Product
        public Product Product { get; set; } // Navigation property to access the related Product
        public int Quantity { get; set; } 
    }
}
