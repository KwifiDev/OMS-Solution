namespace OMS.API.Extensions
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        private const string ErrorResponse = "{\"error\": \"Tenant information is missing or invalid.\"}";

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantProvider tenantProvider)
        {
            var tenant = tenantProvider.GetFromJwtClaim() ?? tenantProvider.GetFromHeader() ?? tenantProvider.GetLocal();

            if (tenant is null)
            {
                await WriteErrorResponseAsync(context, StatusCodes.Status400BadRequest, ErrorResponse);
                return;
            }

            // Store the tenant information for later use in the request pipeline
            tenantProvider.SetTenant(tenant);

            // Call the next middleware in the pipeline
            await _next(context);
        }

        private static async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(message);
        }
    }
}