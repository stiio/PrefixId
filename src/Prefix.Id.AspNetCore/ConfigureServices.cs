using Microsoft.Extensions.DependencyInjection;
using Stio.Prefix.Id.AspNetCore.Options;
using Stio.Prefix.Id.Swashbuckle;

namespace Stio.Prefix.Id.AspNetCore;

public static class ConfigureServices
{
    public static IServiceCollection AddPrefixId(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureSystemTextJsonOptions>();
        services.ConfigureOptions<ConfigureMvcOptions>();

        services.AddPrefixIdSwashbuckle();

        return services;
    }
}