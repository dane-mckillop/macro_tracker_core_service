namespace FoodApi.Models
{
    public class FoodPer100g
    {
        public int FoodId { get; set; }
        public string Name { get; set; }
        public float Protein { get; set; }
        public float Calories { get; set; }
        public float Carbs { get; set; }
        public float Fats { get; set; }
        public float Price { get; set; }
    }
}
