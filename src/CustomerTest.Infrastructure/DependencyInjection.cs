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
        //TODO:Move Connection String To ENV or Secret
        services.AddDbContext<CustomerTestDbContext>(options =>
            options.UseSqlServer("Data Source=navid;Initial Catalog=CustomerTest;Integrated Security=True;Encrypt=false"));

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        services.AddScoped<PublishDomainEventInterceptor>();

        return services;
    }
}