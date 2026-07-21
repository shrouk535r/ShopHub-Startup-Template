using myshop.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.BLL.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<bool> SwitchRole(string userId);
        Task<bool> ToggleLockout(string userId);
        Task<bool> DeleteUser(string userId, string currentAdminId);
    }
}
