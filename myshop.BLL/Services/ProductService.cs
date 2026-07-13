using AddressBook.BLL.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using myshop.BLL.DTO;
using myshop.BLL.Services.IServices;
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
