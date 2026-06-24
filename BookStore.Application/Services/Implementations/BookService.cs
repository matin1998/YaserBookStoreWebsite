using BookStore.Application.DTOs.AdminSide.Book;
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
using BookStore.Application.Interfaces; // Ensure this is already present

namespace BookStore.Application.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IFileService _fileService;
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository, IFileService fileService) 
        {
            _bookRepository = bookRepository;
            _fileService = fileService;
        }
        public async Task AddBookToDataBase(BookDTO model)
        {
            string imageName = "";

            if (model.ImageFile != null)
            {
                imageName = await _fileService.SaveImageAsync(model.ImageFile);
            }
            #region Object Mapping

            Book book = new Book();
            book.BookTitle = model.BookTitle;
            book.BookDescription = model.BookDescription;
            book.BookPrice = model.BookPrice;
            book.BookInventory = model.BookInventory;
            book.CategoryId = model.CategoryId;
            book.Category = model.Categories.FirstOrDefault(c => c.Id == model.CategoryId);
            book.ImageName = imageName;
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
        }

        public async Task DeleteABook(Book book)
        {
            // حذف فایل عکس
            if (!string.IsNullOrEmpty(book.ImageName))
            {
                _fileService.DeleteImage(book.ImageName);
            }
            await _bookRepository.DeleteABook(book);
        }

        public async Task EditABook(EditBookDTO model)
        {
            Book book = await _bookRepository.GetABookByIdAsync(model.Id);
            if (model.ImageFile != null)
            {
                // حذف عکس قبلی
                if (!string.IsNullOrEmpty(book.ImageName))
                {
                    _fileService.DeleteImage(book.ImageName);
                }
                // ثبت نام عکس جدید در دیتابیس
                book.ImageName = await _fileService.SaveImageAsync(model.ImageFile);
                
            }
            await _bookRepository.EditABook(book);
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
}
