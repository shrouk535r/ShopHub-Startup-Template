using Microsoft.EntityFrameworkCore;
using myshop.DAL.Repositories.IRepositories;
using myshop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    internal class DeleteRepo<TEntity>:IDeleteRepo<TEntity> where TEntity : class
    {
        private ApplicationDbContext _context;
        public DeleteRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(TEntity entity)
        {
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                return true;
            }
            return false;
        }
    }
}
