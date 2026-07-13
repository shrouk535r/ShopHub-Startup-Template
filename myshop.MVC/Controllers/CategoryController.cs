using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTO;
using myshop.BLL.Services.IServices;
using myshop.DAL.UnitOfWork.Interfaces;
using myshop.DataAccess;
using myshop.Entities.Models;

namespace myshop.MVC.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryServices;
        public IUnitOfWork _unitOfWork;

        public CategoryController(ICategoryService categoryService, IUnitOfWork unitOfWork)
        {
            _categoryServices = categoryService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetCategories();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> GetDetails(int? id)
        {
            CategoryDetailsDto category;
            if (id == null)
                return Json(new { success = false, message = "Error while getting Category Details" });
            try
            {
                category = await _categoryServices.GetCategoryDetails(id ?? 0);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error while getting Category Details- {ex.Message}" });

            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryServices.Create(category);
                TempData["Create"] = "Item has Created Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }
            var categoryIndb = await _unitOfWork.CategoryRepository.GetById(id??0);

            return View(categoryIndb);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryServices.Edit(category);

                TempData["Update"] = "Data has Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null | id == 0)
        //    {
        //        NotFound();
        //    }
        //    var categoryIndb = _context.Categories.Where(x => x.Id == id).FirstOrDefault();

        //    return View(categoryIndb);
        //}

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return Json(new { success = false, message = "Error while Deleting" });

            try
            {
                await _categoryServices.Delete(id ?? 0);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            TempData["Delete"] = "Item has Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
