using Stio.Prefix.Id.Models;

namespace Stio.DebugConsole.Models;

public record BookId : PrefixId
{
    protected override string Prefix => "book";
}