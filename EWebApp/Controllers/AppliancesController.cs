using EWebApp.Data;
using EWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace EWebApp.Controllers
{
    public class AppliancesController : Controller
    {
        private readonly IProductRepository _products;

        public AppliancesController(IProductRepository products)
        {

            _products = products;
        }
        public async Task<IActionResult> Index()
        {

            var allKitchen = await _products.GetByCategoriesAsync(ProductCategory.Appliances);

            return View(allKitchen);
        }
    }
}
