using Microsoft.EntityFrameworkCore;
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
    internal class CartItemRepo : GenericRepo<CartItem>, ICartItemRepo
    {
        private ApplicationDbContext _context;
        private IDeleteRepo<CartItem> _deleteproductrepo;

        public CartItemRepo(ApplicationDbContext context, IDeleteRepo<CartItem> deleteproductrepo) : base(context)
        {
            _context = context;
            _deleteproductrepo = deleteproductrepo;
        }

        public async Task<bool> Delete(CartItem entity)
        {
            return await _deleteproductrepo.Delete(entity);
        }

        public async Task<IEnumerable<CartItem>> GetbyCartId(int CartId)
        {
            return await _context.CartItems
                .Include(CI => CI.product)
                .Where(CI => CI.ShoppingCartId==CartId)
                .ToListAsync();
        }
    }
}
