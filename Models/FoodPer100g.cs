using System.ComponentModel.DataAnnotations;

namespace macro_tracker_core_service.Models
{
    public class FoodPer100g
    {
        [Key]
        public int FoodId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public double Protein { get; set; }
        public double Calories { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
        public double Price { get; set; }
    }
}
