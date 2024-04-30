using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using SharpTranslate.Helpers;
using SharpTranslate.Helpers.Interfaces;
using SharpTranslate.Models.DatabaseModels.Core;
using SharpTranslate.Repositories;
using SharpTranslate.Repositories.Interfaces;
using SharpTranslate.Services;
using SharpTranslate.Services.Interfaces;
using System.Text.Json.Serialization;
using System.Text.Json;

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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}