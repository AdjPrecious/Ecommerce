using EWebApp.Data;
using System.ComponentModel.DataAnnotations;

namespace EWebApp.Models.ProductVM.ProductViewModel
{
    public class KitchenViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Reqired")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is compulsory")]
        [Display(Description = "Describe the product")]
        public string Description { get; set; }

        [Required]
        [Display(Description = "Price")]
        public double Price { get; set; }

        public IFormFile ProductPhoto { get; set; }
       
        
    }
}
