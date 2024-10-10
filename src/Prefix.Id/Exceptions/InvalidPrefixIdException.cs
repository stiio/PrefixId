namespace Stio.Prefix.Id.Exceptions;

public class InvalidPrefixIdException : Exception
{
    public InvalidPrefixIdException(string? value, string expectedPrefix)
        : base($"Id {value} has the wrong prefix. Expected: {expectedPrefix}")
    {
    }
}