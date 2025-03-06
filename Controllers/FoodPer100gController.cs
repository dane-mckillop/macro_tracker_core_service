using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using macro_tracker_core_service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace macro_tracker_core_service.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly MacroTrackerContext _context;

        public FoodsController(MacroTrackerContext context)
        {
            _context = context;
        }

        private bool FoodExists(int id)
        {
            return _context.FoodsPer100g.Any(e => e.FoodId == id);
        }

        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodPer100g>>> GetAllFoods()
        {
            return await _context.FoodsPer100g.ToListAsync();
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodPer100g>> GetFoodById(int id)
        {
            var food = await _context.FoodsPer100g.FindAsync(id);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }

        // POST: api/Foods
        [HttpPost]
        public async Task<ActionResult<FoodPer100g>> CreateFood(FoodPer100g food)
        {
            // Basic input validation
            if (food == null)
            {
                return BadRequest("Food item cannot be null");
            }

            //Insert food item, exceptions handled by middleware
            _context.FoodsPer100g.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFoodById), new { id = food.FoodId }, food);
        }

        // PUT: api/Foods/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFood(int id, FoodPer100g food)
        {
            if (id != food.FoodId)
            {
                return BadRequest("Id mismatch between route and food object");
            }

            var existingFood = await _context.FoodsPer100g.FindAsync(id);
            if (existingFood == null)
            {
                return NotFound($"Food item with ID {id} was not found");
            }

            _context.Entry(existingFood).CurrentValues.SetValues(food);

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
                {
                    return NotFound($"Food item with ID {id} no longer exists");
                }
                else
                {
                    throw; 
                }
            }
        }
    }
}