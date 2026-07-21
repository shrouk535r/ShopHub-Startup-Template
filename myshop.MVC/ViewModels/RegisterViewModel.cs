using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace myshop.MVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("First Name")]
        [MinLength(3, ErrorMessage = "First name must contain at least 3 characters.")]
        public string FName { get; set; }
        [DisplayName("Last Name")]
        [MinLength(3, ErrorMessage = "Last name must contain at least 3 characters.")]
        public string LName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

    }
}
