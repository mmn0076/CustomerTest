using System.Web;
using CustomerTest.Infrastructure.Persistence.DbContext;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerTest.IntegrationTests.Abstraction;

public class BaseIntegrationTest : IClassFixture<WebAppFactory>
{
    protected readonly CustomerTestDbContext DbContext;
    protected HttpClient Client { get; set; }

    protected BaseIntegrationTest(WebAppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<CustomerTestDbContext>();
        DbContext.Database.EnsureCreated();

        Client = factory.CreateClient();
    }

    protected static string ToQueryString(object obj)
    {
        var properties = from p in obj.GetType().GetProperties()
            let value = p.GetValue(obj, null)
            where value != null
            select p.Name + "=" + HttpUtility.UrlEncode(value.ToString());

        return string.Join("&", properties.ToArray());
    }

    protected async Task AddEntityAsync<TEntity>(TEntity entity) where TEntity : class
    {
        DbContext.Set<TEntity>().Add(entity);
        await DbContext.SaveChangesAsync();
    }
}