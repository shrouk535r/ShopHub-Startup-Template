using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using myshop.DAL.Models;
using myshop.DAL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.Entities.Models
{
    public class Product:ISoftDelete,IAuditable
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string Img { get; set; }

        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateOnly? DeletedAt {  get; set; }
        public DateOnly UpdatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }

    }
}
