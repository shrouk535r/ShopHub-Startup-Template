using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Repositories.IRepositories
{
    public interface ICategoryRepo:IGenericRepo<Category>,IDeleteRepo<Category>
    {
    }
}
