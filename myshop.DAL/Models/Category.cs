using myshop.DAL.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace myshop.Entities.Models
{
    public class Category:ISoftDelete,IAuditable
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public DateOnly? DeletedAt { get; set; }
        public DateOnly UpdatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public ICollection<Product>? Products { get; set; }

    }
}
