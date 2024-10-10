using System.Net;
using System.Text.Json;
using Stio.DebugConsole.Models;
using Stio.Prefix.Id.JsonConverters;

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
    Name = "Book",
};

var json = JsonSerializer.Serialize(book, jsonOptions);

Console.WriteLine(json);
