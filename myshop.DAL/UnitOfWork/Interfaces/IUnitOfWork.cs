using myshop.DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        public IProductRepo ProductRepository { get; }
        public ICategoryRepo CategoryRepository { get; }
        public Task Save();
    }
}
