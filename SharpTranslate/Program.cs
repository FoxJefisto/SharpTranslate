using Microsoft.EntityFrameworkCore;
using SharpTranslate.Helpers;
using SharpTranslate.Helpers.Interfaces;
using SharpTranslate.Models.DatabaseModels.Core;
using SharpTranslate.Repositories;
using SharpTranslate.Repositories.Interfaces;
using SharpTranslate.Services;
using SharpTranslate.Services.Interfaces;
using SharpTranslate.Middlewares;
using SharpTranslate.Middlewares.Models;
using Serilog;
using Serilog.Exceptions;
using App.Metrics.Formatters.Prometheus;
using App.Metrics.DotNetRuntime;
using Microsoft.Extensions.DependencyInjection;

namespace SharpTranslate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = builder.Configuration;

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IWordRepository, WordRepository>();
            builder.Services.AddTransient<IWordPairRepository, WordPairRepository>();
            builder.Services.AddTransient<IUserWordPairRepository, UserWordPairRepository>();

            builder.Services.AddTransient<ITranslateHelper, TranslateHelper>();
            builder.Services.AddTransient<IUserWordsManager, UserWordsManager>();
            builder.Services.AddTransient<IWordRequestResponseConverter, WordRequestResponseConverter>();

            builder.Services.AddSingleton<RequestTracker>();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Debug()
                .WriteTo.Console()
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            builder.Host.UseSerilog();

            builder.Host
                .UseMetricsWebTracking(options =>
                {
                    options.OAuth2TrackingEnabled = true;
                })
                .UseMetricsEndpoints(options =>
                {
                options.EnvironmentInfoEndpointEnabled = true;
                options.MetricsTextEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                options.MetricsEndpointOutputFormatter = new MetricsPrometheusTextOutputFormatter();
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            app.UseMiddleware<RequestTrackerMiddleware>();

            app.Run();
        }
    }
}