﻿using BookStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task AddBookToDataBase(Book book);
        List<string> GetListOFBooks();

        Task<Book> GetABookByIdAsync(int bookId);

        Task EditABook(Book book);

        Task DeleteABook(Book book);
    }
}
