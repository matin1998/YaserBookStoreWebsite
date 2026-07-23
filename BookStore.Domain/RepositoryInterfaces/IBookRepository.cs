using BookStore.Domain.Entities;

namespace BookStore.Domain.RepositoryInterfaces;

public interface IBookRepository : IBaseRepository<Book>
{
    List<Book> GetListOfBooks();

    Task AddBookToDataBase(Book book);
    Task<Book?> GetABookByIdAsync(long bookId);

    Task EditABook(Book book);

    Task DeleteABook(long bookId);
}
