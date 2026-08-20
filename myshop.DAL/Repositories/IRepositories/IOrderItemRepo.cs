using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories.IRepositories
{
    public interface IOrderItemRepo
    {
        public Task AddItem(OrderItem orderItem);
    }
}
