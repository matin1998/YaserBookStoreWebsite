using 
namespace BookStore.Domain.RepositoryInterfaces;

public interface IBookRepository
{
    List<Education> GetListOfBooks();

    Task AddBookToDataBase(Book education);

    Task<Education> GetABookByIdAsync(int educationId);

    Task EditABook(Education education);

    Task DeleteABook(Education education);
}
