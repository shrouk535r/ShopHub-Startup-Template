using myshop.DAL.Repositories.IRepositories;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    public class OrderHeaderRepo : IOrderHeaderRepo
    {
        private ApplicationDbContext _context;
        public OrderHeaderRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(OrderHeader order)
        {
            await _context.OrderHeaders.AddAsync(order);
        }
    }
}
