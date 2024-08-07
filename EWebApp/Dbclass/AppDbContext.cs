using EWebApp.Models;
using EWebApp.Models.Shopping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EWebApp.Dbclass
{
    public class AppDbContext :IdentityDbContext<ApplicationUser>
    {
       
        private readonly DbContextOptions<AppDbContext> _options;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
           
            _options = options;
        }


        public DbSet<Products> Products { get; set; }


        // Ordered related tables
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ShoppingCartItems> ShoppingCartItems { get; set; }

    }
}
