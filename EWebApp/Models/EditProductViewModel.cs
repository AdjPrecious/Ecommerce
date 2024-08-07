using EWebApp.Data;
using System.ComponentModel.DataAnnotations;

namespace EWebApp.Models
{
    public class EditProductViewModel : ProductCreateViewModel
    {
        public int Id { get; set; }


        [Display(Name = "Photo")]
        public string EditPhoto { get; set; }
    }
}
        
      
