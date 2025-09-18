using Microsoft.AspNetCore.Mvc;
using OMS.DA.Context;

namespace OMS.API.Controllers
{
    /// <summary>
    /// Controller responsible for health checks and monitoring the service status.
    /// </summary>
    [Route("api/healthcheck")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheckController"/>.
        /// </summary>
        /// <param name="serviceProvider">The service provider for accessing database context.</param>
        public HealthCheckController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Simple ping endpoint to check if the service is running.
        /// </summary>
        /// <returns>Returns an "OK" response with a message confirming service status.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public ActionResult<string> Ping()
        {
            return Ok("Service is running");
        }

        /// <summary>
        /// Checks the health status of the application by verifying the database connectivity.
        /// </summary>
        /// <returns>Returns health status information including timestamp.</returns>
        [HttpGet("status")]
        [TenantHeader]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<object>> GetHealthStatus()
        {
            bool isSuccess = await TryConnectToDbAsync();
            var healthStatus = new
            {
                Status = isSuccess ? "Healthy" : "Unhealthy",
                Timestamp = DateTime.UtcNow
            };

            return isSuccess ? Ok(healthStatus) : StatusCode(StatusCodes.Status500InternalServerError, healthStatus);
        }

        private async Task<bool> TryConnectToDbAsync()
        {
            try
            {
                var dbContext = _serviceProvider.GetRequiredService<AppDbContext>();
                return await dbContext.Database.CanConnectAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
