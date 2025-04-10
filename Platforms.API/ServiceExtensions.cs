using Platforms.API.Services;
using Platforms.API.Services.Interfaces;

namespace Platforms.API;

public static class ServiceExtensions
{
    public static IServiceCollection AddServiceLogic(this IServiceCollection services)
    {
        services.AddSingleton<IPlatformService, PlatformService>();
        return services;
    }
}