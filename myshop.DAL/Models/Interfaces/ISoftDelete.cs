using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Models.Interfaces
{
    public interface ISoftDelete
    {
        public bool IsDeleted {  get; set; }
        public DateOnly? DeletedAt { get; set; }
    }
}
