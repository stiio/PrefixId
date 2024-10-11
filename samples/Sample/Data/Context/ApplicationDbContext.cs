using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Stio.Prefix.Id.EntityFramework;
using Stio.Sample.Data.Models;

namespace Stio.Sample.Data.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyTypedIdConverters();
        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // configurationBuilder.ApplyTypedIdConversions(Assembly.GetExecutingAssembly());
        configurationBuilder.Properties<string>()
            .HaveMaxLength(256);

        base.ConfigureConventions(configurationBuilder);
    }
}