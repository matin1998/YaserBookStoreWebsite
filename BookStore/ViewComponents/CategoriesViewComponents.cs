using BookStore.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.ViewComponents;

public class CategoriesViewComponent : ViewComponent
{
    private readonly ICategoryService _categoryService;

    public CategoriesViewComponent(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public IViewComponentResult Invoke()
    {
        var categories = _categoryService.GetListOFCategories();
        return View(categories);
    }
}
