
using EWebApp.Data;
using EWebApp.Dbclass;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace EWebApp.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context ) 
        {
            _context = context;
        }
        public async Task AddAsync(Products product) => await _context.Products.AddAsync(product);
             
     
        public async Task<IEnumerable<Products>> GetAllAsync() => await _context.Products.ToListAsync();
            
        

        public async Task<Products> GetByIdAync(int id) => await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            

        public async Task<Products?> UpdateAsync(Products product)
        {
            var products = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id); 
            if(products != null)
            {
                products.Price = product.Price;
                products.ProduceCategory = product.ProduceCategory;
                products.Photo = product.Photo;
                products.Description = product.Description;
                product.Id = product.Id;
                   
                await _context.SaveChangesAsync();
            }
            return products;


        }

        public async Task<IEnumerable<Products>> GetByCategoriesAsync(ProductCategory productCategory) => await _context.Products.Where(p => p.ProduceCategory == productCategory).ToListAsync();

        public async Task<List<ProductCategory>> GetAllCategoriesAsync()
        {
            var productTypes = await _context.Products.Select(p => p.ProduceCategory).Distinct().ToListAsync();
            return productTypes;
        }

        public async Task<Products> DeleteAsync(int id)
        {
            var item = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if(item != null)
            {
                _context.Products.Remove(item);
                _context.SaveChanges();
            }
            return item;
        }
    }
}
