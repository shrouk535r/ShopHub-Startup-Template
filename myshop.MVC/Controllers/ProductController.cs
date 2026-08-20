using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using myshop.BLL.DTO;
using myshop.BLL.Services.IServices;
using myshop.DAL.Repositories.IRepositories;
using myshop.DAL.UnitOfWork.Interfaces;
using myshop.DataAccess;
using myshop.Entities.Models;
using myshop.Entities.ViewModels;

namespace myshop.MVC.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdminOnly")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IProductService _productService;
        public IUnitOfWork _unitOfWork;
        public ProductController( IWebHostEnvironment webHostEnvironment, IProductService productService,IUnitOfWork unitOfWork)
        {
            _webHostEnvironment = webHostEnvironment;
            _productService = productService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(bool IsArchieved=false)
        {
            ViewBag.IsArchieved = IsArchieved;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData(bool IsArchieved=false)
        {
            IEnumerable<ProductDto> products;
            if (!IsArchieved)
            {
                products = await _productService.GetProducts();
            }
            else
            {
                products = await _productService.GetArchievedProducts();
            }
            return Json(new { data = products, IsArchieved });
        }
        [HttpGet]
        public async Task<IActionResult> GetDetails(int ?id)
        {
            ProductDetailsDto product;
            if (id == null)
                return Json(new { success = false, message = "Error while getting Product Details" });
            try
            {
                product = await _productService.GetProductDetails(id ?? 0);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = $"Error while getting Product Details- {ex.Message}" });

            }
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int ? categoryid)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product { CategoryId = categoryid ?? 0 },
                CategoryList = (await _unitOfWork.CategoryRepository.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                })
            };
            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM productVM)
        {

            if (!_productService.ValidateImageFile(productVM.File, out string err))
                ModelState.AddModelError("File", err);
            if (ModelState.IsValid)
            {
                await _productService.Create(productVM.Product, productVM.File);
                TempData["Create"] = "Item has Created Successfully";
                return RedirectToAction("Index");
            }
            productVM.CategoryList = (await _unitOfWork.CategoryRepository.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            return View(productVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            ProductVM productVM = new ProductVM()
            {
                Product =await _unitOfWork.ProductRepository.GetById(id??0),
                CategoryList = (await _unitOfWork.CategoryRepository.GetAll()).Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
            };

            return View(productVM);
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(ProductVM productVM)
        {
            if (!_productService.ValidateImageFile(productVM.File, out string err))
                ModelState.AddModelError("File", err);
            if (ModelState.IsValid)
            {
                await _productService.Edit(productVM.Product, productVM.File);
                TempData["Update"] = "Data has Updated Successfully";
                return RedirectToAction("Index");
            }
            productVM.CategoryList = (await _unitOfWork.CategoryRepository.GetAll()).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });
            return View(productVM);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id==null)
                return Json(new { success = false, message = "Error while Deleting" });

            try
            {
                await _productService.Delete(id??0);
            }
            catch(Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true, message = "file has been Deleted" });
        }
        [HttpPost]
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
                return Json(new { success = false, message = "Error while Restoring" });

            try
            {
                await _productService.RestoreProduct(id ?? 0);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true, message = "file has been Restore Successfully" });
        }

    }
}
