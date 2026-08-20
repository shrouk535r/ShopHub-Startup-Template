using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myshop.BLL.Services;
using myshop.BLL.Services.IServices;
using myshop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL
{
    public static class dependencyInjection
    {
        public static IServiceCollection AddBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDAL(configuration);
            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService,CategoryService >();
            services.AddScoped<IFileService,FileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}