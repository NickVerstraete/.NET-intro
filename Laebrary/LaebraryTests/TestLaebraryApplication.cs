using Laebrary.Models;
using Laebrary.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LaebraryTests
{
    internal class TestLaebraryApplication
    {
        public WebApplicationFactory<Program> GetWebApplicationFactory(Func<LaebraryContext, Task> AddDataToDb)
        {
            var appFactory = new WebApplicationFactory<Program>()
                            .WithWebHostBuilder(host =>
                            {
                                host.ConfigureServices(async services =>
                                {
                                    var descriptor = services.SingleOrDefault(
                                        d => d.ServiceType ==
                                        typeof(DbContextOptions<LaebraryContext>));

                                    services.Remove(descriptor);

                                    services.AddDbContext<LaebraryContext>(options =>
                                    {
                                        options.UseInMemoryDatabase("InMemoryDB");
                                    });

                                    var sp = services.BuildServiceProvider();

                                    using (var scope = sp.CreateScope())
                                    {
                                        var provider = scope.ServiceProvider;
                                        using (var labraeryDbContext = provider.GetRequiredService<LaebraryContext>())
                                        {
                                            await labraeryDbContext.Database.EnsureDeletedAsync();
                                            await labraeryDbContext.Database.EnsureCreatedAsync();

                                            await AddDataToDb(labraeryDbContext);
                                            await labraeryDbContext.SaveChangesAsync();
                                        }
                                    }
                                    services.AddControllers();
                                });

                            });
            return appFactory;
        }

    }
}
