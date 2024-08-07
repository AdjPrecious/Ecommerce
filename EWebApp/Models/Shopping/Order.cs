using System.ComponentModel.DataAnnotations.Schema;

namespace EWebApp.Models.Shopping
{
    public class Order
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string userId { get; set; }

        [ForeignKey(nameof(userId))]
        public ApplicationUser User { get; set; }


        public List<OrderItem> OrderItems { get; set; }
    }
}
