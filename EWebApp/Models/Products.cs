using EWebApp.Data;

namespace EWebApp.Models
{
    public class Products
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public String Photo { get; set; }

        public ProductCategory ProduceCategory { get; set; }
    }
}
