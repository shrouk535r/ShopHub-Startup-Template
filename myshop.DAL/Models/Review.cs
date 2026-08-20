using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Description { get; set; }
        [Range(1,5,ErrorMessage ="value must be from 1 to 5")]
        public int Value { get; set;}
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }
    }
}
