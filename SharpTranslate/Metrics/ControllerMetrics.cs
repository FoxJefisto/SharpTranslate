using App.Metrics;
using App.Metrics.Counter;

namespace SharpTranslate.Metrics
{
    public static class ControllerMetrics
    {
        public static CounterOptions TranslateRequestCounter => new CounterOptions
        {
            Context = "Controllers",
            Name = "Translate Requests Total",
            MeasurementUnit = Unit.Requests,
            Tags = new MetricTags( new[] { "Controller" }, new[] { "TranslateController" } )
        };

        public static CounterOptions UserWordsRequestCounter => new CounterOptions
        {
            Context = "Controllers",
            Name = "UserWords Requests Total",
            MeasurementUnit = Unit.Requests,
            Tags = new MetricTags(new[] { "Controller" }, new[] { "UserWordsController" })
        };

        public static CounterOptions StatsRequestCounter => new CounterOptions
        {
            Context = "Controllers",
            Name = "Stats Requests Total",
            MeasurementUnit = Unit.Requests,
            Tags = new MetricTags(new[] { "Controller" }, new[] { "StatsController" })
        };

        public static CounterOptions TotalRequestCounter => new CounterOptions
        {
            Context = "Controllers",
            Name = "All Requests Total",
            MeasurementUnit = Unit.Requests
        };
    }
}
