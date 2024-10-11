using Stio.Sample.Data.ValueTypes;

namespace Stio.Sample.Data.Models;

public class Book
{
    public BookId Id { get; set; } = new();

    public string? Name { get; set; }
}