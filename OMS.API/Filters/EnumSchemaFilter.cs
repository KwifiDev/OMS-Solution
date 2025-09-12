using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();
            foreach (var enumValue in Enum.GetValues(context.Type))
            {
                var memberInfo = context.Type.GetMember(enumValue.ToString()!)[0];
                var description = memberInfo.GetCustomAttribute<DescriptionAttribute>()?.Description;

                schema.Enum.Add(new OpenApiString($"{enumValue} - {description ?? enumValue.ToString()}"));
            }
        }
    }
}