using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.TypeConverters;

internal class PrefixIdHelper
{
    private static readonly ConcurrentDictionary<Type, Delegate> PrefixIdFactories = new();

    public static Func<string, object> GetFactory(Type prefixIdType)
    {
        return (Func<string, object>)PrefixIdFactories.GetOrAdd(
            prefixIdType,
            CreateFactory);
    }

    private static Func<string, object> CreateFactory(Type prefixIdType)
    {
        if (!IsPrefixId(prefixIdType))
        {
            throw new ArgumentException($"Type '{prefixIdType}' is not a strongly-typed id type", nameof(prefixIdType));
        }

        var valueParam = Expression.Parameter(typeof(string), "value");
        var ctor = Expression.New(prefixIdType);
        var valueProperty = prefixIdType.GetProperty("Value");
        var valueAssignment = Expression.Bind(valueProperty!, valueParam);
        var memberInit = Expression.MemberInit(ctor, valueAssignment);

        var lambda = Expression.Lambda<Func<string, object>>(memberInit, valueParam);

        return lambda.Compile();
    }

    private static bool IsPrefixId(Type prefixIdType)
    {
        return prefixIdType.IsAssignableTo(typeof(PrefixId));
    }
}