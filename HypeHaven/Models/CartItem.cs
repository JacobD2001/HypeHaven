//using HypeHaven.models;

namespace HypeHaven.models
{
    public class CartItem
    {
        public int CartItemId { get; set; } // Unique identifier for the cart item
        public int CartId { get; set; } // Foreign key for the Cart
        public Cart Cart { get; set; } // Navigation property to access the related Cart
        public int ProductId { get; set; } // Foreign key for the Product
        public Product Product { get; set; } // Navigation property to access the related Product
        public int Quantity { get; set; } // Quantity of the product in the cart
    }
}
