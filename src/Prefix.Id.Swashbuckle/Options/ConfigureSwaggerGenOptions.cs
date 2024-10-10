using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Stio.Prefix.Id.Swashbuckle.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Stio.Prefix.Id.Swashbuckle.Options;

internal class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.SchemaFilter<PrefixIdSchemaFilter>();
    }
}