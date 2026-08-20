using Microsoft.EntityFrameworkCore;
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
    public class CartRepo : ICartRepo
    {
        private ApplicationDbContext _context;
        public CartRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCart?> GetCartByUser(string UserId)
        {
            return await _context.ShoppingCarts
                .Include(SC => SC.CartItems)
                .ThenInclude(ci => ci.product)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == UserId);
        }
        public async Task<ShoppingCart?> GetCartById(int Id)
        {
            return await _context.ShoppingCarts
                .Include(SC => SC.CartItems)
                .ThenInclude(ci => ci.product)
                .FirstOrDefaultAsync(c => c.Id == Id);
        }
        public async Task AddCart(ShoppingCart cart)
        {
            await _context.ShoppingCarts.AddAsync(cart);
        }
        public void Update(ShoppingCart cart)
        {
            _context.ShoppingCarts.Update(cart);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
