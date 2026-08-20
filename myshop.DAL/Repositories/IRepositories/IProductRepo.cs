using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories.IRepositories
{
    public interface IProductRepo:IGenericRepo<Product>,IDeleteRepo<Product>
    {
        public Task<(IEnumerable<Product> Products, int total)> GetFilteredProducts(int pageSize, int Pagenum, string searchTxt, Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy);

        public Task<IEnumerable<Product>> GetArchievedProducts();
        public Task<Product?> GetArchievedProductBbyId(int Id);
        public Task<Product?> GetProductDetailById(int id);
        public Task RestoreProduct(Product P);

    }
}
