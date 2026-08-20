using Microsoft.EntityFrameworkCore;
using myshop.DAL.Enums;
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
    internal class ProductRepo : GenericRepo<Product>, IProductRepo
    {
        private ApplicationDbContext _context;
        private IDeleteRepo<Product> _deleteproductrepo;
        public ProductRepo(ApplicationDbContext context,
            IDeleteRepo<Product> deleteproductrepo) : base(context)
        {
            _context = context;
            _deleteproductrepo = deleteproductrepo;
        }
        public async Task<Product?> GetProductDetailById(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.User!) 
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<(IEnumerable<Product> Products, int total)> GetFilteredProducts(int pageSize,int Pagenum, string searchTxt,
            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy)
        {
            var query =  _context.Products.Include(p => p.Category)
                .Where(p => p.Name.ToLower().Contains(searchTxt.ToLower()) ||
                p.Description.ToLower().Contains(searchTxt.ToLower()));
            
            int total = await query.CountAsync();
            query = orderBy == null ? query : orderBy(query);
            
            var products= await query.Skip((Pagenum-1)*pageSize).Take(pageSize).ToListAsync();
            return (products, total);

        }

        public async Task<bool> Delete(Product p)
        {
            return await _deleteproductrepo.Delete(p);
        }

        public async Task<IEnumerable<Product>> GetArchievedProducts()
        {
            return await _context.Products
                .Include(P => P.Category)
                .IgnoreQueryFilters()
                .Where(p => p.IsDeleted)
                .ToListAsync();
        }
        public async Task<Product?> GetArchievedProductBbyId(int Id)
        {
            return await _context.Products
                .Include(P => P.Category)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(P => P.Id == Id);
                }
        public async Task RestoreProduct(Product P) => P.IsDeleted = false;

    }
}
