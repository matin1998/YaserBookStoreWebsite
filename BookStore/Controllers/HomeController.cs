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



    public async Task AddBookToDataBase(Book book)
    {
       await _bookService.AddBookToDataBase(book);
    }
    public async Task DeleteABook(Book book)
    {
        await _bookService.DeleteABook(book);
    }
    public async Task EditABook(Book book)
    {
        await _bookService.EditABook(book);
    }
    public async Task<Book> GetABookByIdAsync(int bookId)
    {
        Book book = await _bookService.GetABookByIdAsync(bookId);
        return book;
    }
    public List<string> GetListOFBooks()
    {
        return _bookService.GetListOFBooks();
    }
}
