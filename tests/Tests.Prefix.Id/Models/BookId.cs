using Stio.Prefix.Id.Models;

namespace Stio.Tests.Prefix.Id.Models;

public record BookId : PrefixId
{
    protected override string Prefix => "book";
}