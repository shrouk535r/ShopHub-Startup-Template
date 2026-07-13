using Microsoft.EntityFrameworkCore;
using myshop.DAL.Repositories.IRepositories;
using myshop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories
{
    internal class GenericRepo<TEntity> :IGenericRepo<TEntity> where TEntity : class
    {
        private ApplicationDbContext _context;
        public GenericRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<TEntity>> GetAll( params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

            }
            
            return await query.ToListAsync();
        }
        public async Task<TEntity> GetById(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query= _context.Set<TEntity>().AsQueryable();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

            }
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

        }
        public async Task Add(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
        
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
