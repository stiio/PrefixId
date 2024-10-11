using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.EntityFramework.ValueConverters;

internal class PrefixIdValueConverter<T> : ValueConverter<T, string>
    where T : PrefixId, new()
{
    public PrefixIdValueConverter()
        : base(
            v => v.Value,
            v => new T { Value = v })
    {
    }
}