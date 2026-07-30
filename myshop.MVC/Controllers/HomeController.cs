using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Services;
using myshop.BLL.Services.IServices;
using myshop.DAL.Enums;
using myshop.Entities.Models;
using myshop.MVC.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace myshop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger,
            ICategoryService categoryService,
            IProductService productService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Admin"))
                    return (RedirectToAction("Index", "Product"));
                var CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            }

            var categories = (await _categoryService.GetCategories()).Take(3);
            var products = (await _productService.GetProducts()).Take(8);
            HomeVM homeVM = new HomeVM
            {
                Products = products,
                Categories = categories
            };
            return View(homeVM);
        }
        public async Task<IActionResult> Products(int? Pagenum, SortEnum? order,string SearchedText)
        {
            var products = _productService.GetFilteredAndSortedProducts(Pagenum, order, SearchedText);
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}