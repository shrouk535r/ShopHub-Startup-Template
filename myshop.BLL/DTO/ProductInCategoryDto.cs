using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTO
{
    public class ProductInCategoryDto
    {
        public string Name { get; set; }
        [DisplayName("Image")]
        [ValidateNever]
        public string Img { get; set; }

    }
}
