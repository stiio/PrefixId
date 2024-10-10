using Microsoft.OpenApi.Models;
using Stio.Prefix.Id.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Stio.Prefix.Id.Swashbuckle.Filters;

internal class PrefixIdSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (IsPrefixId(context))
        {
            if (schema.Type == "array" && schema.Items != null)
            {
                schema.Items.Type = "string";
                schema.Items.Format = null;
                return;
            }

            schema.Type = "string";
            schema.Format = null;
        }
    }

    private static bool IsPrefixId(SchemaFilterContext context)
    {
        return context.Type.IsArray
            ? context.Type.MakeArrayType().IsAssignableTo(typeof(PrefixId))
            : context.Type.IsAssignableTo(typeof(PrefixId));
    }
}