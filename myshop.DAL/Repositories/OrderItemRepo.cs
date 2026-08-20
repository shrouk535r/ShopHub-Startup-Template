using myshop.DAL.Models;
using myshop.DAL.Repositories.IRepositories;
using myshop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    internal class OrderItemRepo : IOrderItemRepo
    {
        private ApplicationDbContext _context;
        public OrderItemRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddItem(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
        }
    }
}
