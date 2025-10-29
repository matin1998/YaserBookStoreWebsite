using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using BookStore.infrastructure.YaserBookStoreDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        #region Ctor

        private readonly BookStoreDbContext _context;

        public BookRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        #endregion
        public async Task AddBookToDataBase(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteABook(Book book)
        {
             _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task EditABook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public Task<Book> GetABookByIdAsync(int bookId)
        {
            return _context.Books.FirstOrDefaultAsync(p=>p.Id==bookId);
        }

        public List<string> GetListOfBooks()
        {
            return _context.Books
                .Select(b=>b.BookTitle)
                .ToList();
        }

    }
}
