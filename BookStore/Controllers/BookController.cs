using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Controllers
{
    public class BookController : Controller
    {
        
        private readonly ICategoryService _categoryService;
        private readonly IBookService _bookService;
        public BookController (ICategoryService categoryService , IBookService bookService)
        {
            _categoryService = categoryService;
            _bookService = bookService;
        }
        // GET: BookController
        public ActionResult Categories()
        {
            var model = _categoryService.GetListOFCategories();
            return View(model);
        }
        public ActionResult Category(int categoryId)
        {
            //باید کتاب های یک دسته خاص را باز کند
            var books= _bookService.GetListOfBooksByCategoryId(categoryId);
            return View(books);
        }
        public async Task<IActionResult> Details(int bookId)
        {
            var book = await _bookService.GetABookByIdAsync(bookId);
            return View(book);
        }
    }
}
