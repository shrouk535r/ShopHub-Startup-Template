using Microsoft.AspNetCore.Http;
using myshop.BLL.DTO;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services.IServices
{
    public interface ICategoryService
    {
        public Task<ICollection<CategoryDto>> GetCategories();
        public Task<CategoryDetailsDto> GetCategoryDetails(int categoryId);
        public Task Create(Category category);
        public Task Edit(Category category);
        public Task Delete(int categoryId);
    }
}
