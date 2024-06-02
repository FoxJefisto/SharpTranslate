using App.Metrics;
using App.Metrics.DotNetRuntime;
using SharpTranslate.Metrics;
using SharpTranslate.Middlewares.Models;

namespace SharpTranslate.Middlewares
{
    public class RequestTrackerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RequestTracker _requestTracker;
        private readonly IMetrics _metrics;

        public RequestTrackerMiddleware(RequestDelegate next, RequestTracker requestTracker, IMetrics metrics)
        {
            _next = next;
            _requestTracker = requestTracker;
            _metrics = metrics;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            DotNetRuntimeStatsBuilder.Default().StartCollecting(_metrics);

            // Получаем код состояния ответа
            int statusCode = context.Response.StatusCode;

            // Инкрементируем количество обработанных запросов
            _requestTracker.HandledRequests++;

            // Добавляем код состояния в список обработанных кодов
            _requestTracker.HandledCodes.Add(statusCode);
        }
    }
}
