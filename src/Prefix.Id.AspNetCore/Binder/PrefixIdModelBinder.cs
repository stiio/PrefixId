using Microsoft.AspNetCore.Mvc.ModelBinding;
using Stio.Prefix.Id.Exceptions;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.AspNetCore.Binder;

internal class PrefixIdModelBinder<TId> : IModelBinder
    where TId : PrefixId, new()
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext is null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;
        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        try
        {
            var result = new TId()
            {
                Value = value,
            };

            bindingContext.Result = ModelBindingResult.Success(result);
        }
        catch (InvalidPrefixIdException e)
        {
            bindingContext.ModelState.TryAddModelError(modelName, e.Message);
        }

        return Task.CompletedTask;
    }
}