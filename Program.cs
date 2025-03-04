using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;
using macro_tracker_core_service.Models;
using macro_tracker_core_service.Middleware;

namespace macro_tracker_core_service
{
    class Program
    {
        static void Main(string[] args)
        {
            string server = Environment.GetEnvironmentVariable("SQL_MACRO_SERVER") ?? throw new Exception("SQL_MACRO_SERVER not set");
            string database = Environment.GetEnvironmentVariable("SQL_MACRO_DATABASE") ?? throw new Exception("SQL_MACRO_DATABASE not set");
            string username = Environment.GetEnvironmentVariable("SQL_MACRO_USERNAME") ?? throw new Exception("SQL_MACRO_USERNAME not set");
            string password = Environment.GetEnvironmentVariable("SQL_MACRO_PASSWORD") ?? throw new Exception("SQL_MACRO_PASSWORD not set");
            string connectionString = $"Server={server};Database={database};User Id={username};Password={password};TrustServerCertificate=True;";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddDbContext<MacroTrackerContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMacroExceptionHandler();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}