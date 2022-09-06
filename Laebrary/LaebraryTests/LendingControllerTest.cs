using Laebrary.Models;
using System.Net;
using System.Net.Http.Json;

namespace LaebraryTests
{
    [TestClass]
    public class LendingControllerTest
    {
        [TestMethod]
        public async Task ShouldCreateLending()
        {
            var appFactory = new TestLaebraryApplication().GetWebApplicationFactory(AddBooksAndMembersToDb);
            var httpClient = appFactory.CreateClient();
            var value = new Dictionary<string, string>
             {
                { "bookId", "1" },
                { "nationalNumberMember", "13545526514" }
             };
            var content = new FormUrlEncodedContent(value);
            await httpClient.PostAsync("/api/Lending/LendBook?bookId=1&nationalNumberMember=13545526514", content);
            var books = await httpClient.GetFromJsonAsync<List<Book>>("/api/Lending/GetLendings?nationalNumber=13545526514");
            Assert.AreEqual(1, books.Count());
            Assert.AreEqual("C# 8.0 and .NET Core 3.0", books.First().Title);
        }

        [TestMethod]
        public async Task ShouldNotAllowLending5Items()
        {
            var appFactory = new TestLaebraryApplication().GetWebApplicationFactory(AddBooksAndMembersToDb);
            var httpClient = appFactory.CreateClient();
            var value = new Dictionary<string, string>
             {
                { "bookId", "1" },
                { "nationalNumberMember", "13545526514" }
             };
            var content = new FormUrlEncodedContent(value);
            await httpClient.PostAsync("/api/Lending/LendBook?bookId=1&nationalNumberMember=13545526514", content);
            value = new Dictionary<string, string>
             {
                { "bookId", "2" },
                { "nationalNumberMember", "13545526514" }
             };
            content = new FormUrlEncodedContent(value);
            await httpClient.PostAsync("/api/Lending/LendBook?bookId=2&nationalNumberMember=13545526514", content);
            value = new Dictionary<string, string>
             {
                { "bookId", "3" },
                { "nationalNumberMember", "13545526514" }
             };
            content = new FormUrlEncodedContent(value);
            await httpClient.PostAsync("/api/Lending/LendBook?bookId=3&nationalNumberMember=13545526514", content);
            value = new Dictionary<string, string>
             {
                { "bookId", "4" },
                { "nationalNumberMember", "13545526514" }
             };
            content = new FormUrlEncodedContent(value);
            await httpClient.PostAsync("/api/Lending/LendBook?bookId=4&nationalNumberMember=13545526514", content);

             value = new Dictionary<string, string>
                 {
                    { "bookId", "5" },
                    { "nationalNumberMember", "13545526514" }
                 };
            content = new FormUrlEncodedContent(value);
            var result = await httpClient.PostAsync("/api/Lending/LendBook?bookId=1&nationalNumberMember=13545526514", content);
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            var books = await httpClient.GetFromJsonAsync<List<Book>>("/api/Lending/GetLendings?nationalNumber=13545526514");
            Assert.AreEqual(4, books.Count());
        }

        [TestMethod]
        public async Task ShouldNotLendOutBookTwice()
        {
            var appFactory = new TestLaebraryApplication().GetWebApplicationFactory(AddBooksAndMembersToDb);
            var httpClient = appFactory.CreateClient();
            var value = new Dictionary<string, string>
             {
                { "bookId", "1" },
                { "nationalNumberMember", "13545526514" }
             };
            var content = new FormUrlEncodedContent(value);
            await httpClient.PostAsync("/api/Lending/LendBook?bookId=1&nationalNumberMember=13545526514", content);
            value = new Dictionary<string, string>
             {
                { "bookId", "1" },
                { "nationalNumberMember", "13545526514" }
             };
            content = new FormUrlEncodedContent(value);
            var result = await httpClient.PostAsync("/api/Lending/LendBook?bookId=1&nationalNumberMember=44956526514", content);           
            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            var books1 = await httpClient.GetFromJsonAsync<List<Book>>("/api/Lending/GetLendings?nationalNumber=13545526514");
            Assert.AreEqual(1, books1.Count());
            var books2 = await httpClient.GetFromJsonAsync<List<Book>>("/api/Lending/GetLendings?nationalNumber=44956526514");
            Assert.AreEqual(0, books2.Count());
        }

        [TestMethod]
        public async Task ShouldRemoveLending()
        {
            var appFactory = new TestLaebraryApplication().GetWebApplicationFactory(AddBooksAndMembersToDb);
            var httpClient = appFactory.CreateClient();
            var value = new Dictionary<string, string>
             {
                { "bookId", "1" },
                { "nationalNumberMember", "13545526514" }
             };
            var content = new FormUrlEncodedContent(value);
            await httpClient.PostAsync("/api/Lending/LendBook?bookId=1&nationalNumberMember=13545526514", content);
            var booksBeforeReturning = await httpClient.GetFromJsonAsync<List<Book>>("/api/Lending/GetLendings?nationalNumber=13545526514");
            Assert.AreEqual(1, booksBeforeReturning.Count());
            Assert.AreEqual("C# 8.0 and .NET Core 3.0", booksBeforeReturning.First().Title);
            value = new Dictionary<string, string>
             {
                { "bookId", "1" },
             };
            content = new FormUrlEncodedContent(value);
            await httpClient.PostAsync("/api/Lending/ReturnBook?bookId=1", content);
            var booksAfterReturning = await httpClient.GetFromJsonAsync<List<Book>>("/api/Lending/GetLendings?nationalNumber=13545526514");
            Assert.AreEqual(0, booksAfterReturning.Count());

        }

        private static async Task AddBooksAndMembersToDb(LaebraryContext labraeryDbContext)
        {
            await labraeryDbContext.Books.AddAsync(new Book { Title = "C# 8.0 and .NET Core 3.0", Author = "Mark J. Price", Isbn = "9781788478120", Description = "Modern Cross-Platform Development: Build applications with C#, .NET Core, Entity Framework Core, ASP.NET Core, and ML.NET using ... Studio Code, 4th Edition (English Edition) " });
            await labraeryDbContext.Books.AddAsync(new Book { Title = "Head First C#", Author = "Andrew Stellman", Isbn = "1491976705", Description = "A Learner's Guide to Real-World Programming with C# and .NET Core" });
            await labraeryDbContext.Books.AddAsync(new Book { Title = "C# For Dummies", Author = "Stephen Randy Davis", Isbn = "9780764508141", Description = "This book covers everything you need to begin programming in C# as painlessly as possible. C# For Dummies introduces the new language, covers the syntax of the language, explains expert programming techniques and jumps right into writing applications." });
            await labraeryDbContext.Books.AddAsync(new Book { Title = "The C# Programming Language", Author = "Anders Hejlsberg", Isbn = "9780321334435", Description = "C# is a simple, modern, object-oriented, and type-safe programming language that combines the high productivity of rapid application development languages with the raw power of C and C++." });
            await labraeryDbContext.Books.AddAsync(new Book { Title = "Object Oriented Programming using C#", Author = "Simon Kendal", Isbn = "9788740321401", Description = "This book will explain the Object Oriented approach to programming and through the use of small exercises in C#, for which feedback is provided, develop some practical skills as well." });

            await labraeryDbContext.Members.AddAsync(new Member { FirstName = "Bruce", LastName = "Simpson", NationalNumber = "13545526514" });
            await labraeryDbContext.Members.AddAsync(new Member { FirstName = "John", LastName = "Lee", NationalNumber = "44956526514" });
        }       
    }
    
}