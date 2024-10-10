using Stio.Prefix.Id.Exceptions;

namespace Stio.Prefix.Id.Models;

public abstract record PrefixId : IParsable<PrefixId>
{
    private const string Separator = "_";
    private readonly string value;

    protected PrefixId()
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        this.value = this.Prefix + Separator + Ulid.NewUlid().ToString().ToLower();
    }

    public string Value
    {
        get => this.value;
        init
        {
            if (value.Split(Separator).FirstOrDefault() != this.Prefix)
            {
                throw new InvalidPrefixIdException(value, this.Prefix);
            }

            this.value = value;
        }
    }

    protected abstract string Prefix { get; }

    /// <summary>
    /// Need for correct binding.
    /// </summary>
    /// <returns>throw NotImplementedException.</returns>
    public static PrefixId Parse(string s, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Need for correct binding.
    /// </summary>
    /// <returns>throw NotImplementedException.</returns>
    public static bool TryParse(string? s, IFormatProvider? provider, out PrefixId result)
    {
        throw new NotImplementedException();
    }
}