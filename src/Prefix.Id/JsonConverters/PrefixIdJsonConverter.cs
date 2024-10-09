using System.Text.Json;
using System.Text.Json.Serialization;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.JsonConverters;

internal class PrefixIdJsonConverter<TId> : JsonConverter<TId> where TId : PrefixId, new()
{
    public override TId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null)
        {
            return null;
        }

        var value = JsonSerializer.Deserialize<string>(ref reader, options);
        var id = new TId { Value = value! };
        return id;
    }

    public override void Write(Utf8JsonWriter writer, TId? value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            writer.WriteNullValue();
        }
        else
        {
            JsonSerializer.Serialize(writer, value.Value, options);
        }
    }
}