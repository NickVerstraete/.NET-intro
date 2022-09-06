using Laebrary.Models;
using System.Net.Http.Json;

namespace LaebraryTests
{
    [TestClass]
    public class BookControllerTests
    {
        [TestMethod]
        public async Task ShouldReturnBookInDb()
        {
            var appFactory = new TestLaebraryApplication().GetWebApplicationFactory(AddBookToDb);
            var httpClient = appFactory.CreateClient();

            var books = await httpClient.GetFromJsonAsync<List<Book>>("/api/Book");
            Assert.AreEqual(1, books.Count());
            Assert.AreEqual("C# 8.0 and .NET Core 3.0", books.First().Title);
        }

        private static async Task AddBookToDb(LaebraryContext labraeryDbContext)
        {
            await labraeryDbContext.Books.AddAsync(new Book { Title = "C# 8.0 and .NET Core 3.0", Author = "Mark J. Price", Isbn = "978-1788478120", Description = "Modern Cross-Platform Development: Build applications with C#, .NET Core, Entity Framework Core, ASP.NET Core, and ML.NET using ... Studio Code, 4th Edition (English Edition) " });
        }

        [TestMethod]
        public async Task ShouldAddAndReturnBook()
        {
            var appFactory = new TestLaebraryApplication().GetWebApplicationFactory(context => Task.CompletedTask);
            var httpClient = appFactory.CreateClient();
            var book1 = new Book { Title = "C# 8.0 and .NET Core 3.0", Author = "Mark J. Price", Isbn = "978-1788478120", Description = "Modern Cross-Platform Development: Build applications with C#, .NET Core, Entity Framework Core, ASP.NET Core, and ML.NET using ... Studio Code, 4th Edition (English Edition) " };
            var book2 = new Book { Title = "Head First C#", Author = "Andrew Stellman", Isbn = "1491976705", Description = "A Learner's Guide to Real-World Programming with C# and .NET Core" };
            await httpClient.PostAsJsonAsync("/api/Book", book1);
            await httpClient.PostAsJsonAsync("/api/Book", book2);
            var books = await httpClient.GetFromJsonAsync<List<Book>>("/api/Book");
            Assert.AreEqual(2, books.Count());
            Assert.AreEqual("C# 8.0 and .NET Core 3.0", books.OrderBy(x => x.Title).First().Title);
            Assert.AreEqual("Head First C#", books.OrderBy(x => x.Title).Last().Title);
        }
    }
    
}