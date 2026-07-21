using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using myshop.BLL.DTO;
using myshop.BLL.Services.IServices;
using myshop.DAL.Enums;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = roles.FirstOrDefault() ?? "Customer",
                    IsLocked = user.LockoutEnd != null && user.LockoutEnd > System.DateTimeOffset.Now
                });
            }

            return userList;
        }

        public async Task<bool> SwitchRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                await _userManager.AddToRoleAsync(user, "Customer");
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, "Customer");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return true;
        }

        public async Task<bool> ToggleLockout(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            if (user.LockoutEnd != null && user.LockoutEnd > System.DateTimeOffset.Now)
            {
                await _userManager.SetLockoutEndDateAsync(user, null);
            }
            else
            {
                await _userManager.SetLockoutEndDateAsync(user, System.DateTimeOffset.MaxValue);
            }

            return true;
        }

        public async Task<bool> DeleteUser(string userId, string currentAdminId)
        {
            if (userId == currentAdminId) return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;
            if (user.Role == RoleEnum.Admin)
                return false;
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
    }
}
