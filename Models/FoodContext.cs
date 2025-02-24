using Microsoft.EntityFrameworkCore;

namespace FoodApi.Models
{
    public class FoodPer100gContext : DbContext
    {
        public FoodPer100gContext(DbContextOptions<FoodPer100gContext> options) : base(options) { }

        public DbSet<FoodPer100g> FoodsPer100g { get; set; }
    }
}
