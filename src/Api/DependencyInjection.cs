using Api.Common.Mapping;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings()
            .AddCors()
            .AddSignalR();

        services.AddControllers();
        
        return services;
    }
}