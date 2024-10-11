using Stio.Prefix.Id.Models;

namespace Stio.Sample.Data.ValueTypes;

public record CommentId : PrefixId
{
    protected override string Prefix => "comm";
}