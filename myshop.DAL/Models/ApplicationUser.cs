using Microsoft.AspNetCore.Identity;
using myshop.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [DisplayName("First Name")]
        [MinLength(3,ErrorMessage = "First name must contain at least 3 characters.")]
        public string FName { get; set; }
        [DisplayName("Last Name")]
        [MinLength(3,ErrorMessage = "Last name must contain at least 3 characters.")]
        public string LName { get; set; }
        [DisplayName("Name")]
        public string FullName => $"{FName} {LName}";
        public string Address { get; set; }
        public string City { get; set; }
        public RoleEnum Role { get; set; }
    }
}
