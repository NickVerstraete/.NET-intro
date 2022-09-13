using Laebrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Laebrary.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LaebraryContext _dbContext;

        public MemberRepository(LaebraryContext dbContext)
        {
            _dbContext = dbContext;
        }        

        public Task<Member> GetMemberByNationalNumber(string nationalNumber)
        {
            var member = _dbContext.Members.SingleOrDefaultAsync(m => m.NationalNumber == nationalNumber);
            if (member == null) throw new Exception("A member with national number: " + nationalNumber + " does not exist");
            return member;
        }

        public Task<Member> GetMemberById(int id)
        {
            return _dbContext.Members.SingleAsync(m => m.Id == id);
        }

        public Task<List<Member>> GetAllMembers()
        {
            return _dbContext.Members.ToListAsync();
        }

        public async Task AddMember(Member member)
        {
            _dbContext.Add(member);
            await _dbContext.SaveChangesAsync();
        }
    }
}
