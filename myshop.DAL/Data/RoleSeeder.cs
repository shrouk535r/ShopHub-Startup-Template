using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using myshop.DAL.Enums;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { RoleEnum.Admin.ToString(), RoleEnum.Customer.ToString() };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            string adminEmail = "admin@myshop.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FName = "Admin",
                    LName = "Shrouk",
                    Address = "Cairo",
                    City = "Cairo",
                    Role = RoleEnum.Admin
                };

                var result = await userManager.CreateAsync(newAdmin, "Admin123$");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, RoleEnum.Admin.ToString());
                }
            }
        }
    }
}
