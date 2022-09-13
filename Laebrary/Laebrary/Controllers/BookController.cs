using Laebrary.Models;
using Laebrary.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Laebrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task AddBook(Book book)
        {
            await _bookRepository.AddBook(book);
        }
    }
}