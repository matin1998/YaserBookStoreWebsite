using BookStore.Domain.Entities;

namespace BookStore.Domain.RepositoryInterfaces;

public interface IBookRepository
{
    List<Book> GetListOfBooks();

    Task AddBookToDataBase(Book book);

    Task<Book> GetABookByIdAsync(int bookId);

    Task EditABook(Book book);

    Task DeleteABook(Book book);
}
