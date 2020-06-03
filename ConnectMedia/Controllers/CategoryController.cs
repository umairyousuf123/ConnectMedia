using ConnectMedia.Common.IServices;
using ConnectMedia.Common.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ConnectMedia.Controllers
{
  //  // (AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)
  [Authorize]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            this._categoryService = categoryService;
        }
        [HttpGet("Index")]
        public IActionResult Index()
        {
            List<Category> categories = new List<Category>();
            categories = _categoryService.CategoryList(getCurrentUserId());
            return View(categories);
        }

        [HttpGet("Create")]
        public IActionResult Create(int Id)
        {
            try
            {
                Category category = new Category();
                if (Id > 0)
                {
                    category = _categoryService.CategoryDetailById(Id);
                }
                return View(category);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Url", ex.Message);
                return RedirectToAction("Index");
            }
        }
        [HttpPost("Create")]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id > 0)
                {
                    category.UpdatedBy = getCurrentUserId();
                    category.UpdatedOn = DateTime.Now;
                }
                else
                {
                    category.CreatedBy = getCurrentUserId();
                    category.CreatedOn = DateTime.Now;
                }
                _categoryService.AddEditCategory(category);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("Delete")]
        public IActionResult Delete(int Id)
        {
            if (Id > 0)
            {
                int CurrentUserId = getCurrentUserId();
                _categoryService.Delete(Id, CurrentUserId);
            }
            return RedirectToAction("Index");
        }
    }
}