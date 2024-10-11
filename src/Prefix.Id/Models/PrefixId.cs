using System.ComponentModel;
using Stio.Prefix.Id.Exceptions;
using Stio.Prefix.Id.TypeConverters;

namespace Stio.Prefix.Id.Models;

[TypeConverter(typeof(PrefixIdTypeConverter))]
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
}