using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string UserName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public string? OrderStatus { get; set; }

    }
}
