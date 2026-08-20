using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using myshop.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime ShippingDate { get; set; }


        public string? OrderStatus { get; set; } = "Completed";
        public string? PaymentStatus { get; set; } = "succeed";

        //public string? TrakcingNumber { get; set; }
        //public string? Carrier { get;set; }

        public DateTime PaymentDate { get; set; }
        public PaymentMethod MethodType { get; set; }
        //Stripe Properties

        //public string? SessionId { get; set; }
        //public string? PaymentIntentId { get; set; }

        //User Data
        [Length(3,100)]
        public string UserName { get; set; }
        [MaxLength(2000)]
        public string Address { get; set; }
        [MaxLength(200)]
        public string City { get; set; }
        
        [Phone]
        public string PhoneNumber { get; set; }

        
    }
}
