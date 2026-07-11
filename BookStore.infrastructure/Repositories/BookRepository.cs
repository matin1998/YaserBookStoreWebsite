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

        public async Task DeleteABook(int bookId)
        {
            var book = await GetABookByIdAsync(bookId);
            if (book == null)
                return;
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task EditABook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book?> GetABookByIdAsync(int bookId)
        {
            return await _context.Books.Include(i => i.Images).FirstOrDefaultAsync(p=>p.Id==bookId);
        }

        public List<Book> GetListOfBooks()
        {
            return _context.Books.Include(b => b.Category).Include(i => i.Images).AsSplitQuery().ToList();
        }

    }
}
