using Stio.Prefix.Id.Exceptions;

namespace Stio.Prefix.Id.Models;

public abstract record PrefixId
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
    /// Need for correct binding. Throw <c cref="NotImplementedException">NotImplementedException</c>.
    /// </summary>
    /// <returns>throw <c cref="NotImplementedException">NotImplementedException</c></returns>
    public static bool TryParse(string? s, IFormatProvider? provider, out PrefixId result)
    {
        // TODO: по хорошему убрать бы этот метод и заменить на какие-нибудь конвенции mvc, если это возможно
        throw new NotImplementedException();
    }
}