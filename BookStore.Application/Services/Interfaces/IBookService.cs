using BookStore.Application.DTOs.AdminSide.Book;
using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task AddBookToDataBase(BookDTO model);
        List<Book> GetListOFBooks();

        List<Book> GetListOfBooksByCategoryId(int categoryId);

        Task<Book> GetABookByIdAsync(int bookId);

        Task EditABook(EditBookDTO book);

        Task DeleteABook(Book book);
    }
}
