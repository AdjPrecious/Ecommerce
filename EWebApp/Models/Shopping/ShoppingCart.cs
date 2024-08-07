using EWebApp.Dbclass;
using Microsoft.EntityFrameworkCore;

namespace EWebApp.Models.Shopping
{
    public class ShoppingCart
    {
        public AppDbContext _context { get; }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItems> shoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }


        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemToCart(Products products)
        {
            var shoppingCartItems = _context.ShoppingCartItems.FirstOrDefault(n =>
            n.Products.Id == products.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItems == null)
            {
                shoppingCartItems = new ShoppingCartItems()
                {
                    ShoppingCartId = ShoppingCartId,
                    Products = products,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItems);
            }
            else
            {
                shoppingCartItems.Amount++;
            }
            _context.SaveChanges();
        }

        public void RemoveItemFromCart(Products products)
        {
            var shoppingCartItems = _context.ShoppingCartItems.FirstOrDefault(n =>
            n.Products.Id == products.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItems != null)
            {
                if (shoppingCartItems.Amount > 1)
                {
                    shoppingCartItems.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItems);

                }
            }
            _context.SaveChanges();
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
                                                        .ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public List<ShoppingCartItems> GetShoppingCartItems()
        {
            return shoppingCartItems ??= _context.ShoppingCartItems.Include(n => n.Products)
                                                                   .Where(n => n.ShoppingCartId == ShoppingCartId)
                                                                   .ToList();
        }

        public double GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
                                             .Select(n => n.Products.Price * n.Amount)
                                             .Sum();
        }
    }
}
        
        

        
    

