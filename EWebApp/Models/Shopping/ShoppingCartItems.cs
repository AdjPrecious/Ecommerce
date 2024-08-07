namespace EWebApp.Models.Shopping
{
    public class ShoppingCartItems
    {
        public int Id { get; set; }

        public Products Products { get; set; }

        public int Amount {  get; set; }

        public string ShoppingCartId { get; set; }  
    }
}
