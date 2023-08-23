using Mapster;
using MapsterMapper;
using CustomerTest.Presentation.Api.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Reflection;

namespace CustomerTest.Presentation.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        //Add Mappings
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton<IMapper>(new Mapper(config));

        services.AddSingleton<ProblemDetailsFactory, DefaultProblemDetailsFactory>();

        return services;
    }
}