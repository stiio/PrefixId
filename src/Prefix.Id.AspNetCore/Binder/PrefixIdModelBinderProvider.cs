using Microsoft.AspNetCore.Mvc.ModelBinding;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.AspNetCore.Binder;

internal class PrefixIdModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType.IsAssignableTo(typeof(PrefixId)))
        {
            var binder = (IModelBinder?)Activator.CreateInstance(typeof(PrefixIdModelBinder<>).MakeGenericType(context.Metadata.ModelType));
            return binder;
        }

        return null;
    }
}