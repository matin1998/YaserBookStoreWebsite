using BookStore.Domain.Entities;
using BookStore.Domain.RepositoryInterfaces;
using BookStore.infrastructure.YaserBookStoreDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.infrastructure.Repositories;

public class BookRepository : BaseRepository<Book>,IBookRepository
{
    #region Ctor
    public BookRepository(BookStoreDbContext context)
        : base(context)
    {

    }
    #endregion
    public async Task AddBookToDataBase(Book book)
    {
        await AddAsync(book);
    }

    public async Task DeleteABook(long bookId)
    {
        var book = await GetABookByIdAsync(bookId);
        if (book == null)
            return;
        await DeleteAsync(book);
    }

    public async Task EditABook(Book book)
    {
        await UpdateAsync(book);
    }

    public async Task<Book?> GetABookByIdAsync(long bookId)
    {
        return await _context.Books.Include(i => i.Images).FirstOrDefaultAsync(p=>p.Id==bookId);
    }

    public List<Book> GetListOfBooks()
    {
        return _context.Books.Include(b => b.Category).Include(i => i.Images).AsSplitQuery().ToList();
    }

}
