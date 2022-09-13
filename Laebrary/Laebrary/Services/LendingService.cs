using Laebrary.Models;
using Laebrary.Repositories;

namespace Laebrary.Services
{
    public class LendingService: ILendingService
    {
        private readonly ILendingRepository _lendingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public LendingService(ILendingRepository lendingRepository, IBookRepository bookRepository, IMemberRepository memberRepository)
        {
            _lendingRepository = lendingRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        public async Task LendBook(int bookId, string memberNationalNumber) 
        {
            //create new Lending
            var book = await _bookRepository.GetBookById(bookId);
            var member = await _memberRepository.GetMemberByNationalNumber(memberNationalNumber);
            var lending = new Lending(book, member, DateTime.Now.AddDays(30));
            await _lendingRepository.AddLending(lending);
        }        
    }
}
