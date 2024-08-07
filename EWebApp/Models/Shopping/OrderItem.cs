using System.ComponentModel.DataAnnotations.Schema;

namespace EWebApp.Models.Shopping
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }

        public int OrdersId { get; set; }

        [ForeignKey("OrdersId")]
        public Order Orders { get; set; }

        public int ProductsId { get; set; }

        [ForeignKey("ProductsId")]
        public Products Products { get; set; }
    }
}
