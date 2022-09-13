using Laebrary.Models;

namespace Laebrary.Repositories
{
    public interface IBookRepository
    {
        Task AddBook(Book book);
        Task<Book> GetBookById(int id);

    }
}
