using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SharpTranslate.Services
{
    public class MyHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            // Your health check logic here
            return HealthCheckResult.Healthy("MyHealthCheck is healthy");
        }
    }
}
