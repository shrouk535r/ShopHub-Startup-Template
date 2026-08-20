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
        public async Task<ICollection<ProductDto>> GetArchievedProducts()
        {
            var products = await _unitOfWork.ProductRepository.GetArchievedProducts();
            var productsDto = products.Select(P => _mapper.Map<ProductDto>(P)).ToList();
            return productsDto;
        }
        public async Task<(ICollection<ProductDto> items, int total)> GetFilteredAndSortedProducts(int? Pagenum,SortEnum? order,string SearchedText)
        {
            int pageSize = 8;
            Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = order switch 
            {
                SortEnum.NameAsec => q => q.OrderBy(p => p.Name),
                SortEnum.NameDesc => q => q.OrderByDescending(p => p.Name),
                SortEnum.PriceAsec => q => q.OrderBy(p => p.Price),
                SortEnum.PriceDesc => q => q.OrderByDescending(p => p.Price),
                _ => null            
            };
            var (products, totalcount) = await _unitOfWork.ProductRepository.GetFilteredProducts(pageSize, Pagenum ?? 1, SearchedText ?? "",orderBy);


            var productsDto = products.Select(P => _mapper.Map<ProductDto>(P)).ToList();

            return (productsDto,totalcount);
        }
        public async Task RestoreProduct(int ProductId)
        {
            var product = await _unitOfWork.ProductRepository.GetArchievedProductBbyId(ProductId);
            if (product == null)
                throw new Exception("Product with This Id Not Found");
            if(!product.IsDeleted)
                throw new Exception("Product with This Id Not Deleted");
            await _unitOfWork.ProductRepository.RestoreProduct(product);
            await _unitOfWork.Save();
        }


        public async Task<ProductDetailsDto> GetProductDetails(int ProductId)
        {
            var product=await _unitOfWork.ProductRepository.GetProductDetailById(ProductId);
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
