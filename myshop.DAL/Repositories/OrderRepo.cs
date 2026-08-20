using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
    internal class OrderRepo : IOrderRepo
    {
        private ApplicationDbContext _context;
        public OrderRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task<Order?> GetOrder(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderHeader)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.product)
                .FirstOrDefaultAsync(o => o.Id==id); 
        }


        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders
                .Include(o => o.OrderHeader)
                .ToListAsync();

        }
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
