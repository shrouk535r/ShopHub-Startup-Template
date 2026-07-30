using AddressBook.BLL.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using myshop.BLL.DTO;
using myshop.BLL.Services.IServices;
using myshop.DAL.Enums;
using myshop.DAL.UnitOfWork.Interfaces;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace myshop.BLL.Services
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IFileService _fileService;
        public ProductService(IUnitOfWork unitOfWork,IMapper mapper, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<ICollection<ProductDto>> GetProducts()
        {
            var products = await _unitOfWork.ProductRepository.GetAll(p => p.Category);
            var productsDto = products.Select(P => _mapper.Map<ProductDto>(P)).ToList();
            return productsDto;
        }
        public async Task<ICollection<ProductDto>> GetFilteredAndSortedProducts(int? Pagenum,SortEnum? order,string SearchedText)
        {
            int pageSize = 10;
            var products = await _unitOfWork.ProductRepository.GetAll(p => p.Category);
            int skippedPages = (Pagenum??1 - 1) * pageSize;
            switch(order)
            {
                case SortEnum.NameAsec:
                    products= products.OrderBy(p => p.Name).ToList();
                    break;
                case SortEnum.NameDesc:
                    products = products.OrderByDescending(p => p.Name).ToList();
                    break;
                case SortEnum.PriceAsec:
                    products = products.OrderBy(p => p.Price).ToList();
                    break;
                case SortEnum.PriceDesc:
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(SearchedText))
                products = products.Where(p => p.Name.ToLower().Contains(SearchedText.ToLower()) || p.Description.ToLower().Contains(SearchedText.ToLower())).ToList();
            products = products.Skip(skippedPages).Take(pageSize).ToList();
            var productsDto = products.Select(P => _mapper.Map<ProductDto>(P)).ToList();

            return productsDto;
        }
        


        public async Task<ProductDetailsDto> GetProductDetails(int ProductId)
        {
            var product=await _unitOfWork.ProductRepository.GetById(ProductId, p => p.Category);
            if (product == null)
            {
                throw new Exception("there is no Product for this Id");
            }
            return _mapper.Map<ProductDetailsDto>(product);
        }
        public async Task Create(Product product, IFormFile file)
        {
            string? uploadpath = _fileService.uploadFile(file, @"Images\Products\");
            product.Img = uploadpath ?? null;
            await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.Save();
        }
        public async Task Edit(Product product, IFormFile file)
        {
            if (file != null)
            {
                if (product.Img != null)
                {
                    _fileService.DeleteFIle(product.Img);
                 
                }
                string? uploadpath = _fileService.uploadFile(file, @"Images\Products\");
                product.Img = uploadpath;
            }
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Save();
        }
        public bool ValidateImageFile(IFormFile File, out string ErrMsg)
        {
            ErrMsg = "";
            if (File != null)
            {
                
                float fileLenInMb = File.Length / (1024f * 1024f);
                string fileType = File.ContentType.Split('/')[0];
                string fileExt = File.ContentType.Split('/')[1].ToLower();
                if (fileLenInMb > 2)
                {
                    ErrMsg = "Invalid file. The file must not exceed 2 MB.";
                    return false;
                }
                else if ((fileType != "image") || (fileExt != "jpg" && fileExt != "jpeg" && fileExt != "png" && fileExt != "webp"))
                {
                    ErrMsg = "Invalid file. The file must be in JPG, JPEG, PNG, or WebP format.";
                    return false;
                }
                
            }
           
            return true;

        }
        public async Task Delete(int ProductId)
        {
            var product = await _unitOfWork.ProductRepository.GetById(ProductId);
            if (product == null)
                throw new Exception("Error while Deleting- the product NOT FOUND");
            if (product.Img != null)
            {
                _fileService.DeleteFIle(product.Img);

            }
            await _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.Save();

        }
    }
}
