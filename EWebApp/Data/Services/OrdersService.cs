using EWebApp.Dbclass;
using EWebApp.Models;
using EWebApp.Models.Shopping;
using Microsoft.EntityFrameworkCore;

namespace EWebApp.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
           var orders = await _context.Orders.Include(n => n.OrderItems)
                         
                                       .ThenInclude(n => n.Products)
                                       .Include(n => n.User)
                                       .ToListAsync();
            if(userRole != "admin")
            {
                orders = orders.Where(n => n.userId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItems> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                userId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    OrdersId = order.Id,
                    Price = item.Products.Price,
                    ProductsId = item.Products.Id,
                    
                };
                await _context.OrderItems.AddAsync(orderItem);

            }
            await _context.SaveChangesAsync();
        }
    }
}
