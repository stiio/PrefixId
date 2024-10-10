using System.Text.Json;
using Stio.Prefix.Id.JsonConverters;
using Stio.Tests.Prefix.Id.Models;

namespace Stio.Tests.Prefix.Id.Tests;

public class JsonTest
{
    [Fact]
    public void SerializeToJsonTest()
    {
        var jsonOptions = new JsonSerializerOptions()
        {
            Converters = { new PrefixIdJsonConverterFactory() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
        };

        var book = new BookDto()
        {
            Id = new BookId()
            {
                Value = "book_123",
            },
        };

        var json = JsonSerializer.Serialize(book, jsonOptions);

        var expected = """
            {"id":"book_123"}
            """;

        Assert.Equal(expected, json);
    }

    [Fact]
    public void DeserializeFromJsonTest()
    {
        var jsonOptions = new JsonSerializerOptions()
        {
            Converters = { new PrefixIdJsonConverterFactory() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
        };

        var json = """
            {"id":"book_123"}
            """;

        var book = JsonSerializer.Deserialize<BookDto>(json, jsonOptions);

        var expected = new BookDto()
        {
            Id = new BookId()
            {
                Value = "book_123",
            },
        };

        Assert.Equal(expected.Id, book?.Id);
    }
}