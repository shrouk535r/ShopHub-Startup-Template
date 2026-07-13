using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories.IRepositories
{
    public interface IDeleteRepo<TEntity>where TEntity : class
    {
        public Task<bool> Delete(TEntity entity);

    }
}
