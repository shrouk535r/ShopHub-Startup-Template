using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTO
{
    public class ShoppingCartDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int Count { get; set; }
        public List<ShoppingCartItemDto> CartItems { get; set; }
    }
}
