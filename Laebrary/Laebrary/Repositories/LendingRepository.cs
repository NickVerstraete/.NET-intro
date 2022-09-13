using Laebrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Laebrary.Repositories
{
    public class LendingRepository : ILendingRepository
    {
        private readonly LaebraryContext _dbContext;

        public LendingRepository(LaebraryContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public Task AddLending(Lending lending)
        {
            
            _dbContext.Lendings.AddAsync(lending);
            return _dbContext.SaveChangesAsync();    
        }

        public Task<List<Book>> GetLendedBooks(string nationalNumber)
        {
            return _dbContext.Lendings.Where(l => l.Member.NationalNumber == nationalNumber).Select(l => l.Book).ToListAsync(); ;
        }

    }
}
