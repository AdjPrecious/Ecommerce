using System.ComponentModel.DataAnnotations;

namespace EWebApp.Models.Account
{
    public class RegisterVN
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }


        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email Address is required")]
        public string EmailAddress { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Confirm Password")]
        [Required(ErrorMessage ="Password is different")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
