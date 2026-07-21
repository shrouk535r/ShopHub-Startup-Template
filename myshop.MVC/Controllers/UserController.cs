using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Services.IServices;
using System.Security.Claims;

namespace myshop.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> SwitchRole(string id)
        {
            await _userService.SwitchRole(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ToggleLock(string id)
        {
            await _userService.ToggleLockout(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var currentAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _userService.DeleteUser(id, currentAdminId);
            return RedirectToAction(nameof(Index));
        }
    }
}
