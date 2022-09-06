using Laebrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laebrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    { 
        public MemberController()
        {
        }

        [HttpGet]
        public async Task<IEnumerable<Member>> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task AddMember(Member member)
        {
            throw new NotImplementedException();
        }
    }
}