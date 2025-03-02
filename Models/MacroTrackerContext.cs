using Microsoft.EntityFrameworkCore;

namespace FoodApi.Models
{
    public class MacroTrackerContext : DbContext
    {
        public MacroTrackerContext(DbContextOptions<MacroTrackerContext> options) : base(options) { }

        public DbSet<FoodPer100g> FoodsPer100g { get; set; } // Pluralized for convention, maps to FoodPer100g table

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodPer100g>().ToTable("FoodPer100g"); // Ensures exact table name
        }
    }
}