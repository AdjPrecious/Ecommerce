using EWebApp.Data;

namespace EWebApp.Models
{
    public interface IProductRepository 
    {
        Task<IEnumerable<Products>> GetAllAsync();
        Task<Products> GetByIdAync(int id);

        Task <IEnumerable<Products>> GetByCategoriesAsync(ProductCategory productCategory);
        Task<Products> UpdateAsync(Products product);

        Task<Products> DeleteAsync(int id);

        Task AddAsync(Products product);

        Task<List<ProductCategory>> GetAllCategoriesAsync();

       
    }
}
