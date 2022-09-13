using Laebrary.Models;
using Laebrary.Repositories;
using Laebrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace Laebrary.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LendingController : ControllerBase
    {
        private readonly ILendingService _lendingService;
        private readonly ILendingRepository _lendingRepository;

        public LendingController(ILendingService lendingService, ILendingRepository lendingRepository)
        {
            _lendingService = lendingService;
            _lendingRepository = lendingRepository;
        }               

        [HttpPost]
        public async Task LendBook(int bookId, string nationalNumberMember)
        {
            await _lendingService.LendBook(bookId, nationalNumberMember);
        }

        [HttpPost]
        public async Task ReturnBook(int bookId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<List<Book>> GetLendings(string nationalNumber)
        {
            return await _lendingRepository.GetLendedBooks(nationalNumber);
        }
    }
}