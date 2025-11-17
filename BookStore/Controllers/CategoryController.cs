using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]
        public async Task<IActionResult> AddCategoryToDataBase(Category category)
        {
            await _categoryService.AddCategoryToDataBase(category);
            return RedirectToAction(nameof(GetListOFCategories));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteACategory(Category category)
        {
            await _categoryService.DeleteACategory(category);
            return RedirectToAction(nameof(GetListOFCategories));
        }
        [HttpPut]
        public async Task<IActionResult> EditACategory(Category category)
        {
            await _categoryService.EditACategory(category);
            return RedirectToAction(nameof(GetListOFCategories));
        }
        [HttpGet("id")]
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
        }
        [HttpGet]
        public ActionResult<List<string>> GetListOFCategories()
        {
            return _categoryService.GetListOFCategories();
        }

    }
}
