using BookStore.Application.DTOs.AdminSide.Books;
using BookStore.Application.Services.Implementations;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Areas.AdminPanel.Controllers
{

    public class CategoryController:AdminBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult ListOfCategories()
        {
            var model = _categoryService.GetListOFCategories();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            var model = new Category();
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _categoryService.AddCategoryToDataBase(category);
            return RedirectToAction(nameof(ListOfCategories));
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAcategory(int categoryId)
        {
            #region Get A Category By Id

            var category = await _categoryService.GetACategoryByIdAsync(categoryId);

            #endregion

            return View(category);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteACategory(int categoryId)
        {
            
            var category = await _categoryService.GetACategoryByIdAsync(categoryId);
            await _categoryService.DeleteACategory(category);

            return RedirectToAction(nameof(ListOfCategories));
        }
        [HttpGet]
        public async Task<IActionResult> EditAcategory(int categoryId)
        {
            #region Get A book By Id

            var category = await _categoryService.GetACategoryByIdAsync(categoryId);

            #endregion

            return View(category);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditACategory(int categoryId)
        {
            var category = await _categoryService.GetACategoryByIdAsync(categoryId);
            await _categoryService.EditACategory(category);
            return RedirectToAction(nameof(ListOfCategories));
        }
        /*[HttpGet("id")]
        public async Task<ActionResult<Category>> GetACategoryByIdAsync(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid Id");
            }
            Category category = await _categoryService.GetACategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            return category;
        }*/

    }
}
