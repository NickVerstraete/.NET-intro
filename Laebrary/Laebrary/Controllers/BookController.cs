using Laebrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Laebrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {     


        public BookController()
        {         
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task AddBook(Book book)
        {
            throw new NotImplementedException();
        }
    }
}