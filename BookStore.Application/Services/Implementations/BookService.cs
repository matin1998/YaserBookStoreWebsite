using BookStore.Application.Services.Interfaces;
using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository) 
        {
            _bookRepository = bookRepository;
        }
        public async Task AddBookToDataBase(Book book)
        {
            List<string> booksNames = _bookRepository.GetListOfBooks();
            bool flag = false;
            foreach (var b in booksNames) {
                if (b == book.BookTitle)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                book.BookInventory++;
                await _bookRepository.EditABook(book);
            }
            else {
                await _bookRepository.AddBookToDataBase(book);
            }
        }

        public async Task DeleteABook(Book book)
        {
            await _bookRepository.DeleteABook(book);
        }

        public async Task EditABook(Book book)
        {
            await _bookRepository.EditABook(book);
        }

        public async Task<Book> GetABookByIdAsync(int bookId)
        {
            Book book=await _bookRepository.GetABookByIdAsync(bookId);
            return book;
            //Console.WriteLine(book.BookTitle+" "+book.BookPrice+" "+book.BookInventory+" "+book.BookDescription);
        }

        public List<string> GetListOFBooks()
        {
           return _bookRepository.GetListOfBooks();
        }
    }
}
