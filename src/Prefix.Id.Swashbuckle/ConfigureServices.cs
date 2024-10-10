using Microsoft.Extensions.DependencyInjection;
using Stio.Prefix.Id.Swashbuckle.Options;

namespace Stio.Prefix.Id.Swashbuckle;

public static class ConfigureServices
{
    public static IServiceCollection AddPrefixIdSwashbuckle(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureSwaggerGenOptions>();

        return services;
    }
}