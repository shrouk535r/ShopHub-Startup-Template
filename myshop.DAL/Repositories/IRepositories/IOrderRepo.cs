using Microsoft.EntityFrameworkCore.Storage;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories.IRepositories
{
    public interface IOrderRepo
    {
        public Task<IEnumerable<Order>> GetOrders();
        public Task<Order> GetOrder(int id);
        public Task AddOrder(Order order);
        public Task<IDbContextTransaction> BeginTransaction();
        public Task SaveChangesAsync();
    }
}
