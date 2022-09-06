using Laebrary.Models;
using System.Net.Http.Json;

namespace LaebraryTests
{
    [TestClass]
    public class MemberControllerTests
    {
        [TestMethod]
        public async Task ShouldReturnMembersInDb()
        {
            var appFactory = new TestLaebraryApplication().GetWebApplicationFactory(AddMemberToDb);
            var httpClient = appFactory.CreateClient();

            var members = await httpClient.GetFromJsonAsync<List<Member>>("/api/Member");
            Assert.AreEqual(1, members.Count());
            Assert.AreEqual("Bruce", members.First().FirstName);
        }

        private static async Task AddMemberToDb(LaebraryContext labraeryDbContext)
        {
            await labraeryDbContext.Members.AddAsync(new Member { FirstName = "Bruce", LastName = "Simpson", NationalNumber = "135455-26514" });
        }

        [TestMethod]
        public async Task ShouldAddAndReturnMember()
        {
            var appFactory = new TestLaebraryApplication().GetWebApplicationFactory(context => Task.CompletedTask);
            var httpClient = appFactory.CreateClient();
            var member1 = new Member { FirstName = "Bruce", LastName = "Simpson", NationalNumber = "135455-26514" };
            var member2 = new Member { FirstName = "John", LastName = "Lee", NationalNumber = "449565-26514" };
            await httpClient.PostAsJsonAsync("/api/Member", member1);
            await httpClient.PostAsJsonAsync("/api/Member", member2);
            var members = await httpClient.GetFromJsonAsync<List<Member>>("/api/Member");
            Assert.AreEqual(2, members.Count());
            Assert.AreEqual("Bruce", members.OrderBy(x => x.FirstName).First().FirstName);
            Assert.AreEqual("John", members.OrderBy(x => x.FirstName).Last().FirstName);
        }
    }
    
}