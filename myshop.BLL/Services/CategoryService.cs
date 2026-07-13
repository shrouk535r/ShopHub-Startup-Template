using AutoMapper;
using Microsoft.AspNetCore.Http;
using myshop.BLL.DTO;
using myshop.BLL.Services.IServices;
using myshop.DAL.UnitOfWork.Interfaces;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    internal class CategoryService:ICategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ICollection<CategoryDto>> GetCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll(c => c.Products);
            var categoriesDto = categories.Select(c => _mapper.Map<CategoryDto>(c)).ToList();
            return categoriesDto;
        }
        public async Task<CategoryDetailsDto> GetCategoryDetails(int categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(categoryId,c =>c.Products);
            if (category == null)
            {
                throw new Exception("There is no Category for this Id");
            }
            return _mapper.Map<CategoryDetailsDto>(category);
        }
        public async Task Create(Category category)
        {
            await _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.Save();
        }
        public async Task Edit(Category category)
        {
            
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.Save();
        }

        public async Task Delete(int categoryId)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(categoryId);
            if (category == null)
                throw new Exception("Error while Deleting- the Category NOT FOUND");
            
            await _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.Save();

        }
    }
}
