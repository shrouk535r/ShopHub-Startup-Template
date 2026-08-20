using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTO
{
    public class ShoppingCartItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string? productImg { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}
