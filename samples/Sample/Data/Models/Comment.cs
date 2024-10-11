using Stio.Sample.Data.ValueTypes;

namespace Stio.Sample.Data.Models;

public class Comment
{
    public CommentId Id { get; set; } = new();

    public BookId? BookId { get; set; }

    public Book? Book { get; set; }

    public string? Text { get; set; }
}
