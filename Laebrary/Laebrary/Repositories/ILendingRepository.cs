using Laebrary.Models;

namespace Laebrary.Repositories
{
    public interface ILendingRepository
    {
        Task AddLending(Lending lending);
        Task<List<Book>> GetLendedBooks(string nationalNumber);

    }
}
