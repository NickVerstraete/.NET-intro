using Laebrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laebrary.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LendingController : ControllerBase
    {     
        public LendingController()
        {
        }               

        [HttpPost]
        public async Task LendBook(int bookId, string nationalNumberMember)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task ReturnBook(int bookId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<List<Book>> GetLendings(string nationalNumber)
        {
            throw new NotImplementedException();
        }
    }
}