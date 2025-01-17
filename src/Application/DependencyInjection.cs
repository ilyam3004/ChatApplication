using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddMediatR(config 
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;    
    }
}