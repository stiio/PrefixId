using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stio.Prefix.Id.EntityFramework.ValueConverters;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.EntityFramework;

public static class ConfigureServices
{
    public static void ApplyTypedIdConversions(
        this ModelConfigurationBuilder configurationBuilder,
        params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var typedIdTypes = assembly.GetTypes().Where(t =>
                t is { IsClass: true, IsAbstract: false }
                && t.IsSubclassOf(typeof(PrefixId)));

            foreach (var type in typedIdTypes)
            {
                var converterType = typeof(PrefixIdValueConverter<>).MakeGenericType(type);
                configurationBuilder
                    .Properties(type)
                    .HaveConversion(converterType)
                    .HaveMaxLength(40);
            }
        }
    }

    public static void ApplyTypedIdConverters(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in
                     entityType.ClrType.GetProperties().Where(x => x.PropertyType.IsAssignableTo(typeof(PrefixId))))
            {
                var converterType = typeof(PrefixIdValueConverter<>).MakeGenericType(property.PropertyType);
                modelBuilder.Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(converterType)
                    .HasMaxLength(40);
            }
        }
    }
}
