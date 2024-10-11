using System.Reflection;
using Dapper;
using Stio.Prefix.Id.Dapper.SqlMappers;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.Dapper;

public static class PrefixIdDapper
{
    public static void AddDapperTypeHandlers(params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(PrefixId)) && !x.IsAbstract))
            {
                SqlMapper.AddTypeHandler(type, new PrefixIdTypeHandler());
            }
        }
    }
}