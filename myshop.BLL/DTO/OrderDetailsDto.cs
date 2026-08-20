using myshop.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTO
{
    public class OrderDetailsDto
    {
        //orderInfo
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public string? OrderStatus { get; set; }
        public List<OrderItemDto> OrderItems { get; set;}
        public decimal Price { get; set; }

        public int Count { get; set; }

        // paymentinfo
        public string? PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod MethodType { get; set; }

        //userinfo
        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }

    }
}
