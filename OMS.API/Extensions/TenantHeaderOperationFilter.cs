using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class TenantHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var tenantHeaderAttribute = context.MethodInfo
            .GetCustomAttributes(typeof(TenantHeaderAttribute), false)
            .FirstOrDefault() as TenantHeaderAttribute;

        if (tenantHeaderAttribute != null)
        {
            operation.Parameters ??= [];
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Tenant-ID",
                In = ParameterLocation.Header,
                Required = tenantHeaderAttribute.Required,
                Schema = new OpenApiSchema { Type = "string" },
                Description = tenantHeaderAttribute.Description
            });
        }
    }
}