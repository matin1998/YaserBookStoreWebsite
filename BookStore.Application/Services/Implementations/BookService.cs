using BookStore.Application.DTOs.AdminSide.Books;
using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting; // Ensure this is already present
using Microsoft.Extensions.Hosting;
namespace BookStore.Application.Services.Implementations;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IImageService _imageService;

    public BookService(
        IBookRepository bookRepository,
        IImageService imageService)
    {
        _bookRepository = bookRepository;
        _imageService = imageService;
    }
    public async Task AddBookToDataBase(BookDTO model)
    {
        #region Object Mapping
        Book book = new Book();
        book.BookTitle = model.BookTitle;
        book.BookDescription = model.BookDescription;
        book.BookPrice = model.BookPrice;
        book.BookInventory = model.BookInventory;
        book.CategoryId = model.CategoryId;
        //book.Category = model.Categories.FirstOrDefault(c => c.Id == model.CategoryId);
        #endregion
        List<Book> books = _bookRepository.GetListOfBooks();
        var existingBook = books.FirstOrDefault(b => b.BookTitle == model.BookTitle);
        if (existingBook != null)
        {
            existingBook.BookInventory++;
            await _bookRepository.EditABook(existingBook);
        }
        else {
            await _bookRepository.AddBookToDataBase(book);
        }
        foreach (var image in model.Images)
        {
            await _imageService
                .AddImageAsync(image, book.Id);
        }
    }

    public async Task DeleteABook(int bookId)
    {
        await _imageService.DeleteImagesByBookIdAsync(bookId);
        await _bookRepository.DeleteABook(bookId);
    }

    public async Task EditABook(EditBookDTO model)
    {
        Book book =
        await _bookRepository
            .GetABookByIdAsync(model.Id);

        if (book == null)
            throw new Exception("Book not found");

        book.BookTitle = model.BookTitle;
        book.BookDescription = model.BookDescription;
        book.BookPrice = model.BookPrice;
        book.BookInventory = model.BookInventory;
        book.CategoryId = model.CategoryId;

        await _bookRepository.EditABook(book);

        if (model.NewImages != null &&
            model.NewImages.Any())
        {
            foreach (var image in model.NewImages)
            {
                await _imageService
                    .AddImageAsync(image, book.Id);
            }
        }
    }

    public async Task<Book> GetABookByIdAsync(int bookId)
    {
        Book book=await _bookRepository.GetABookByIdAsync(bookId);
        return book;
        //Console.WriteLine(book.BookTitle+" "+book.BookPrice+" "+book.BookInventory+" "+book.BookDescription);
    }

    public List<Book> GetListOFBooks()
    {
       return _bookRepository.GetListOfBooks();
    }

    public List<Book> GetListOfBooksByCategoryId(int categoryId)
    {
       return _bookRepository.GetListOfBooks()
            .Where(b => b.CategoryId == categoryId).ToList();
    }
}
