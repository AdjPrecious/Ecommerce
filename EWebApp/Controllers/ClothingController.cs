using EWebApp.Data;
using EWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EWebApp.Controllers
{
    public class ClothingController : Controller
    {
        private readonly IProductRepository _products;

        public ClothingController(IProductRepository products)
        {

            _products = products;
        }
        public async Task<IActionResult> Index()
        {

            var allKitchen = await _products.GetByCategoriesAsync(ProductCategory.Clothing);

            return View(allKitchen);
        }
    }
}
