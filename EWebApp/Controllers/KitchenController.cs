using EWebApp.Data;
using EWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EWebApp.Controllers
{
    public class KitchenController : Controller
    {
        private readonly IProductRepository _products;

        public KitchenController(IProductRepository products)
        {
            
            _products = products;
        }
        public async Task<IActionResult> Index()
        {

            var allKitchen = await _products.GetByCategoriesAsync(ProductCategory.Kitchen);

            return View(allKitchen);
        }
    }
}
