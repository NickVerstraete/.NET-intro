namespace Laebrary.Services
{
    public interface ILendingService
    {
        Task LendBook(int bookId, string memberNationalNumber);
    }
}
