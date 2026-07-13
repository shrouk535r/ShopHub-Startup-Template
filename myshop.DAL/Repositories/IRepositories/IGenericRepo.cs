using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories.IRepositories
{
    public interface IGenericRepo<TEntity>where TEntity : class
    {
        public Task<ICollection<TEntity>> GetAll( params Expression<Func<TEntity, object>>[] includes);
        public Task<TEntity> GetById(int id, params Expression<Func<TEntity, object>>[] includes);
        public Task Add(TEntity entity);
        public void Update(TEntity entity);
        public Task SaveChanges();
    }
}
