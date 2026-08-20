using myshop.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTO
{
    public class CheckoutDto
    {
        //userinfo
        //user will enter these info

        //if not enter name will take from user info
        [Length(3, 100)]
        public string? UserName { get; set; }
        [MaxLength(2000)]
        public string Address { get; set; }
        [MaxLength(200)]
        public string City { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        //user will show these info
        public List<ShoppingCartItemDto> ShoppingCartItems { get; set; }
        public DateTime ShippingDate { get; set; }= DateTime.Now.AddDays(3);

        //then choose payment method
        public PaymentMethod MethodType { get; set; }

        //then will view order summary
        public int Count { get; set; }

        public decimal Price { get; set; }


    }
}
