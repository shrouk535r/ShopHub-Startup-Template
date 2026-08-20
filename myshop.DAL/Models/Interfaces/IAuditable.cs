using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Models.Interfaces
{
    public interface IAuditable
    {
        public DateOnly UpdatedAt { get; set; }
        public DateOnly CreatedAt { get; set; } 

    }
}
