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
        
        public async Task<bool> Delete(Product p)
        {
            return await _deleteproductrepo.Delete(p);
        }

    }
}
