using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myshop.DAL.Repositories;
using myshop.DAL.Repositories.IRepositories;
using myshop.DAL.UnitOfWork;
using myshop.DAL.UnitOfWork.Interfaces;
using myshop.DataAccess;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            configuration.GetConnectionString("MyShopDb")
            ));
            services.AddIdentity<ApplicationUser, IdentityRole>(
                options => options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(4)
                ).AddDefaultTokenProviders().AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped(typeof(IDeleteRepo<>), typeof(DeleteRepo<>));
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICategoryRepo, CategoryRepo>();
            services.AddScoped<IUnitOfWork, UnitOfwork>();



            return services;
        }
    }
}
