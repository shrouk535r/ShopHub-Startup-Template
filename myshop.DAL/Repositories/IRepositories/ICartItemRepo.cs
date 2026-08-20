using myshop.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories.IRepositories
{
    public interface ICartItemRepo:IGenericRepo<CartItem>,IDeleteRepo<CartItem>
    {
        public Task<IEnumerable<CartItem>> GetbyCartId(int CartId);
       
    }
}
