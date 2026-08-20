using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; } = 0;

        public int Count { get; set; } = 0;

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
