namespace OMS.API.Extensions
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITenantProvider tenantProvider)
        {
            var tenant = tenantProvider.GetFromJwtClaim() ?? tenantProvider.GetFromHeader();

            if (tenant is null)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"error\": \"Tenant information is missing or invalid.\"}");
                return;
            }


            // You can store the tenant information in the TenantProvider (Service LifeTime is scoped) for later use
            tenantProvider.SetTenant(tenant);

            // Call the next middleware in the pipeline
            await _next(context);

        }
    }
}