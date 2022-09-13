using Laebrary.Models;
using Laebrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Laebrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;

        public MemberController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Member>> Get()
        {
            return await _memberRepository.GetAllMembers();
        }

        [HttpPost]
        public async Task AddMember(Member member)
        {
            await _memberRepository.AddMember(member);
        }
    }
}