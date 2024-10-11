using Stio.Prefix.Id.Models;

namespace Stio.Sample.Data.ValueTypes;

public record BookId : PrefixId
{
    protected override string Prefix => "book";
}
