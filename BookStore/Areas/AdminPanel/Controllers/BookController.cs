using BookStore.Application.DTOs.AdminSide.Books;
using BookStore.Application.Services.Implementations;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Presentation.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class BookController : AdminBaseController
{
    #region Ctor

    private readonly IBookService _bookService;
    private readonly ICategoryService _categoryService;
    public BookController(IBookService bookService, ICategoryService categoryService)
    {
        _bookService = bookService;
        _categoryService = categoryService;
    }

    #endregion

    #region List Of books

    [HttpGet]
    public IActionResult ListOfBooks()
    {
        var model = _bookService.GetListOFBooks();

        return View(model);
    }

    #endregion

    #region Create A book

    [HttpGet]
    public IActionResult CreateABook()
    {
        var model = new BookDTO
        {
            Categories = _categoryService.GetListOFCategories().ToList()
        };
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateABook(BookDTO model)
    {
        if (ModelState.IsValid)
        {
            await _bookService.AddBookToDataBase(model);

            return RedirectToAction(nameof(ListOfBooks));
        }

        return View();
    }

    #endregion

    #region Edit A Book

    [HttpGet]
    public async Task<IActionResult> EditABook(long bookId)
    {
        #region Get A book By Id

        var book = await _bookService.GetABookByIdAsync(bookId);
        var model = new EditBookDTO
        {
            Id = book.Id,
            BookTitle = book.BookTitle,
            BookPrice = book.BookPrice,
            BookInventory = book.BookInventory,
            BookDescription = book.BookDescription,
            Categories = _categoryService.GetListOFCategories().ToList(),
            CategoryId = book.CategoryId
        };

        #endregion

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> EditABook(EditBookDTO model)
    {
        #region Update A book

        await _bookService.EditABook(model);

        return RedirectToAction(nameof(ListOfBooks));

        #endregion
    }

    #endregion

    #region Delete A book

    [HttpGet]
    public async Task<IActionResult> DeleteAbook(int bookId)
    {
        #region Get A book By Id

        var book = await _bookService.GetABookByIdAsync(bookId);

        #endregion

        return View(book);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteABook(int bookId)
    {
        #region Update A book

        await _bookService.DeleteABook(bookId);

        return RedirectToAction(nameof(ListOfBooks));

        #endregion
    }
    #endregion
}

