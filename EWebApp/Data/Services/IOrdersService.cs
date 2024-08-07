using EWebApp.Models.Shopping;

namespace EWebApp.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItems> items, string userId, string userEmailAddress);

        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
