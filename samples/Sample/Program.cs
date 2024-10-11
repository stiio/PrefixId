using System.Reflection;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Stio.Prefix.Id.AspNetCore;
using Stio.Prefix.Id.Dapper;
using Stio.Sample.Data.Context;
using Stio.Sample.Data.ValueTypes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPrefixId();

DefaultTypeMap.MatchNamesWithUnderscores = true;
PrefixIdDapper.AddDapperTypeHandlers(Assembly.GetExecutingAssembly());

builder.Services.AddDbContextPool<ApplicationDbContext>(otps =>
{
    otps.UseNpgsql("Host=localhost;Port=5432;Database=prefix_id;Username=postgres;Password=postgres;", x =>
    {
        x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });

    otps.UseSnakeCaseNamingConvention();
});

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
