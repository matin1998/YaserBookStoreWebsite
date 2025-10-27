using System.Diagnostics;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities.Book;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.Controllers;
[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly IBookService _bookService;

    public HomeController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpPost]
    public async Task<IActionResult> AddBookToDataBase(Book book)
    {
       await _bookService.AddBookToDataBase(book);
        return RedirectToAction(nameof(GetListOFBooks));
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteABook(Book book)
    {
        await _bookService.DeleteABook(book);
        return RedirectToAction(nameof(GetListOFBooks));
    }
    [HttpPut]
    public async Task<IActionResult> EditABook(Book book)
    {
        await _bookService.EditABook(book);
        return RedirectToAction(nameof(GetListOFBooks));
    }
    [HttpGet("id")]
    public async Task<ActionResult<Book>> GetABookByIdAsync(int id)
    {
        if (id < 0) 
        {
            return BadRequest("Invalid Id");
        }
        Book book = await _bookService.GetABookByIdAsync(id);
        if (book == null) 
        {
            return NotFound();
        }

        return book;
    }
    [HttpGet]
    public ActionResult<List<string>> GetListOFBooks()
    {
         return  _bookService.GetListOFBooks();
    }
}
