using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Stio.Prefix.Id.EntityFramework.ValueConverters;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.EntityFramework;
public static class ConfigureServices
{
    public static void ApplyTypedIdConversions(
        this ModelConfigurationBuilder configurationBuilder,
        params Assembly[] assemblies)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var typedIdTypes = assembly.GetTypes().Where(t =>
            t is { IsClass: true, IsAbstract: false }
            && t.IsSubclassOf(typeof(PrefixId)));

        foreach (var type in typedIdTypes)
        {
            var converterType = typeof(PrefixIdValueConverter<>).MakeGenericType(type);
            configurationBuilder
                .Properties(type)
                .HaveConversion(converterType);
        }
    }

    public static void ApplyTypedIdConverters(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetDeclaredProperties()
                         .Where(x => x.ClrType.IsAssignableTo(typeof(PrefixId))))
            {
                var converterType = typeof(PrefixIdValueConverter<>).MakeGenericType(property.ClrType);
                modelBuilder.Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(converterType);
            }
        }
    }
}
