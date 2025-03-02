using Microsoft.EntityFrameworkCore;
using System;

using FoodApi.Models;

namespace FoodApi
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load environment variables
            string server = Environment.GetEnvironmentVariable("SQL_MACRO_SERVER") ?? throw new Exception("SQL_MACRO_SERVER not set");
            string database = Environment.GetEnvironmentVariable("SQL_MACRO_DATABASE") ?? throw new Exception("SQL_MACRO_DATABASE not set");
            string username = Environment.GetEnvironmentVariable("SQL_MACRO_USERNAME") ?? throw new Exception("SQL_MACRO_USERNAME not set");
            string password = Environment.GetEnvironmentVariable("SQL_MACRO_PASSWORD") ?? throw new Exception("SQL_MACRO_PASSWORD not set");

            string connectionString = $"Server={server};Database={database};User Id={username};Password={password};TrustServerCertificate=True;";

            // Set up dependency injection
            var services = new ServiceCollection()
                .AddDbContext<MacroTrackerContext>(options => options.UseSqlServer(connectionString))
                .BuildServiceProvider();

            // Access the DbContext
            using var scope = services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MacroTrackerContext>();

            try
            {
                // Query the FoodPer100g table
                var foods = dbContext.FoodsPer100g.ToList();
                Console.WriteLine("Connected successfully! Foods in FoodPer100g table:");
                foreach (var food in foods)
                {
                    Console.WriteLine($"FoodId: {food.FoodId}, Name: {food.Name}, Protein: {food.Protein}, Calories: {food.Calories}, Carbs: {food.Carbs}, Fats: {food.Fats}, Price: {food.Price}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error accessing FoodPer100g: {ex.Message}");
            }
        }
    }
}