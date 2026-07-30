using Microsoft.AspNetCore.Http;
using myshop.BLL.DTO;
using myshop.DAL.Enums;
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
        public bool ValidateImageFile(IFormFile File, out string ErrMsg);

        public Task<ICollection<ProductDto>> GetFilteredAndSortedProducts(int? Pagenum, SortEnum? order, string SearchedText);


    }
}
