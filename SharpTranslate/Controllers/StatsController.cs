using App.Metrics;
using Microsoft.AspNetCore.Mvc;
using SharpTranslate.Metrics;
using SharpTranslate.Middlewares.Models;
using SharpTranslate.Models;
using System.Diagnostics;

namespace SharpTranslate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly RequestTracker _requestTracker;
        private readonly IMetrics _metrics;

        public StatsController(ILogger<StatsController> logger,
            IServiceProvider serviceProvider,
            RequestTracker requestTracker,
            IMetrics metrics)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _requestTracker = requestTracker;
            _metrics = metrics;
        }

        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            _logger.LogInformation("Получен запрос на получение статистики");
            _metrics.Measure.Counter.Increment(ControllerMetrics.TotalRequestCounter);

            var stats = new StatsModel
            {
                Version = typeof(StatsController)?.Assembly?.GetName()?.Version?.ToString() ?? "1.0.0",
                RequestsCount = _requestTracker.HandledRequests,
                RequestsCodes = _requestTracker.HandledCodes,
                StartTime = Process.GetCurrentProcess().StartTime.ToLocalTime()
            };

            _logger.LogInformation("Возврат статистики: {@Stats}", stats);

            return Ok(stats);
        }
    }
}
