using AutoMapper;
using Microsoft.Extensions.Configuration;
using myshop.BLL.DTO;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.Mapping
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category == null ? "" : src.Category.Name));
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category == null ? "" : src.Category.Name));
            CreateMap<Category, CategoryDto>();
            CreateMap<Product, ProductInCategoryDto>();
            CreateMap<Category, CategoryDetailsDto>();
                
            //CreateMap<AddressBookEntryCreateDto, AddressBookEntry>()
            //    .ForMember(dest => dest.photo, opt => opt.Ignore());

            //CreateMap<Department, DepartmentDto>();
            //CreateMap<DepartmentDto, Department>()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore());

            //CreateMap<Job, JobDto>();
            //CreateMap<JobDto, Job>()
            //    .ForMember(dest => dest.Id, opt => opt.Ignore());


        }
    }
}
