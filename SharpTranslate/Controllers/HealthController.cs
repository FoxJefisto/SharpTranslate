using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SharpTranslate.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealthAsync()
        {
            var result = await _healthCheckService.CheckHealthAsync();
            return result.Status == HealthStatus.Healthy ? Ok("Healthy") : StatusCode(503, "Unhealthy");
        }
    }
}
