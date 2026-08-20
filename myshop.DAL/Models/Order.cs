using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int OrderHeaderId { get; set; }
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }

        [ValidateNever]
        public ICollection<OrderItem> OrderItems { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }


    }
}
