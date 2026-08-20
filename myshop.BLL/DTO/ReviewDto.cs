using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.DTO
{
    public class ReviewDto
    {
        public string UserName { get; set; }
        public string Description { get; set; }
        [Range(1, 5, ErrorMessage = "value must be from 1 to 5")]
        public int Value { get; set; }

    }
}
