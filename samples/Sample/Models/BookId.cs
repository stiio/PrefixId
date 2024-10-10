using Stio.Prefix.Id.Models;

namespace Stio.Sample.Models;

public record BookId : PrefixId
{
    protected override string Prefix => "book";
}
