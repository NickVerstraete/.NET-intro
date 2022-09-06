using Microsoft.EntityFrameworkCore;

namespace Laebrary.Models
{
    public class LaebraryContext: DbContext
    {
        public LaebraryContext(DbContextOptions<LaebraryContext> options): base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = null;
        public DbSet<Member> Members { get; set; } = null;
        public DbSet<Lending> Lendings { get; set; } = null;
    }
}
