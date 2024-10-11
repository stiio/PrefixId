using System.ComponentModel;
using System.Globalization;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.TypeConverters;

internal class PrefixIdTypeConverter : TypeConverter
{
    private readonly Type type;

    public PrefixIdTypeConverter(Type type)
    {
        this.type = type;
    }

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string s)
        {
            var factory = PrefixIdHelper.GetFactory(this.type);
            return factory(s);
        }

        return base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is PrefixId prefixId)
        {
            return prefixId.Value;
        }

        return base.ConvertTo(context, culture, value, destinationType);
    }
}