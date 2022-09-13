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
            //check book is not lended by anyone else
            if (await _lendingRepository.IsBookLended(bookId)) throw new Exception("Book is not lendable");
            //check member has not reached max rentable items
            if ((await _lendingRepository.GetLendedBooks(memberNationalNumber)).Count() >= 4) throw new Exception("Max number of lended books reached");
            //create new Lending
            var book = await _bookRepository.GetBookById(bookId);
            var member = await _memberRepository.GetMemberByNationalNumber(memberNationalNumber);
            var lending = new Lending(book, member, DateTime.Now.AddDays(30));
            await _lendingRepository.AddLending(lending);
        }

        public async Task ReturnBook(int bookId)
        {
            //check book is lended
            if (!await _lendingRepository.IsBookLended(bookId)) throw new Exception("Book is not lend out, impossible to return");
            await _lendingRepository.RemoveLending(bookId);
        }
    }
}
