using CustomerTest.Infrastructure.Persistence.DbContext;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerTest.EndToEndTests;

public class BaseEndToEndTest : IClassFixture<WebAppFactory>
{
    protected readonly CustomerTestDbContext DbContext;
    protected HttpClient Client { get;  set; }

    protected BaseEndToEndTest(WebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<CustomerTestDbContext>();
        DbContext.Database.EnsureCreated();
        
        Client = factory.CreateClient();
    }
}