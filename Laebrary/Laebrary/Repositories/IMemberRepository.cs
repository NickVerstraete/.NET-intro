using Laebrary.Models;

namespace Laebrary.Repositories
{
    public interface IMemberRepository
    {        
        Task<Member> GetMemberByNationalNumber(string nationalNumber);
        Task<Member> GetMemberById(int id);
        Task<List<Member>> GetAllMembers();
        Task AddMember(Member member);
    }
}
