using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTO
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [DisplayName("Image")]
        [ValidateNever]
        public string Img { get; set; }

        [Required]
        public decimal Price { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        public int ReviewsCount { get; set; }
        public decimal ReviewRating { get; set; }

        public List<ReviewDto> Reviews { get; set; }
    }
}
