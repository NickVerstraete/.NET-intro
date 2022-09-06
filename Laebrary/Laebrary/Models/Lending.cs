namespace Laebrary.Models
{
    public class Lending
    {
        public Lending(Book book, Member member, DateTime lendUntil)
        {
            Book = book;
            Member = member;
            LendUntil = lendUntil;
        }

        public Lending() { }

        public int Id { get; set; } 
        public Book Book { get; set; }
        public Member Member { get; set; }
        public DateTime LendUntil { get; set; }   
    }
}
