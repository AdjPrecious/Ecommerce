using EWebApp.Dbclass;
using EWebApp.Models;
using EWebApp.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EWebApp.Controllers
{
    public class ProductionController : Controller
    {
        private readonly IProductRepository _products;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductionController(IProductRepository products, AppDbContext context,
                                    IWebHostEnvironment hostEnvironment)
        {
            _products = products;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var allproduct = await _products.GetAllAsync();
            return View(allproduct);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            

            var allproduct = await _products.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allproduct.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();
                if (filteredResult == null)
                {
                    return View("Filter", filteredResult);

                }
                else
                    return View("Filter");
                
            }
            return View("Index", allproduct);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
          /*  var productService = new ProductRepository(_context);
            var productTypes = await productService.GetAllCategoriesAsync();

            var productTypeSelectedList = productTypes.Select(pt => new SelectListItem
            {
                Value = ((int)pt).ToString(),
                Text = pt.ToString(),
            }).ToList();

            ViewBag.ProductTypeSelectList = productTypeSelectedList;*/


            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Products newProduct = new Products()
                {
                    Name = model.Name,
                    Price = model.Price,
                    ProduceCategory = model.ProduceCategory,
                    Description = model.Description,
                    Photo = uniqueFileName
                };
                _context.Products.AddAsync(newProduct);
                _context.SaveChanges();

                return RedirectToAction("details", new { id = newProduct.Id });
            }
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            
            var getProducts = await _products.GetByIdAync(id.Value);

            if (getProducts == null)
            {
                Response.StatusCode = 404;
                return View("NotFound", id.Value);
            }

            ProductDetailsViewModel viewModel = new ProductDetailsViewModel()
            {
                products = getProducts
                
            };

            return View(viewModel);


        }

        private string ProcessUploadedFile(ProductCreateViewModel model)
        {
            string uniqueFileName = " ";
            if (model.ProductPhoto != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProductPhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProductPhoto.CopyTo(fileStream);

                }
            }

            return uniqueFileName;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _products.GetByIdAync(id);
            if (productDetails == null) return View("NotFound");

            
            
                var response = new EditProductViewModel()
                {
                    Id = productDetails.Id,
                    Name = productDetails.Name,
                    Price = productDetails.Price,
                    Description = productDetails.Description,
                    ProduceCategory = productDetails.ProduceCategory,
                    EditPhoto = productDetails.Photo

                };
                return View(response);
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit (EditProductViewModel model)
        {
           
            if (ModelState.IsValid)
            {
                var product = await _products.GetByIdAync(model.Id);

                
                    product.Name = model.Name;
                    product.Price = model.Price;
                    product.Description = model.Description;
                    product.ProduceCategory = model.ProduceCategory;
                    if(model.ProductPhoto != null)
                    {
                        if(model.EditPhoto != null)
                        {
                            string filePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", model.EditPhoto);
                            System.IO.File.Delete(filePath);

                        }
                         product.Photo = ProcessUploadedFile(model);

                    }

                await _products.UpdateAsync(product); 
                
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete (int id)
        {
            var product = await _products.GetByIdAync(id);
            if (product != null)
            {
                await _products.DeleteAsync(product.Id);
                return RedirectToAction("Index", "Production");
            }

            return View("NotFound");


        }




    }


}
      