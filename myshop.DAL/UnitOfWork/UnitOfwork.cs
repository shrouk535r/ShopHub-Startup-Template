using myshop.DAL.Repositories;
using myshop.DAL.Repositories.IRepositories;
using myshop.DAL.UnitOfWork.Interfaces;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.UnitOfWork
{
    internal class UnitOfwork : IUnitOfWork
    {
        public IProductRepo ProductRepository { get; }
        public ICategoryRepo CategoryRepository { get; }
        private ApplicationDbContext _companyContext;

        public UnitOfwork(ApplicationDbContext companycontext,
            IProductRepo productRepository,
            ICategoryRepo categoryRepository

            )
        {
            _companyContext = companycontext;
            ProductRepository = productRepository; ;
            CategoryRepository = categoryRepository;
        }

        public async Task Save()
        {
            await _companyContext.SaveChangesAsync();
        }
    }
}
