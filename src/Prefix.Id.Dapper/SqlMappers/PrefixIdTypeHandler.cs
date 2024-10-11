using System.Data;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using Dapper;
using Stio.Prefix.Id.Models;

namespace Stio.Prefix.Id.Dapper.SqlMappers;

internal class PrefixIdTypeHandler : SqlMapper.ITypeHandler
{
    public void SetValue(IDbDataParameter parameter, object? value)
    {
        if (value is PrefixId prefixId)
        {
            parameter.Value = prefixId.Value;
        }
        else
        {
            parameter.Value = DBNull.Value;
        }
    }

    public object? Parse(Type destinationType, object value)
    {
        return this.BuildLambda(destinationType).Invoke((string)value);
    }

    private Func<string, object?> BuildLambda(Type destinationType)
    {
        var valueParam = Expression.Parameter(typeof(string), "value");
        var ctor = Expression.New(destinationType);
        var valueProperty = destinationType.GetProperty("Value");
        var valueAssignment = Expression.Bind(valueProperty!, valueParam);
        var memberInit = Expression.MemberInit(ctor, valueAssignment);

        var lambda = Expression.Lambda<Func<string, object?>>(memberInit, valueParam);

        return lambda.Compile();
    }
}