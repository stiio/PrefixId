using System.Text.Json;
using System.Text.Json.Serialization;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.JsonConverters;

public class PrefixIdJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableTo(typeof(PrefixId));
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converterType = typeof(PrefixIdJsonConverter<>).MakeGenericType(typeToConvert);

        return (JsonConverter?)Activator.CreateInstance(converterType);
    }
}