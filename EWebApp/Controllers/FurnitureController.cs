using EWebApp.Data;
using EWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EWebApp.Controllers
{
    public class FurnitureController : Controller
    {
        private readonly IProductRepository _products;

        public FurnitureController(IProductRepository products)
        {

            _products = products;
        }
        public async Task<IActionResult> Index()
        {

            var allKitchen = await _products.GetByCategoriesAsync(ProductCategory.Furniture);

            return View(allKitchen);
        }


        
    }
}
