using CustomerTest.Application.Common.Interfaces.Persistence;
using CustomerTest.Infrastructure.Interceptors;
using CustomerTest.Infrastructure.Persistence.DbContext;
using CustomerTest.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerTest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var dbHost = Environment.GetEnvironmentVariable("CustomerTest_dbHost");
        var dbName = Environment.GetEnvironmentVariable("CustomerTest_dbName");
        var dbPassword = Environment.GetEnvironmentVariable("CustomerTest_dbPassword");
        services.AddDbContext<CustomerTestDbContext>(options =>
                options.UseSqlServer($"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};Encrypt=false"));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        services.AddScoped<PublishDomainEventInterceptor>();

        return services;
    }
}