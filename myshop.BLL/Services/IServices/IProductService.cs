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
    public interface IProductService
    {
        public Task<ICollection<ProductDto>> GetProducts();
        public Task<ProductDetailsDto> GetProductDetails(int ProductId);
        public Task Create(Product product, IFormFile file);
        public Task Edit(Product product, IFormFile file);
        public Task Delete(int ProductId);



    }
}
