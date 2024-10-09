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
                throw new Exception($"Id {value} has the wrong prefix. Expected: {this.Prefix}");
            }

            this.value = value;
        }
    }

    protected abstract string Prefix { get; }
}