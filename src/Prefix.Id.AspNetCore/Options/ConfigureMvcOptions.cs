using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stio.Prefix.Id.AspNetCore.Binder;

namespace Stio.Prefix.Id.AspNetCore.Options;

internal class ConfigureMvcOptions : IConfigureOptions<MvcOptions>
{
    private readonly IOptions<JsonOptions> jsonOptions;

    public ConfigureMvcOptions(IOptions<JsonOptions> jsonOptions)
    {
        this.jsonOptions = jsonOptions;
    }

    public void Configure(MvcOptions options)
    {
        options.ModelBinderProviders.Insert(0, new PrefixIdModelBinderProvider());
    }
}