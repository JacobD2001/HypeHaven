//PLATNOSC
namespace HypeHaven.models
{
    public class Cart
    {
        public int CartId { get; set; }          
        public string UserId { get; set; }       // ID of the user who owns the cart 
        public List<CartItem> CartItems { get; set; } // List of products in the cart
    }
}
