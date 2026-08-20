using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories.IRepositories
{
    public interface ICartRepo
    {
        public Task<ShoppingCart> GetCartByUser(string UserId);
        public Task<ShoppingCart?> GetCartById(int Id);
        public Task AddCart(ShoppingCart cart);
        public void Update(ShoppingCart cart);
        public Task Save();

    }
}
