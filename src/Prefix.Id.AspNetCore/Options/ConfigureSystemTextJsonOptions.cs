using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stio.Prefix.Id.JsonConverters;

namespace Stio.Prefix.Id.AspNetCore.Options;

internal class ConfigureSystemTextJsonOptions : IConfigureOptions<JsonOptions>
{
    public void Configure(JsonOptions options)
    {
        options.JsonSerializerOptions.Converters.Add(new PrefixIdJsonConverterFactory());
    }
}