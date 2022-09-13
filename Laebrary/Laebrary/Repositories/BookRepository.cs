using Laebrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Laebrary.Repositories
{
    public class BookRepository: IBookRepository
    {
        private readonly LaebraryContext _dbContext;

        public BookRepository(LaebraryContext dbContext)
        {
            _dbContext = dbContext;
        }        

        public async Task AddBook(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
        }        
    }
}
