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
    internal class CategoryRepo:GenericRepo<Category>,ICategoryRepo
    {
        private ApplicationDbContext _context;
        private IDeleteRepo<Category> _deletecategoryrepo;
        public CategoryRepo(ApplicationDbContext context,
            IDeleteRepo<Category> deleteCategoryrepo) : base(context)
        {
            _context = context;
            _deletecategoryrepo = deleteCategoryrepo;
        }
        public async Task<bool> Delete(Category C)
        {
            return await _deletecategoryrepo.Delete(C);
        }
    }
}
