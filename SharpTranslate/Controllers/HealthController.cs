using App.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SharpTranslate.Metrics;

namespace SharpTranslate.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;
        private readonly IMetrics _metrics;

        public HealthController(HealthCheckService healthCheckService, IMetrics metrics)
        {
            _healthCheckService = healthCheckService;
            _metrics = metrics;
        }

        [HttpGet]
        public async Task<IActionResult> GetHealthAsync()
        {
            _metrics.Measure.Counter.Increment(ControllerMetrics.HealthRequestCounter);
            var result = await _healthCheckService.CheckHealthAsync();
            return result.Status == HealthStatus.Healthy ? Ok("Healthy") : StatusCode(503, "Unhealthy");
        }
    }
}
