using AutoMapper;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Extensions.Configuration;
using myshop.BLL.DTO;
using myshop.DAL.Enums;
using myshop.DAL.Models;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category == null ? "" : src.Category.Name));
            CreateMap<Product, ProductDetailsDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category == null ? "" : src.Category.Name))
                .ForMember(dest => dest.ReviewsCount, opt => opt.MapFrom(src => src.Reviews == null ? 0 : src.Reviews.Count))
                .ForMember(dest => dest.ReviewRating, opt => opt.MapFrom(src => src.Reviews == null || !src.Reviews.Any() ? 0m : (decimal)src.Reviews.Average(r => r.Value)));

            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Products != null && src.Products.Any() ? src.Products.FirstOrDefault().Img : "/Images/Categories/DefaultCategory.jpg"));
            CreateMap<Product, ProductInCategoryDto>();
            CreateMap<Category, CategoryDetailsDto>();

         //Shopping Cart Dtos
            CreateMap<CartItem, ShoppingCartItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.product.Price))
                .ForMember(dest => dest.productImg, opt => opt.MapFrom(src => src.product.Img));

            CreateMap<ShoppingCart, ShoppingCartDto>();


            //Order Dtos

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.OrderHeader.UserName))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderHeader.OrderDate))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderHeader.OrderStatus))
                .ForMember(dest => dest.ShippingDate, opt => opt.MapFrom(src => src.OrderHeader.ShippingDate));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.product.Price))
                .ForMember(dest => dest.productImg, opt => opt.MapFrom(src => src.product.Img));

            CreateMap<Order, OrderDetailsDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.OrderHeader.UserName))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.OrderHeader.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.OrderHeader.City))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.OrderHeader.PhoneNumber))
                .ForMember(dest => dest.MethodType, opt => opt.MapFrom(src => src.OrderHeader.MethodType))
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.OrderHeader.PaymentDate))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.OrderHeader.PaymentStatus))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderHeader.OrderDate))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderHeader.OrderStatus))
                .ForMember(dest => dest.ShippingDate, opt => opt.MapFrom(src => src.OrderHeader.ShippingDate));

            
                
               
                //Chechout
            CreateMap<ShoppingCart,CheckoutDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<CartItem, OrderItem>();

            CreateMap<CheckoutDto, OrderHeader>()
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.MethodType == PaymentMethod.CashOnDelivary ? src.ShippingDate : DateTime.Now));
            //- ApplicationUserId
            //check username



            CreateMap<ShoppingCart,Order>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.TotalPrice));
            //-order headerid



            //Review Dtos
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName));


        }
    }
}
